using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//A confirm screen is an object with a single user input, such as a notification, splash page, or 'Thanks for playing". The unityEvent ConfirmEvent
public class UIConfirmation : UIElement
{
    public bool confirmed = false;
    public Button confirmButton;

    public bool confirmAnyInput = true;

    public UnityEvent confirmEvent;

    public TMP_Text messageText;
    // Start is called before the first frame update
    void Start()
    {
        if (confirmButton)
        {
            confirmButton.onClick.AddListener(confirm);
            confirmAnyInput = false;
        }
        if (messageText && confirmEvent == null)
        {
            messageText.text = "This confirmation window was not given a confirmation event. This window will never close.";
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkConfirmation();
        if (confirmed)
        {
            confirmEvent.Invoke();
        }
    }



    public void checkConfirmation()
    {
        if (confirmAnyInput && game.controls.anyInput())
        {
            confirmed = true;
        }
    }

    public void confirm()
    {
        confirmed = true;
    }

    public void quitApplication()
    {
        Application.Quit();
    }
}
