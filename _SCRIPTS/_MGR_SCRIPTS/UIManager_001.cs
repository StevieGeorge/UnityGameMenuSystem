using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
    The UIManager controls the menus and HUD elements presented to the player.
    It does not send any instructions of any other kind; 
    the windows it presents should have those instructions built into their respective scripts


    Terms:
        "Settings" is used instead of "Options". Options are often parameters that can be empty, while settings are always set
        "UI" is used instead of "Window", "Menu", or "Screen", mainly for consistency 
*/
public class UIManager : MonoBehaviour
{
    public GameManager game;
    public GameObject canvas;
    public UIConfirmation uiLanding;
    public UIGameControl uiMainMenu;
    public UIGameControl uiPause;
    public UIGameControl uiRoundOver;
    public UIElement uiPlayerControls;
    public UIElement uiSettings;
    public UIConfirmation uiThanks;

    public List<UIElement> allElements;
    // Start is called before the first frame update
    void Start()
    {
        allElements = new List<UIElement>{
            uiLanding, 
            uiMainMenu, 
            uiPause, 
            uiRoundOver, 
            uiPlayerControls,
            uiSettings, 
            uiThanks};
        for(int i = 0; i < allElements.Count; i++)
        {    
            if(game && allElements[i])
            {
                allElements[i].game = game;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openLanding()
    {
        closeAll();
        if (uiLanding)
            uiLanding.gameObject.SetActive(true);
        else
            openMainMenu();
    }

    public void openMainMenu()
    {
        closeAll();
        uiMainMenu.gameObject.SetActive(true);
    }

    public void closeAll()
    {
        for (int i = 0; i < allElements.Count; i++)
        {
            if (allElements[i])
                allElements[i].gameObject.SetActive(false);
        }
    }

    public void pause()
    {
        closeAll();
        uiPause.gameObject.SetActive(true);
    }
    public void unpause()
    {
        closeAll();
        uiPlayerControls.game.gameObject.SetActive(true);
    }


    public void settings()
    {
        closeAll();
        uiSettings.gameObject.SetActive(true);
    }
    public void uiRoundStart()
    {
        closeAll();
        uiPlayerControls.gameObject.SetActive(true);
    }

    public void roundOver()
    {
        closeAll();
        uiRoundOver.gameObject.SetActive(true);
    }
    public void applicationQuit()
    {
        closeAll();

        //Was a thanks screen set and has a confirmEvent assigned? If not, just quit.
        if (uiThanks && uiThanks.confirmEvent != null)
        {
            uiThanks.gameObject.SetActive(true);
        }
        else
            Application.Quit();
    }
}
