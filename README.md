# TwigScript - Simple scripting engine for games

TwigScript is a simple scripting engine which directly connects to a key/value dictionary. It has commands to get and set data and manipulate it in various ways. It is interpretive and not compiled, in order to give it greater flexibility.

TwigScript consists of functions starting with "@" with zero or more parameters defined for each. Parameter values can be strings or other functions returning strings. Strings are anything not starting with "@", and don't need quotes around them if they have no spaces or special symbols. Some functions expect the string parameters to be numbers or functions returning numbers, such as arithmetic functions.

Many of the built-in functions directly access the data from the key/value dictionary. "@get(mykey)" reads the key "mykey" and returns the value. "@set(mykey,myvalue)" sets that value in the dictionary. Other functions return text to the calling program, or execute more scripts.

TwigScript can be extended by creating new functions and adding them to the dictionary. They are no different than the built-in functions and are usable for any purpose.

TwigScript works great with GROD, a Game Resource Overlay Dictionary. This contains base values plus all changes in an overlay as the game is played, so changes can be saved and restored. See the GrodLibrary GitHub site for more details.