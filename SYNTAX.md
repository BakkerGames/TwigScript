# TwigScript Syntax

TwigScript uses a dictionary of (key,value) pairs. Keys cannot be null, "", or contain only whitespace. Values which are null are changed to "" may be stored as "null".

Below, "key" specifies the key of a pair in the dictionary, and "value" refers to the value stored for that key. The actual value for "key" may be constructed by other commands.

"Raw value" refers to the value from the dictionary with no processing. "Processed value" will run value as a script first if it starts with "@", and return the final result.

Scripts are processed by RunScript(script, result), with "result" a StringBuilder parameter that will contain any output.

There is also a "Subchannel" property which is used for communication between TwigScript and the calling program, separate from "result".


<br>

## Constants

"null"

"true"

"false"


<br>

## Statements

@comment("comment text")

>Used for commenting code. Quotes are recommended.

@exec(value)

>Executes the script specified by "value".

@script(key)

>Runs the script stored in "key".

@set(key,value)

>Sets the value for "key" in the dictionary to "value".

@swap(key1,key2)

>Swaps the values stored in "key1" and "key2".


<br>

## Numeric Statements

@addto(key,value)

>Adds integer "value" to that stored in "key". Stores answer back in "key".

@subto(key,value)

>Subtracts integer "value" from that stored in "key". Stores answer back in "key".

@multo(key,value)

>Multiples integer "value" by that stored in "key". Stores answer back in "key".

@divto(key,value)

>Divides "value" stored in "key" by integer value. Stores answer back in "key".

@modto(key,value)

>Divides "value" stored in "key" by integer value. Stores remainder back in "key".


<br>

## Output Statements

@msg(key)

>Writes the processed value stored in "key" plus "\n" (two separate characters) to result.

@nl

>Writes "\n" (two separate characters) to result.

@write(value1",value2...")

>Writes all the values concatinated to result.


<br>

## Functions

@abs(value)

>Returns the absolute value of integer "value".

@add(value1,value2)

>Returns the integer answer from adding "value1" and "value2".

@concat(value1,value2...)

>Concatenates all specified values into a single string.

@div(value1,value2)

>Returns the integer answer from dividing "value1" by "value2".

@format(value,v0,v1...)

>Replaces tokens "{0}", "{1}"... in value with v0, v1... and returns the result.

@get(key)

>Returns the raw value for "key" from the dictionary.

@getvalue(key)

>Returns the processed value for "key", running it as a script if necessary.

@lower(value)

>Returns "value" changed to all lowercase.

@mod(value1,value2)

>Returns the integer remainder from dividing "value1" by "value2".

@mul(value1,value2)

>Returns the integer answer from multiplying "value1" and "value2".

@replace(value,old,new)

>Returns "value" with all occurances of "old" changed to "new".

@rnd(value)

>Returns a random integer from 0 to "value" minus 1.

@sub(value1,value2)

>Returns the integer answer from subtracting "value2" from "value1".

@trim(value)

>Trims leading and trailing spaces from "value".

@upper(value)

>Returns "value" changed to all uppercase.


<br>

## If Block

@if

>Starts an "@if" block. Followed by condition statements until "@then".

@then

>End of conditions, begin running statements if conditions were true. Required after "@if" and after each "@elseif".

@elseif

>Starts another "@if" block if the first one was not true. Followed by conditions, "@then", and statements. Multiple "@elseif" blocks may be chained one after another. Optional.

@else

>Final statements to be processed if all previous conditions were false. Optional.

@endif

>Ends the "@if" block. Required.


<br>

## If Conditions

Conditions return true/false values, or values that are "truthy" or "falsey".

"true", "t", "on", "yes", "y", "1", and "-1" are truthy.

"false", "f", "off", "no", "n", "0", "null", and "" are falsey.

Conditions are processed strictly left-to-right, with no parenthesis used for grouping. They are connected with "@and" and "@or", which short-circuit when possible.

Any functions which returns truthy or falsey values may be defined and used as "@if" conditions.

@eq(value1,value2)

>Checks if the two values are equal. Compares as integers if both convert to integers, otherwise compares as exact strings (ignoring case).

@false(value)

>Returns true if "value" is falsey.

@ge(value1,value2)

>Checks if integer "value1" is greater than or equal to integer "value2". Error if not integers.

@gt(value1,value2)

>Checks if integer "value1" is greater than integer "value2". Error if not integers.

@isnull(value)

>Returns true if "value" is "" or "null",

@isscript(value)

>Checks if "value" starts with "@".

@le(value1,value2)

>Checks if integer "value1" is less than or equal to integer "value2". Error if not integers.

@lt(value1,value2)

>Checks if integer "value1" is less than integer "value2". Error if not integers.

@ne(value1,value2)

>Checks if the two values are not equal. Compares as integers if both convert to integers, otherwise compares as exact strings (ignoring case).

@rand(value)

>Checks if a random integer 0-99 is less than integer "value" 1-100. Shortened version of "@lt(@rnd(100),value)".

@true(value)

>Returns true if "value" is truthy.


<br>

## Condition Connectors/Modifiers

@and

>Stops processing if the condition so far is false and jumps over the "@then" statements to "@elseif", "@else", or "@endif". Otherwise continues.

@or

>Stops processing if the condition so far is true and jumps to the "@then" statements. Otherwise continues.

@not

>Reverses the answer on the next condition.


<br>

## For Loop

@for(token,start,end)

>Executes the code in the "for" block multiple times, by replacing "$token" (the token with a leading "$") anywhere in it with the numeric values from "start" to "end" (inclusive). Tokens can be anything, such as "i", "x", "y", "token". Nesting is allow if the tokens are different.

@endfor

>Marks the end of the "@for" loop.


<br>

## For Each Loop

@foreach(token,prefix",suffix")

>Executes the code in the "foreach" block multiple times, by replacing "$token" (the token with a leading "$") anywhere in it. It loops through all the keys in the dictionary which start with "prefix" and optionally end with "suffix". The value replaced for "$token" is the remaining part of the key after the prefix and the optional suffix are removed. Tokens can be anything, such as "i", "x", "y", "token". Nesting is allow if the tokens are different. Note that the order of keys returned is not deterministic.

@endforeach

>Marks the end of the "@foreach" loop.


<br>

## List Statements/Functions

These commands allow named lists of values to be stored as a single group instead of separate key/value pairs. They can be indexed by number, appended to, and cleared. The items will be separated by commas, so commas in the values will be replaced by the token "\x2C" when stored. Empty strings "" will be replaced by "null" when stored so they can be handled properly. Lists are stored in the dictionary with the keys "list.{name}".

@addlist(name,value)

>Adds value to the end of the list "name".

@clearlist(name)

>Clears the list "name".

@getlist(name,pos)

>Gets the value at position "pos" (starting at 0) for the list "name". If "pos" is beyond the end of the list, "" is returned.

@insertatlist(name,pos,value)

>Inserts "value" at position "pos" (starting at 0) from list, shifting all later ones. If "pos" is past the end of the list, all values from the end up to "pos" are filled with "null" and then value is added.

@listlength(name)

>Returns the number of items in list "name".

@removeatlist(name,pos)

>Removes the value at position "pos" (starting at 0) from list, shifting all later ones. If "pos" is past the end of the list, nothing happens.

@setlist(name,pos,value)

>Sets the value at position "pos" (starting at 0) for the list "name". If "pos" is beyond the end of the list, all values from the end up to "pos" are filled with "null" and then value is added.


<br>

## Array Statements/Functions

These commands allow a two-dimensional array of values to be stored as a group. They are sparse arrays with unspecified values returned as "". The items will be separated by commas, so commas in the values will be replaced by the token "\x2C" when stored. Empty strings "" will be replaced by "null" when stored so they can be handled properly. Arrays are stored in the dictionary with the keys "array.{name}.{y}", with "{y}" as the row number.

Note that the array values are referenced by row (y) first and then column (x), both starting at 0. Negative indexes throw an error.

@cleararray(name)

>Clears all values from the array "name".

@getarray(name,y,x)

>Gets the value at position "y,x" (starting at 0,0) for the array "name". If either "y" or "x" is beyond the edge of the array, "" is returned.

@setarray(name,y,x,value)

>Sets the value at position "y,x" (starting at 0,0) for the array "name". If either "y" or "x" is beyond the edge of the array, missing values will be set to "null" as needed.


<br>

## Subchannel Commands

@answer(script)

>Sends an "#ANSWER;" message on the subchannel, indication it would like the calling program to send an answer. The script is a key to a saved script in the dictionary. The specified script will be run with the answer provided in a temporary value (key="temp.answer"). The calling program is expected to call "HandleAnswer(input, result)" with the answer.

@quit

>Sends a "#GAMEOVER;" message on the subchannel. All processing should stop after this.

@restart

>Sends a "#RESTART;" message on the subchannel. The calling program is responsible for handling the details of restarting the game. Note that using GROD for the data dictionary makes this simple!

@restore

>Sends a "#RESTORE;" message on the subchannel. The calling program is responsible for handling the details of restoring the saved data.

@save

>Sends a "#SAVE;" message on the subchannel. The calling program is responsible for handling the details of saving data.

@undo

>Sends an "#UNDO;" message on the subchannel. The calling program is responsible for handling the details of undoing the last move.

@yorn(yes_script,no_script,error_script)

>Sends a "#YORN;" message (aka "Yes OR No") on the subchannel, indicating it would like a yes or no answer from the calling program. All three scripts are keys to saved scripts in the dictionary. If the answer can be identified as truthy, the "yes_script" is executed. If it can be identified as falsey, the "no_script" is executed. Otherwise "error_script" will be executed. The calling program is expected to call "HandleYorn(input, result)" with the answer.


<br>

## Public Interface

var twig = new Twig(IDictionary<string, string> dictionary)

>Create a new Twig object connected to an existing dictionary.

RunScript(string script, StringBuilder result)

>Runs the specified script and return any answers in result.

HandleYorn(string input, StringBuilder result)

>Handle a yes-or-no request. Input should be something understood to be "yes" or "no".

HandleAnswer(string input, StringBuilder result)

>Handle a request for an answer, which will be processed by an existing script.

int CompareKeys(string x, string y)

>String comparison function returning -1,0,1. Useful in "keys.Sort(CompareKeys);" to put keys in order. For keys with separating periods, any numeric sections are sorted numerically instead of alphabetically, so "1", "2", "10" instead of "1", "10", "2".

string PrettyScript(string script)

>Returns the script with line splitting and indenting for more readable code.

string Subchannel

>Property for sending information to the calling program. Special public constants are used to communicate.

string Help()

>Returns a syntax listing of all Twig commands.


<br>

## Debugging

Debugging is available in a limited form. It is for stepping through a script and figuring out issues. It steps through each statement and returns each result separately. It will step through each condition in an "@if" block but only at the top level, not for nested "@if"s. It does perform any changes on the data. The calling program starts with DebugScript() and then would call DebugNext() until done.

DebugScript(string script, StringBuilder result)

>Starts debugging the specified "script". If "script" doesn't start with "@" then it is used as a key to get the script value. Performs the first step and returns the result.

DebugNext(StringBuilder result)

>Performs the next step in "script" and returns the results. Returns "Debug done." if at the end.

bool DebugDone()

>Returns True when "script" has finished.