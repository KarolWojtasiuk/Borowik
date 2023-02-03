using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Borowik.Books.Entities;

namespace Borowik.Database.LiteDb;

internal class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver
{
    private readonly Type[] _baseTypes = { typeof(IBookContentNode) };
    private readonly Dictionary<Type, JsonDerivedType[]> _derivedTypesCache = new();

    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        var typeInfo = base.GetTypeInfo(type, options);

        if (!_baseTypes.Contains(typeInfo.Type))
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
        return type.Assembly.GetTypes()
            .Where(t => !t.IsAbstract && t.IsAssignableTo(type))
            .Select(t => new JsonDerivedType(t, GetTypeDiscriminator(t)))
            .ToArray();
    }

    private static string GetTypeDiscriminator(Type type)
    {
        var property = type.GetProperty(nameof(IBookContentNode.Type), BindingFlags.Public | BindingFlags.Static);
        var value = property?.GetValue(null)
                    ?? throw new InvalidOperationException($"Cannot find property '{nameof(IBookContentNode.Type)}' on '{type.Name}'");

        return (string)value;
    }
}