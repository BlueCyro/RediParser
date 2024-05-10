using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace RediParser;

using static RespDataParsingHelpers;


/// <summary>
/// Represents the 'big number' RESP data type
/// </summary>
[RespData(RespDataType.BigNumber)]
public readonly struct SimpleBigNumber : IRespData
{
    /// <summary>
    /// Constructs a <see cref="SimpleBigNumber"/> using RESP data as input
    /// </summary>
    /// <param name="data">The data to deserialize</param>
    public SimpleBigNumber(ReadOnlySpan<char> data)
    {
        Number = BigInteger.Parse(data.TrimRespInfo());
    }



    /// <summary>
    /// Constructs a <see cref="SimpleBigNumber"/> using a provided <see cref="BigInteger"/>
    /// </summary>
    /// <param name="number">The error type</param>
    public SimpleBigNumber(BigInteger number)
    {
        Number = number;
    }



    /// <summary>
    /// The held big number
    /// </summary>
    public readonly BigInteger Number;



    /// <inheritdoc cref="IRespData.IsError"/>
    public readonly bool IsError => false;
    


    /// <inheritdoc cref="IRespData.BoxedData"/>
    public readonly object BoxedData => Number;



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
        return (char)RespDataType.BigNumber + Number.ToString() + CRLF;
    }
}