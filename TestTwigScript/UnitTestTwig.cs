using System.Text;
using TwigScript;

namespace TestTwigScript;

public class Tests
{
    [Test]
    public void TestGet()
    {
        Dictionary<string, string> data = [];
        Twig twig = new(data);
        var key = "abc";
        var value = "123";
        data.Add(key, value);
        StringBuilder result = new();
        twig.RunScript($"@get({key})", result);
        Assert.That(result.ToString(), Is.EqualTo(value));
    }

    [Test]
    public void TestSet()
    {
        Dictionary<string, string> data = [];
        Twig twig = new(data);
        var key = "abc";
        var value = "123";
        StringBuilder result = new();
        twig.RunScript($"@set({key},{value})", result);
        twig.RunScript($"@get({key})", result);
        Assert.That(result.ToString(), Is.EqualTo(value));
    }

    [Test]
    public void TestSetArray()
    {
        Dictionary<string, string> data = [];
        Twig twig = new(data);
        var key = "abc";
        var value = "123";
        StringBuilder result = new();
        twig.RunScript($"@setarray({key},2,3,{value})", result);
        twig.RunScript($"@getarray({key},2,3)", result);
        Assert.That(result.ToString(), Is.EqualTo(value));
    }

    [Test]
    public void TestSetArray_Null()
    {
        Dictionary<string, string> data = [];
        Twig twig = new(data);
        var key = "abc";
        var value = "";
        StringBuilder result = new();
        twig.RunScript($"@setarray({key},2,3,{value})", result);
        twig.RunScript($"@getarray({key},2,3)", result);
        Assert.That(result.ToString(), Is.EqualTo(value));
    }

    [Test]
    public void TestClearArray()
    {
        Dictionary<string, string> data = [];
        Twig twig = new(data);
        var key = "abc";
        var value = "";
        StringBuilder result = new();
        twig.RunScript($"@setarray({key},2,3,{value})", result);
        twig.RunScript($"@cleararray({key})", result);
        twig.RunScript($"@getarray({key},2,3)", result);
        Assert.That(result.ToString(), Is.EqualTo(""));
    }

    [Test]
    public void TestSetList()
    {
        Dictionary<string, string> data = [];
        Twig twig = new(data);
        var key = "abc";
        var value = "123";
        StringBuilder result = new();
        twig.RunScript($"@setlist({key},1,{value})", result);
        twig.RunScript($"@getlist({key},1)", result);
        Assert.That(result.ToString(), Is.EqualTo(value));
    }

    [Test]
    public void TestSetList_Null()
    {
        Dictionary<string, string> data = [];
        Twig twig = new(data);
        var key = "abc";
        var value = "";
        StringBuilder result = new();
        twig.RunScript($"@setlist({key},1,{value})", result);
        twig.RunScript($"@getlist({key},1)", result);
        Assert.That(result.ToString(), Is.EqualTo(value));
    }

    [Test]
    public void TestInsertAtList()
    {
        Dictionary<string, string> data = [];
        Twig twig = new(data);
        var key = "abc";
        var value = "123";
        StringBuilder result = new();
        twig.RunScript($"@addlist({key},0)", result);
        twig.RunScript($"@addlist({key},1)", result);
        twig.RunScript($"@addlist({key},2)", result);
        twig.RunScript($"@addlist({key},3)", result);
        twig.RunScript($"@insertatlist({key},1,{value})", result);
        twig.RunScript($"@getlist({key},1)", result);
        Assert.That(result.ToString(), Is.EqualTo(value));
        result.Clear();
        twig.RunScript($"@getlist({key},4)", result);
        Assert.That(result.ToString(), Is.EqualTo("3"));
    }

    [Test]
    public void TestRemoveAtList()
    {
        Dictionary<string, string> data = [];
        Twig twig = new(data);
        var key = "abc";
        var value = "123";
        StringBuilder result = new();
        twig.RunScript($"@setlist({key},3,{value})", result);
        twig.RunScript($"@removeatlist({key},0)", result);
        twig.RunScript($"@getlist({key},2)", result);
        Assert.That(result.ToString(), Is.EqualTo(value));
    }

    [Test]
    public void TestFunction()
    {
        Dictionary<string, string> data = [];
        Twig twig = new(data);
        StringBuilder result = new();
        twig.RunScript("@set(\"@boo\",\"@write(eek!)\")", result);
        twig.RunScript("@boo", result);
        Assert.That(result.ToString(), Is.EqualTo("eek!"));
    }

    [Test]
    public void TestFunctionParameters()
    {
        Dictionary<string, string> data = [];
        Twig twig = new(data);
        StringBuilder result = new();
        twig.RunScript("@set(\"@boo(x)\",\"@write($x)\")", result);
        twig.RunScript("@boo(eek!)", result);
        Assert.That(result.ToString(), Is.EqualTo("eek!"));
    }

    [Test]
    public void TestValidateSucceed()
    {
        Dictionary<string, string> data = [];
        Twig twig = new(data);
        StringBuilder result = new();
        bool value = twig.Validate("@set(key,value)", result);
        Assert.That(value, Is.EqualTo(true));
    }

    [Test]
    public void TestValidateFail()
    {
        Dictionary<string, string> data = [];
        Twig twig = new(data);
        StringBuilder result = new();
        bool value = twig.Validate("@blah(key)", result);
        Assert.That(value, Is.EqualTo(false));
    }
}