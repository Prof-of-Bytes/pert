namespace PertTests;

using PERTLib.MenuItem;
using Microsoft.VisualBasic;
using PERTLib.Structures;

[TestClass]
public sealed class TestProgrammingProblem
{
    private readonly ProgrammingProblem _testProblem = new("Temp Converts", ProgrammingLanguages.CSharp);
    [TestMethod]
    public void TestInfo()
    {
        var info = _testProblem.ProblemInfo();
        Assert.IsNotNull(info);
        Assert.IsInstanceOfType<int>(info.Item1);
        Assert.IsInstanceOfType<string>(info.Item2);
    }
    [TestMethod]
    public void TestMethods()
    {
        var c = _testProblem.GetConcepts();
        var h = _testProblem.GetHints();

        Assert.IsNotNull(c);
        Assert.AreEqual(c.Length, 5);
        Assert.AreEqual(h.Length, 5);
        Assert.IsNull(c[0]);
        //Assert.IsNotNull(c[1]);//should fail

    }
    [TestMethod]
    public void PrintInfo()
    {
        var info = _testProblem.ProblemInfo();
        Console.WriteLine(info);
    }


}
[TestClass]
public sealed class TestingMenuItem
{
    private readonly ConsoleMenuItem menutest = new(Console.In, Console.Out, "menu test", false, null);
    [TestMethod]
    public void TestPrintTest()
    {

        menutest.PrintName();

    }
    [TestMethod]
    public void TestProgramState()
    {
        var programstate = menutest.CurrentState;
        Assert.AreEqual<PERTLib.MenuItem.PROGAM_STATE>(programstate, PERTLib.MenuItem.PROGAM_STATE.NOTHING);
    }
}
