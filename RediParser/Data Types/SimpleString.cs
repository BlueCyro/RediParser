using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace RediParser;

using static RespDataParsingHelpers;


/// <summary>
/// Represents the 'simple string' RESP data type
/// </summary>
[RespData(RespDataType.SimpleString)]
public readonly struct SimpleString : IRespData
{
    /// <summary>
    /// Constructs a <see cref="SimpleString"/> using RESP data as input
    /// </summary>
    /// <param name="data">The data to deserialize</param>
    public SimpleString(ReadOnlySpan<char> data)
    {
        String = data.TrimRespInfo().ToString();
    }



    /// <summary>
    /// Constructs a <see cref="SimpleString"/> using a provided string as input
    /// </summary>
    /// <param name="data">The data to store</param>
    public SimpleString(string data)
    {
        String = data;
    }



    /// <inheritdoc cref="IRespData.IsError"/>
    public readonly bool IsError => false;


    /// <summary>
    /// The string that this struct contains
    /// </summary>
    public readonly string String;

    

    /// <inheritdoc cref="IRespData.BoxedData"/>
    public readonly object BoxedData => String;



    /// <inheritdoc cref="IRespData.DataType"/>
    public readonly RespDataType DataType => RespDataType.SimpleString;



    /// <inheritdoc cref="IRespData.RespDataStructureType"/>
    public readonly Type RespDataStructureType => GetType();



    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return (char)RespDataType.SimpleString + String + CRLF;
    }
}