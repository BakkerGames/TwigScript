using System.Text;
using static TwigScript.Constants;

namespace TwigScript;

/// <summary>
/// Generate a Twig script object and assign its dictionary.
/// </summary>
public partial class Twig(IDictionary<string, string> dict)
{
    /// <summary>
    /// Sends metadata or commands back to the calling program.
    /// </summary>
    public string Subchannel { get; set; } = "";

    /// <summary>
    /// Run one script and return any text in result.
    /// </summary>
    public void RunScript(string script, StringBuilder result)
    {
        if (string.IsNullOrWhiteSpace(script) || script.Equals(NULL_VALUE, OIC))
        {
            return;
        }
        try
        {
            var tokens = SplitTokens(script);
            int index = 0;
            while (index < tokens.Length)
            {
                RunOneCommand(tokens, ref index, result);
            }
        }
        catch (Exception ex)
        {
            throw new SystemException($"{ex.Message}{Environment.NewLine}{script}");
        }
    }

    /// <summary>
    /// Handle a Yes or No input send from the calling program.
    /// The scripts are set by @YORN(yes_script,no_script,invalid_script).
    /// </summary>
    public void HandleYorn(string input, StringBuilder result)
    {
        bool answer;
        try
        {
            answer = ConvertToBool(input);
        }
        catch (Exception)
        {
            RunScript(_yornInvalidScript, result);
            Subchannel += SUBCHANNEL_YORN;
            return;
        }
        RunScript(answer ? _yornYesScript : _yornNoScript, result);
    }

    /// <summary>
    /// Handle an answer, which will be processed by a script.
    /// The script is set by @ANSWER(scriptname).
    /// </summary>
    public void HandleAnswer(string input, StringBuilder result)
    {
        Set($"{TEMP_PREFIX}{ANSWER_SUBKEY}", input);
        RunScript(_answerScript, result);
    }

    /// <summary>
    /// Format the script with line breaks and indents.
    /// </summary>
    public static string PrettyScript(string script)
    {
        if (!script.TrimStart().StartsWith('@'))
        {
            return script;
        }

        StringBuilder result = new();
        int indent = 1;
        int parens = 0;
        bool ifLine = false;
        bool forLine = false;
        bool forEachLine = false;
        var tokens = SplitTokens(script);

        foreach (string s in tokens)
        {
            switch (s)
            {
                case ELSEIF:
                    indent--;
                    break;
                case ELSE:
                    indent--;
                    break;
                case ENDIF:
                    indent--;
                    break;
                case ENDFOR:
                    indent--;
                    break;
                case ENDFOREACH:
                    indent--;
                    break;
            }
            if (parens == 0)
            {
                if (ifLine)
                {
                    result.Append(' ');
                }
                else
                {
                    result.AppendLine();
                    if (indent > 0) result.Append(new string('\t', indent));
                }
            }
            result.Append(s);
            switch (s)
            {
                case IF:
                    ifLine = true;
                    break;
                case ELSEIF:
                    ifLine = true;
                    break;
                case ELSE:
                    indent++;
                    break;
                case THEN:
                    indent++;
                    ifLine = false;
                    break;
                case FOR:
                    forLine = true;
                    break;
                case FOREACH:
                    forEachLine = true;
                    break;
            }
            if (s.EndsWith('('))
            {
                parens++;
            }
            else if (s == ")")
            {
                parens--;
                if (forLine && parens == 0)
                {
                    forLine = false;
                    indent++;
                }
                else if (forEachLine && parens == 0)
                {
                    forEachLine = false;
                    indent++;
                }
            }
        }
        if (indent != 1)
        {
            throw new SystemException($"Indent should be 1 at end of script: {indent}\n{script}");
        }
        if (parens != 0)
        {
            throw new SystemException($"Parenthesis should be 0 at end of script: {parens}\n{script}");
        }
        return result.ToString();
    }

    /// <summary>
    /// Key comparison function, returns -1/0/1. Used in keys.Sort(CompareKeys);
    /// Handles numeric key sections in numeric order, not alphabetic order.
    /// </summary>
    public static int CompareKeys(string x, string y)
    {
        if (x == null)
        {
            if (y == null) return 0;
            return -1;
        }
        if (y == null)
        {
            return 1;
        }
        if (x.Equals(y, OIC)) return 0;
        var xTokens = x.Split('.');
        var yTokens = y.Split('.');
        for (int i = 0; i < Math.Max(xTokens.Length, yTokens.Length); i++)
        {
            if (i >= xTokens.Length) return -1; // x is shorter and earlier
            if (i >= yTokens.Length) return 1; // y is shorter and earlier
            if (xTokens[i].Equals(yTokens[i], OIC)) continue;
            if (xTokens[i] == "*") return 1; // x is later
            if (yTokens[i] == "*") return -1; // y is later
            if (int.TryParse(xTokens[i], out int xVal) && int.TryParse(yTokens[i], out int yVal))
            {
                if (xVal == yVal) continue;
                return (xVal < yVal) ? -1 : 1;
            }
            return string.Compare(xTokens[i], yTokens[i], OIC);
        }
        return 0;
    }
}