
namespace RediParser;

/// <summary>
/// Represents the contract for interacting with redis data types
/// </summary>
public interface IRespData // Applying this to structs and calling 
{
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



    /// <summary>
    /// Serializes the data type into the REDIS format
    /// </summary>
    /// <returns>A string representing the formatted data type</returns>
    public ReadOnlySpan<byte> Serialize();
    

    /*
        We can use better form here by making the RESP data struct readonly and have overloads for its constructor
    */
    // /// <summary>
    // /// Deserializes the REDIS format into this data type
    // /// </summary>
    // /// <param name="data">The data to deserialize into the object</param>
    // public void Deserialize(ReadOnlySpan<byte> data);
    
}