using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    GameControlMenu is as close as possible to a catch-all class for:
        Main Menu
        Pause Menu
        etc.
    It always has a Settings / Options button,
    It always has a Quit Game button,
    and it optionally has more functions
*/

public class UIGameControl : UIElement
{

    public Button buttonMainMenu;
    public Button buttonNewRound;
    public Button buttonUnpause;
    public Button buttonSettings;
    public Button buttonQuitGame;

    
    // Start is called before the first frame update
    public void Start()
    {
        if (buttonMainMenu)
        {
            buttonMainMenu.onClick.AddListener(game.mainMenu);
        }
        if (buttonNewRound)
        {
            buttonNewRound.onClick.AddListener(game.newRound);
        }
        if (buttonUnpause)
        {
            buttonUnpause.onClick.AddListener(game.unpause);
        }
        if (buttonSettings)
        {
            buttonSettings.onClick.AddListener(game.settings);
        }
        if (buttonQuitGame)
        {
            buttonQuitGame.onClick.AddListener(game.quit);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
