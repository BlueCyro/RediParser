using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace RediParser.Tests;

[TestClass]
public class SerializationTests
{
    [TestMethod]
    public void StringSerializeTest()
    {
        SimpleString toSerialize = new("Serialize me!");
        string expectedOutput = "+Serialize me!\r\n";

        Assert.AreEqual(expectedOutput, toSerialize.ToString());
    }



    [TestMethod]
    public void ErrorSerializeTest()
    {
        SimpleError toSerialize = new("URDUMB", "You are dumb!");
        string expectedOutput = "-URDUMB You are dumb!\r\n";

        Assert.AreEqual(expectedOutput, toSerialize.ToString());
    }



    [TestMethod]
    public void BigNumberSerializeTest()
    {
        BigInteger big = new(38324802394892348);
        SimpleBigNumber toSerialize = new(big);
        string expectedOutput = "(38324802394892348\r\n";

        Assert.AreEqual(expectedOutput, toSerialize.ToString());
    }
}