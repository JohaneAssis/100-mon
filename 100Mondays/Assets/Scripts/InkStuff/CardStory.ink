	-> Welcome_To_Office

=== Welcome_To_Office ===
Hello and welcome to your office. 
	-> First_Choice_Options

=== First_Choice_Options ===
Are you ready to start working?
* Yes[.], I think I'm ready.
	-> Yes_Ready
* No[.], I need a moment to gather my thoughts.
	-> No_Ready

= Yes_Ready
Lets get started.
* [Start]
	-> End_Game

= No_Ready
Don't worry, I'll wait.
* [Finally Start]
	-> End_Game

= End_Game
Just kidding, you're fired.
	-> END