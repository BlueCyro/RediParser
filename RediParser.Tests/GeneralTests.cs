using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace RediParser.Tests;

[TestClass]
public class GeneralTests
{
    [TestMethod]
    [DataRow("+OK\r\n")]
    [DataRow("-Error message\r\n")]
    [DataRow("(3759823758923758923\r\n")]

    public void AllTypeSmokeTest(string input)
    {
        try
        {
            IRespData dataStructure = RespDataParsingHelpers.ParseRespInput(input);
        }
        catch (NotImplementedException e)
        {
            Assert.Fail(e.Message);
        }
    }



    [TestMethod]
    [DataRow("@!asdf3878$#(*%)\n")]         // Completely jumbled
    [DataRow("(DefinitelyNotABigNumber\r")] // Correct beginning, wrong ending
    [DataRow("ERR This has failed\r\n")]    // Correct ending, wrong beginning
    [DataRow("(Tricky\r\n")]                // This one will fail, more robust handling needs to be implemented.
    public void RespValidationTest(string input)
    {
        if (RespDataParsingHelpers.ValidateRESP(input)) // TODO: More robust RESP validation needs to happen
        {
            Assert.Fail($"Malformed data was taken as valid! Input was: {input}");
        }
    }
}