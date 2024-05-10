# Preamble

You may notice first of all that this library uses interface-implementing structs rather than inherited classes.

This is somewhat unconventional, but given that the RESP protocol was built for a very scalable framework, ensuring that this library remains viable even in high-stress workloads was an important consideration.

Building out each RESP data type to be represented by a struct will ensure several things:

1) There is no shared code, meaning that the JIT (Just-In-Time) compiler will be forced to make unique code for each implementation of this struct.
2) Functions which take a non-specific IRespData object can be constrained to `struct, IRespData` to avoid virtual calls.
3) In situations where virtual calls are avoided, inlining can become possible.

These things allow the code gen to produce more optimal machine assembly. This is beneficial for hot paths where the (albeit very minor) overhead of virtual dispatch is simply not acceptable.

## Parsing RESP input

Doing so is very easy. You simply call `RespDataParsingHelpers.ParseRespData(string data)` and you receive an `IRespData` object that represents the deserialized data.

Example:

```csharp
IRespData data = RespDataHelpers.ParseRespData("+HELLOTHERE\r\n")

Console.WriteLine(data.BoxedValue);
```

Do note that this will look up a the corresponding constructor delegate in a pre-computed immutable dictionary for the type of RESP data you're receiving, and thus does **not** avoid virtual dispatch.

This method of data access will also perform boxing as `BoxedValue` will return an object (either of the type directly, or of a variable-length tuple). This is convenient for more moderate workloads where you just need to get the job done and want to keep the code short.

This is useful for general purpose parsing, but each specific struct can be constructed with either default input parameters for that type, or be given a corresponding serialized RESP string that is of the proper type. This is useful for efficiently constructing or deserializing data you know the type of beforehand.

String deserialization example:
```csharp
List<string> otherCollection = [];

SimpleString[] datas =
[ 
    new("+ITSME\r\n"),
    new("+YABOY\r\n")
];

foreach (var struct in datas)
{
    otherCollection.Add(struct.String) // Will add strings with "ITSME" and "YABOY" as their contents to the list
}
```

You can also very easily serialize these structures into proper RESP format by simply calling `ToString()` on them:

```csharp
string[] datas =
[
    "HELLOTHERE",
    "HOWAREYOU"
];

foreach (string entry in datas)
{
    string serialized = new(entry).ToString(); // Outputs "+HELLOTHERE\r\n" and "+HOWAREYOU\r\n"

    SendThisSomewhere(serialized);
}
```

Creating a new instance just to ToString it may seem inefficient, but since no virtual calls are being made here, the JIT can seomtimes optimize away the constructors entirely.

