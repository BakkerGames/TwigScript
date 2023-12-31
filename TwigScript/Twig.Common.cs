namespace TwigScript;

public partial class Twig
{
    private static readonly StringComparison OIC = StringComparison.OrdinalIgnoreCase;

    private readonly IDictionary<string, string> _dict = dict;

    private readonly Random _random = new();

    private string _yornYesScript = "";
    private string _yornNoScript = "";
    private string _yornInvalidScript = "";
    private string _answerScript = "";
}