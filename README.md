# UnityGameMenuSystem

Introduction:"Begin with a finish product". It is very easy to start making a game, but even easier to never finish it. However even just a program that opens, says "Thanks for playing!" and closes can be considered a complete game, so this menu system is made to establish this baseline.

Description:
A menu system for presenting and changing settings for a game in Unity engine, with the ultimate goal of becoming game- and engine-independent. Currently an exercise in UX, orchestration, modularity, definitions of terms and code readability.

Current features:
- Main Menu: A central landing page with controls to start, quit, and adjust settings
- Options: An settings menu with functional controls to adjust settings such as resolution
- Pause: A window with controls to restart, quit, and adjust settings
- Game, Round, and Campaign management: a system of organizing game logic to allow starting, stopping, loading and pausing games without the need to modify the menu structure.

Features in development:
- Keybindings: A central point for adjusting keybindings and reading diverse input types.
- Language switcher: A single script to collect all text components in a scene and repopulate them with alternative text for a different langauge
- Saving and loading: A serialization system for saving and loading game states so that they are totally unaffected by the menu or application state.
