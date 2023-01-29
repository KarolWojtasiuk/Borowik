using System.Text.Json.Serialization;

namespace Borowik.Books.Entities;

//TODO: auto generation of derived types
[JsonDerivedType(typeof(BookContentNodes.PlainTextNode), "plainText")]
public interface IBookContentNode
{
}