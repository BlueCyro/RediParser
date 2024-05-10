
namespace RediParser;


/// <summary>
/// Represents a byte denoting a data type for an RESP message
/// </summary>
public enum RespDataType : byte
{
    /// <summary>
    /// Simple string type denoted by '+'
    /// </summary>
    SimpleString = (byte)'+',

    /// <summary>
    /// Simple error type denoted by '-'
    /// </summary>
    SimpleError = (byte)'-',

    /// <summary>
    /// Integer type denoted by ':'
    /// </summary>
    Integer = (byte)':',

    /// <summary>
    /// Bunk string type denoted by '$'
    /// </summary>
    BulkString = (byte)'$',

    /// <summary>
    /// Array type denoted by '*'
    /// </summary>
    Array = (byte)'*',

    /// <summary>
    /// Null type denoted by '_'
    /// </summary>
    Null = (byte)'_',

    /// <summary>
    /// Boolean type denoted by '#'
    /// </summary>
    Boolean = (byte)'#',

    /// <summary>
    /// Double type denoted by ','
    /// </summary>
    Double = (byte)',',

    /// <summary>
    /// Big number type denoted by '('
    /// </summary>
    BigNumber = (byte)'(',

    /// <summary>
    /// Bunk error type denoted by '!'
    /// </summary>
    BulkError = (byte)'!',

    /// <summary>
    /// Verbatim string type denoted by '='
    /// </summary>
    VerbatimString = (byte)'=',

    /// <summary>
    /// Map type denoted by '%'
    /// </summary>
    Map = (byte)'%',

    /// <summary>
    /// Set type denoted by '~'
    /// </summary>
    Set = (byte)'~',
    
    /// <summary>
    /// Push type denoted by '>'
    /// </summary>
    Push = (byte)'>'
}