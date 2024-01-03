using System.Text;
using static TwigScript.Constants;

namespace TwigScript;

public partial class Twig
{
    public bool Validate(string script, StringBuilder result)
    {
        if (string.IsNullOrWhiteSpace(script))
        {
            return true;
        }
        try
        {
            var tokens = SplitTokens(script);
            int index = 0;
            int parenLevel = 0;
            while (index < tokens.Length)
            {
                if (tokens[index].StartsWith('@'))
                {
                    if (tokens[index].EndsWith('('))
                    {
                        parenLevel++;
                    }
                    if (KEYWORDS.Contains(tokens[index]))
                    {
                        index++;
                        continue;
                    }
                    if (tokens[index].EndsWith('('))
                    {
                        var dict = GetByPrefix(tokens[index]);
                        if (dict.Count == 0)
                        {
                            result.AppendLine($"Token not found: {tokens[index]}");
                            return false;
                        }
                    }
                    else
                    {
                        var value = Get(tokens[index]);
                        if (value == "")
                        {
                            result.AppendLine($"Token not found: {tokens[index]}");
                            return false;
                        }
                    }
                }
                else
                {
                    if (tokens[index] == ")")
                    {
                        parenLevel--;
                    }
                }
                index++;
            }
            if (parenLevel != 0)
            {
                result.AppendLine("Mismatched parenthesis");
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            result.AppendLine(ex.Message);
            return false;
        }
    }

    private readonly List<string> KEYWORDS =
    [
        ABS,
        ADD,
        ADDLIST,
        ADDTO,
        AND,
        ANSWER,
        CLEARARRAY,
        CLEARLIST,
        COMMENT,
        CONCAT,
        DIV,
        DIVTO,
        ELSE,
        ELSEIF,
        ENDFOR,
        ENDFOREACH,
        ENDIF,
        EQ,
        EXEC,
        FALSE,
        FALSE_VALUE,
        FOR,
        FOREACH,
        FORMAT,
        GE,
        GET,
        GETARRAY,
        GETLIST,
        GETVALUE,
        GT,
        IF,
        INSERTATLIST,
        ISNULL,
        ISSCRIPT,
        LE,
        LISTLENGTH,
        LOWER,
        LT,
        MOD,
        MODTO,
        MSG,
        MUL,
        MULTO,
        NE,
        NL,
        NOT,
        NULL_VALUE,
        OR,
        QUIT,
        RAND,
        REMOVEATLIST,
        REPLACE,
        RESTART,
        RESTORE,
        RND,
        SAVE,
        SCRIPT,
        SET,
        SETARRAY,
        SETLIST,
        SUB,
        SUBTO,
        SWAP,
        THEN,
        TRIM,
        TRUE,
        TRUE_VALUE,
        UNDO,
        UPPER,
        WRITE,
        YORN,
    ];
}