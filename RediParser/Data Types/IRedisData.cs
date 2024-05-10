
namespace RediParser;

/// <summary>
/// Represents the contract for interacting with redis data types
/// </summary>
public interface IRespData
{
    /// <summary>
    /// Returns true if this structure is an error
    /// </summary>
    public bool IsError { get; }


    
    /// <summary>
    /// The boxed object representation of the data in this structure
    /// </summary>
    public object BoxedData { get; }



    /// <summary>
    /// The type of data held by this structure
    /// </summary>
    public RespDataType DataType { get; }



    /// <summary>
    /// The type of the object used to store the RESP data
    /// </summary>
    public Type RespDataStructureType { get; }
}