using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

/*
    A simple class for UI windows in the presentation framework. Contains a link back to the game manager
    This class is planned to have methods for moving and animating windows around as well as keeping them onscreen
*/

public class UIElement : MonoBehaviour
{

    public GameManager game;
    public UnityEvent onCloseUI;

    public Button closeButton;

    public Color textColor = Color.white;
    public bool setTextColor = false;
    public List<TMP_Text> allTextObjectsInChildren = new List<TMP_Text>();
    // Start is called before the first frame update
    public void Start()
    {
        if (game == null)
        {
            game = GameObject.Find("GAME_MANAGER").GetComponent<GameManager>();
        }

        if (setTextColor)
            setAllTextColor(textColor);

        if (closeButton)
            closeButton.onClick.AddListener(closeThisUi);

    }

    void OnValidate()
    {
        allTextObjectsInChildren = gameObject.GetComponentsInChildren<TMP_Text>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closeThisUi()
    {
        if (onCloseUI != null)
            onCloseUI.Invoke();
        gameObject.SetActive(false);
    }

    public void setAllTextColor(Color col)
    {
        for (int i = 0; i < allTextObjectsInChildren.Count; i++)
            allTextObjectsInChildren[i].color = col;
    }
}
