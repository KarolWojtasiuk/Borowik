using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Borowik.Books.Entities;

namespace Borowik.Database.Dexie;

internal class BookContentNodeTypesResolver : DefaultJsonTypeInfoResolver
{
    private readonly Dictionary<Type, JsonDerivedType[]> _derivedTypesCache = new();

    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        var typeInfo = base.GetTypeInfo(type, options);

        if (typeInfo.Type != typeof(IBookContentNode))
            return typeInfo;

        typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
        {
            TypeDiscriminatorPropertyName = "$type",
            IgnoreUnrecognizedTypeDiscriminators = false,
            UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
        };

        foreach (var derivedType in GetDerivedTypesForBaseType(typeInfo.Type))
            typeInfo.PolymorphismOptions.DerivedTypes.Add(derivedType);

        return typeInfo;
    }

    private IEnumerable<JsonDerivedType> GetDerivedTypesForBaseType(Type type)
    {
        if (_derivedTypesCache.TryGetValue(type, out var derivedTypes))
            return derivedTypes;

        return _derivedTypesCache[type] = FindDerivedTypesForBaseType(type);
    }

    private static JsonDerivedType[] FindDerivedTypesForBaseType(Type type)
    {
        var baseTypeDiscriminator = GetTypeDiscriminator(type);

        var derivedTypes = type.Assembly.GetTypes()
            .Where(t => !t.IsAbstract && t.IsAssignableTo(type))
            .Select(t => new JsonDerivedType(t, GetTypeDiscriminator(t)))
            .ToArray();

        if (derivedTypes.Any(t => t.TypeDiscriminator is null))
            throw new InvalidOperationException($"Every derived type of {type.Name} must have type discriminator");

        if (derivedTypes.Any(t => t.TypeDiscriminator as string == baseTypeDiscriminator))
            throw new InvalidOperationException("Derived type cannot have the same type discriminator as base type");

        if (derivedTypes.GroupBy(t => t.TypeDiscriminator).Any(g => g.Count() > 1))
            throw new InvalidOperationException("Multiple derived type cannot have the same type discriminator");

        return derivedTypes;
    }

    private static string GetTypeDiscriminator(Type type)
    {
        var property = type.GetProperty(nameof(IBookContentNode.Type), BindingFlags.Public | BindingFlags.Static);
        var value = property?.GetValue(null)
                    ?? throw new InvalidOperationException($"Cannot find property '{nameof(IBookContentNode.Type)}' on '{type.Name}'");

        return (string)value;
    }
}