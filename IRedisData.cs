
namespace RediParser;

/// <summary>
/// Represents the contract for interacting with redis data types
/// </summary>
public interface IRedisData
{
    /// <summary>
    /// Serializes the data type into the REDIS format
    /// </summary>
    /// <returns>A string representing the formatted data type</returns>
    public string Serialize();
    


    /// <summary>
    /// Deserializes the REDIS format into this data type
    /// </summary>
    /// <param name="data"></param>
    public void Deserialize(string data);
}