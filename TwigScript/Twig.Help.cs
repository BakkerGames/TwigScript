﻿using System.Text;
using static TwigScript.Constants;

namespace TwigScript;

public partial class Twig
{
    public static string Help()
    {
        StringBuilder result = new();
        
        result.AppendLine("Statements:");
        result.AppendLine($"   {COMMENT}x)");
        result.AppendLine($"   {EXEC}script)");
        result.AppendLine($"   {SCRIPT}key)");
        result.AppendLine($"   {SET}key,x)");
        result.AppendLine($"   {SWAP}key1,key2)");
        result.AppendLine();

        result.AppendLine("Numeric statements:");
        result.AppendLine($"   {ADDTO}key,x)");
        result.AppendLine($"   {DIVTO}key,x)");
        result.AppendLine($"   {MODTO}key,x)");
        result.AppendLine($"   {MULTO}key,x)");
        result.AppendLine($"   {SUBTO}key,x)");
        result.AppendLine();

        result.AppendLine("Output statements:");
        result.AppendLine($"   {NL}");
        result.AppendLine($"   {MSG}key)");
        result.AppendLine($"   {WRITE}x,...)");
        result.AppendLine();

        result.AppendLine("Functions:");
        result.AppendLine($"   {ABS}x)");
        result.AppendLine($"   {ADD}x,y)");
        result.AppendLine($"   {CONCAT}x,y,...)");
        result.AppendLine($"   {DIV}x,y)");
        result.AppendLine($"   {FORMAT}x,y0,y1,...)");
        result.AppendLine($"   {GET}key)");
        result.AppendLine($"   {GETVALUE}key)");
        result.AppendLine($"   {LOWER}x)");
        result.AppendLine($"   {MOD}x,y)");
        result.AppendLine($"   {MUL}x,y)");
        result.AppendLine($"   {REPLACE}x,y,z)");
        result.AppendLine($"   {RND}x)");
        result.AppendLine($"   {SUB}x,y)");
        result.AppendLine($"   {TRIM}x)");
        result.AppendLine($"   {UPPER}x)");
        result.AppendLine();

        result.AppendLine("If tokens:");
        result.AppendLine($"   {IF}");
        result.AppendLine($"   {THEN}");
        result.AppendLine($"   {ELSEIF}");
        result.AppendLine($"   {ELSE}");
        result.AppendLine($"   {ENDIF}");
        result.AppendLine();

        result.AppendLine("If conditions:");
        result.AppendLine($"   {EQ}x,y)");
        result.AppendLine($"   {FALSE}x)");
        result.AppendLine($"   {GE}x,y)");
        result.AppendLine($"   {GT}x,y)");
        result.AppendLine($"   {ISNULL}x)");
        result.AppendLine($"   {ISSCRIPT}x)");
        result.AppendLine($"   {LE}x,y)");
        result.AppendLine($"   {LT}x,y)");
        result.AppendLine($"   {NE}x,y)");
        result.AppendLine($"   {RAND}x)");
        result.AppendLine($"   {TRUE}x)");
        result.AppendLine();

        result.AppendLine("Condition connectors/modifiers:");
        result.AppendLine($"   {AND}");
        result.AppendLine($"   {OR}");
        result.AppendLine($"   {NOT}");

        result.AppendLine("For loop:");
        result.AppendLine($"   {FOR}token,start,end)");
        result.AppendLine($"   {ENDFOR}");
        result.AppendLine();

        result.AppendLine("For Each loop:");
        result.AppendLine($"   {FOREACH}token,prefix[,suffix])");
        result.AppendLine($"   {ENDFOREACH}");
        result.AppendLine();

        result.AppendLine("List statements/functions:");
        result.AppendLine($"   {ADDLIST}name,value)");
        result.AppendLine($"   {CLEARLIST}name)");
        result.AppendLine($"   {GETLIST}name,pos)");
        result.AppendLine($"   {INSERTATLIST}name,pos,value)");
        result.AppendLine($"   {LISTLENGTH}name)");
        result.AppendLine($"   {REMOVEATLIST}name,pos)");
        result.AppendLine($"   {SETLIST}name,pos,value)");
        result.AppendLine();

        result.AppendLine("Array statements/functions:");
        result.AppendLine($"   {CLEARARRAY}name)");
        result.AppendLine($"   {GETARRAY}name,y,x)");
        result.AppendLine($"   {SETARRAY}name,y,x,value)");
        result.AppendLine();

        result.AppendLine("Subchannel commands:");
        result.AppendLine($"   {ANSWER}script)");
        result.AppendLine($"   {QUIT}");
        result.AppendLine($"   {RESTART}");
        result.AppendLine($"   {RESTORE}");
        result.AppendLine($"   {SAVE}");
        result.AppendLine($"   {YORN}yes_script,no_script,error_script)");

        return result.ToString();
    }
}