
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace RediParser;

/// <summary>
/// Delegate signature to construct an RESP data structure
/// </summary>
/// <param name="data">Data to serialize</param>
/// <returns>Deserialized RESP data structure</returns>
public delegate IRespData RespDataConstructor(ReadOnlySpan<char> data);



/// <summary>
/// Provides methods for handling RESP data
/// </summary>
public static class RespDataParsingHelpers
{
    /// <summary>
    /// Represents a carriage return
    /// </summary>
    public const byte CR = 0x0D;



    /// <summary>
    /// Represents a line feed
    /// </summary>
    public const byte LF = 0x0A;


    /// <summary>
    /// Represents a CRLF line-ending
    /// </summary>
    public const string CRLF = "\r\n";



    static RespDataParsingHelpers()
    {
        // Look up all RESP handlers and make a dictionary of of data types -> handler constructors
        // I could just register these all manually, but this is less work for implementation if I
        // need to change something.

        Type respDataType = typeof(IRespData);
        Type constructorInputType = typeof(ReadOnlySpan<char>);
        _respDataMap =                          
            typeof(IRespData)                   
                .Assembly                       
                .GetTypes()
                .Where(t => t.GetInterfaces().Contains(respDataType))
                .Where(t => t.GetCustomAttribute<RespDataAttribute>() != null)
                .ToImmutableDictionary(
                    t =>
                        t.GetCustomAttribute<RespDataAttribute>()!.DataType,
                    t =>
                    {
                        var dataConstructor = t.GetConstructor( [ constructorInputType ] ) ?? throw new Exception($"No matching constructor was found! Make sure {t} has a constructor that takes {constructorInputType}!");
                        var param = Expression.Parameter(constructorInputType, "data");
                        var construct = Expression.New(dataConstructor, param);
                        var converted = Expression.Convert(construct, respDataType);
                        var lambda = Expression.Lambda<RespDataConstructor>(converted, param);

                        return lambda.Compile();
                    }); // Oh LinQ, you are indeed a guilty pleasure.

        #if DEBUG
        Console.WriteLine($"Static constructor on {typeof(RespDataParsingHelpers)} complete!");

        foreach (var mapped in _respDataMap)
        {
            Console.WriteLine($"{mapped.Key}, {mapped.Value}");
        }
        #endif
    }


    static readonly ImmutableDictionary<RespDataType, RespDataConstructor> _respDataMap; // TODO: Find a way to let the JIT inline a call to the constructors, delegates are not inlined v.v
    


    /// <summary>
    /// Parses a a single RESP input
    /// </summary>
    /// <param name="input">Serialized RESP data</param>
    /// <returns>The corresponding deserialized RESP data structure</returns>
    /// <exception cref="NotImplementedException">Thrown if a particular data type is not supported by the parser</exception>
    public static IRespData ParseRespInput(string input)
    {
        ReadOnlySpan<char> inputData = input.AsSpan();
        RespDataType dataType = (RespDataType)inputData[0];

        #if DEBUG
        Console.WriteLine($"Parsing RESP data input for \"{input}\", type is {dataType}");
        #endif


        _respDataMap.TryGetValue(dataType, out RespDataConstructor? constructor);
        return constructor?.Invoke(inputData) ?? throw new NotImplementedException($"RESP data type not implemented for: {dataType}");
    }



    /// <summary>
    /// Interprets a string as a ReadOnly byte span
    /// </summary>
    /// <param name="input">String to interpret</param>
    /// <returns>Read-only byte span</returns>
    public static ReadOnlySpan<byte> AsByteSpan(this string input)
    {
        return MemoryMarshal.Cast<char, byte>(input.AsSpan());
    }



    /// <summary>
    /// Interprets a <see cref="ReadOnlySpan{T}"/> of <see cref="T:byte"/> as <see cref="T:char"/>
    /// </summary>
    /// <param name="input">Byte span to reinterpret</param>
    /// <returns>Read-only char span</returns>
    public static ReadOnlySpan<char> AsCharSpan(this ReadOnlySpan<byte> input)
    {
        return MemoryMarshal.Cast<byte, char>(input);
    }



    /// <summary>
    /// Trims the RESP type and the ending CRLF markers from the input
    /// </summary>
    /// <param name="input">RESP byte span</param>
    /// <returns>Trimmed data</returns>
    public static ReadOnlySpan<char> TrimRespInfo(this ReadOnlySpan<char> input)
    {
        return input[1..].TrimEnd(CRLF);
    }
}