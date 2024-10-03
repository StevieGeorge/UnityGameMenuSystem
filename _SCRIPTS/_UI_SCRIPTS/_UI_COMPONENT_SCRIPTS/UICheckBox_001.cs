using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
	A simple checkbox / pushbutton switch, with optional images and events for activating and deactivating
	It would be nice to use "(un)checked" instead of "(de)activated", but "checked" is already used in c#
*/

public class CheckBox : UIComponent
{
	public Button buttonCheckBox;
	public bool activated = false;

	// optional images to enable on activate and deactivate
	public Image imageActivated;
	public Image imageDeactivated;


	// optional events to invoke on activate and deactivate
	public UnityEvent eventActivated;
	public UnityEvent eventDeactivated;
	public UnityEvent eventChanged;

	void Start()
	{
		buttonCheckBox.onClick.AddListener(toggle);
	}
	
	public void toggle()
	{
		
		activated = !activated;

		//if images are assigned, swap the sprites as needed
		if (imageActivated)
			imageActivated.enabled = activated;

		if (imageDeactivated)
			imageDeactivated.enabled = !activated;

			
		if (eventChanged != null)
				eventChanged.Invoke();

		// fire events (if any)
		switch (activated)
		{
			case true:
				if (eventActivated != null)
					eventActivated.Invoke();
					break;
			case false:
				if (eventDeactivated != null)
					eventDeactivated.Invoke();
					break;
		}
	}
}
