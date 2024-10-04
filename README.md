# UnityGameMenuSystem
A scene-free menu system for presenting, saving, loading, and changing settings for a game in Unity engine, with the ultimate goal of becoming game- and engine-independent. Currently an exercise in UX, orchestration, and code readability.

-----
#Introduction
 "Always have a complete product". It is easier to start a project than to finish it. However even just a program that opens, says "Thanks for playing!" and closes can be considered a complete game, so this menu system is made to establish this baseline.


------
#Features
- GameControlMenu: A universal Main Menu, Pause, or Game Over screen. Contains optional controls to start, restart, quit, and adjust settings. Just add buttons as desired.
- ConfirmationScreen: A universal Welcome, News, Notification and Thanks for Playing screen.
- OptionsMenu: An settings menu with functional controls to adjust settings such as resolution
- Game, Round, and Campaign management: a system of organizing game logic to allow starting, stopping, loading and pausing games without interdependence with the menu framework.

------
#Requirements

This system imposes the following:
- All of the information related to the active-runnng game is contained in a single class called a Round.
- The update loop of the game is explicitly called in Round.Update.
- For game movements that otherwise relied on Time.deltaTime, instead they use Round.frameTime.

As a result, the following is achieved:
- The game state can be saved at any moment by serializing the Round. Its saved state will be unaffected by the graphics settings, UI configuration, etc.
- The game logic can process at any chosen frame rate, including manually step-based
- The game logic can be paused without the need to freeze Time, which would freeze UI animations. Note consequently that the default unity Animators will not pause, however that will only break a game that is using Animators to drive game logic, which is an unsafe practice in the first place.
- Frame times will be consistent across all elements which access it, unlike Time.deltaTime which can yield different values even within the same script.

The system was designed to be used in the following way (but does not impose it):
 - The Round contains all of the variables needed to present the game on screen, and every frame is just reading the information and rendering the state. The engine behavior itself does not factor.
 - All objects are accessible through the hierarchy of interconnected scripts, reducing the need for expensive methods like GameObject.Find and GetComponent.
 


------
#Installation:
- Import the .unitypackage into your project. (Note: If you changed the default Canvas settings, you will need to change them again after the import).
- Alternatively, attach all the scripts from _SCRIPTS/_MGR_SCRIPTS to a single GameObject in your scene, and link them together. 
  - Create a Main menu canvas object, attach the GameControlMenu script, and link the script to a Quit button on the menu.
  - Create a "Thanks for Playing" canvas object, attach the ConfirmationScreen script, and set its event to quitApplication.
  - Optionally add additional buttons and menus to fill in the other fields in the UIManager as desired.


------
#Features for future releases
- Keybindings: A central point for adjusting keybindings and reading diverse input types.
- Language switcher: A single script to collect all text components in a scene and repopulate them with alternative text for a different langauge.
- Saving and loading: A serialization system for saving and loading game states so that they are totally unaffected by the menu or application state.


------
#Development philosphy
- Be as game-independent as possible. Currently designed to support both real-time and turn-based games, and can be implemented as a GUI for console programs.
 
-----
#Lessons Learned

Being an exercise, this project was about learning and finishing. In the process, the following was learned:
- C# Events. The method-based setup used in this project allows someone taking over the code to easily follow the references and method calls, tracing the entire UX flow, however redesigning the system around events might allow everything to collapse down enough to be explained in one document or comment block, rather than needing to be traceable throughout the code itself.
- Unity canvas "dirtying". In the time since creating this, I became aware of Unity's very aggressive tendency to mark all canvas elements for re-rendering whenever a user-visible paramter is changed on any active gameobject. In the future, the canvas could be split into multiple elements to mitigate this, hopefully finding a solution that is highly optimized without being too comlpex and cumbersome.
- Scope creep and inherited code. The goal was to make a complete product and deliver it quickly, but the decision was made to include a graphics settings menu which I'd been developing and adapting throughout numerous projects. While it worked well in those, it still deserved a rewrite to fit with the "standardized and readable" nature of this one. That secondary addition amounted to scope creep which took this project away from being wholly-concieved and "inherently complete".
