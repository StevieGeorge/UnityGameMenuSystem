using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The GameManager is the receiver of general commands that affect the overal UX state, such as pausing, new rounds, and quitting.
    Due to the vagueness of the word "game", it is generally avoided except to refer to overall UI/UX.
    A game session that starts with a "play now" button and ends with a "game over" screen is a Round.
    "Level" and "Stage" have a similar vagueness problem. See notes in StageManager

    The GameManager is a central hub for all of the manager scripts. 
    When a script needs to get information from a manager, it navigates through GameManager

*/


public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public RoundManager roundManager;
    public Stage stage {
        get { return (roundManager.round.stage); }
        set { roundManager.round.stage = value; }
    }
    public bool paused {
        get { return (roundManager.paused); }
    }

    public ControlManagerUnity controls;

    // Start is called before the first frame update
    void Start()
    {
        uiManager.game = this;
        roundManager.game = this;
        controls.game = this;
 //       state = GameState.InMenu;
        openLanding();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void openLanding()
    {
        uiManager.openLanding();
    }

    public void mainMenu()
    {
        uiManager.openMainMenu();
    }


    public void exitToMainMenu()
    {
        roundOver();
    }

    public void newRound()
    {
        roundManager.newRound();
        uiManager.uiRoundStart();
    }

    public void pause()
    {
        roundManager.pause();
        uiManager.pause();
    }

    public void unpause()
    {
        roundManager.unpause();
        uiManager.unpause();
    }
    public void settings()
    {
        roundManager.pause();
        uiManager.settings();
    }

    public void roundOver()
    {
        uiManager.roundOver();
    }

    public void quit()
    {
        uiManager.applicationQuit();
    }
}
