
namespace RediParser;

/// <summary>
/// Marks a structure for handling a particular RESP type
/// </summary>
/// <param name="dataType">The RESP data type this structure should handle</param>
[AttributeUsage(AttributeTargets.Struct)]
public class RespDataAttribute(RespDataType dataType) : Attribute
{
    /// <summary>
    /// The RESP data type to be handled
    /// </summary>
    public RespDataType DataType = dataType;
}