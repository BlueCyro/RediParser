using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace RediParser;

using static RespDataParsingHelpers;


/// <summary>
/// Represents the 'simple error' RESP data type
/// </summary>
[RespData(RespDataType.SimpleError)]
public readonly struct SimpleError : IRespData
{
    /// <summary>
    /// Constructs a <see cref="SimpleError"/> using RESP data as input
    /// </summary>
    /// <param name="data">The data to deserialize</param>
    public SimpleError(ReadOnlySpan<char> data)
    {
        var trimmedData = data.TrimRespInfo();
        int separator = trimmedData.IndexOf(' ');

        Error = trimmedData[..separator].ToString();
        Message = trimmedData[(separator + 1)..].ToString();
    }



    /// <summary>
    /// Constructs a <see cref="SimpleError"/> using a provided error type string and message
    /// </summary>
    /// <param name="error">The error type</param>
    /// <param name="message">The error message</param>
    public SimpleError(string error, string message)
    {
        Error = error;
        Message = message;
    }



    /// <inheritdoc cref="IRespData.IsError"/>
    public readonly bool IsError => true;



    /// <summary>
    /// The name of the error
    /// </summary>
    public readonly string Error;



    /// <summary>
    /// The message the error contains
    /// </summary>
    public readonly string Message;

    

    /// <inheritdoc cref="IRespData.BoxedData"/>
    public readonly object BoxedData => (Error, Message);



    /// <inheritdoc cref="IRespData.DataType"/>
    public readonly RespDataType DataType => RespDataType.SimpleString;



    /// <inheritdoc cref="IRespData.RespDataStructureType"/>
    public readonly Type RespDataStructureType => GetType();



    /// <summary>
    /// Serializes the data into RESP
    /// </summary>
    /// <returns>RESP string</returns>
    public override string ToString()
    {
        return (char)RespDataType.SimpleError + Error + ' ' + Message + CRLF;
    }
}