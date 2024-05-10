using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace RediParser.Tests;

[TestClass]
public class GeneralTests
{
    [TestMethod]
    [DataRow("+OK\r\n")]
    [DataRow("-Error message\r\n")]

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
}