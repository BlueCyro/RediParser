using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RediParser;


namespace RediParser.Tests;


[TestClass]
public class ParsingTests
{
    [TestMethod]
    public void BoxedStringParseTest()
    {
        string toParse = "+OK\r\n";
        string expectedOutput = "OK";

        IRespData structure = RespDataParsingHelpers.ParseRespInput(toParse);

        Assert.AreEqual(expectedOutput, structure.BoxedData);
    }



    [TestMethod]
    public void BoxedErrorParseTest()
    {
        string toParse = "-Error message\r\n";
        (string expectedError, string expectedMessage) = ("Error", "message");

        IRespData structure = RespDataParsingHelpers.ParseRespInput(toParse);

        if (!structure.IsError)
            Assert.Fail("Error not marked correctly");
        
        (string BoxedError, string BoxedMessage) = ((string, string))structure.BoxedData;

        Assert.AreEqual(expectedError, BoxedError);
        Assert.AreEqual(expectedMessage, BoxedMessage);
    }



    [TestMethod]
    public void ErrorParseTest()
    {
        string toParse = "-Error message\r\n";
        (string expectedError, string expectedMessage) = ("Error", "message");

        IRespData structure = RespDataParsingHelpers.ParseRespInput(toParse);

        if (!structure.IsError)
            Assert.Fail("Error not marked correctly");
        
        SimpleError errorStructure = (SimpleError)structure;

        Assert.AreEqual(expectedError, errorStructure.Error);
        Assert.AreEqual(expectedMessage, errorStructure.Message);
    }



    [TestMethod]
    public void BoxedBigNumberParseTest()
    {
        string toParse = "(47593754793725993745\r\n";
        decimal big = 47593754793725993745.0m;

        BigInteger expectedOutput = new(big);

        IRespData structure = RespDataParsingHelpers.ParseRespInput(toParse);
        

        Assert.AreEqual(expectedOutput, structure.BoxedData);
    }



    [TestMethod]
    public void BigNumberParseTest()
    {
        string toParse = "(47593754793725993745\r\n";
        decimal big = 47593754793725993745.0m;

        BigInteger expectedOutput = new(big);

        IRespData structure = RespDataParsingHelpers.ParseRespInput(toParse);


        if (structure is not SimpleBigNumber bigNum)
        {
            Assert.Fail("Structure failed to parse as big number!");
            return;
        }
        

        Assert.AreEqual(expectedOutput, bigNum.Number);
    }
}
