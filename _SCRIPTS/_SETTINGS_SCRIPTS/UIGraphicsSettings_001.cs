using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
	This script was written long ago and is terrible, 
	however it works, and has some features I can't find anywhere else, 
	so it's implented here to be improved with future versions

	To use the options window alone without UIElement and by extension GameManager, just change the base class here from UIElement to MonoBehaviour
*/

public class uiGraphicsSettings : UIElement
{
	
	public List<int> fpsOptions = new List<int> {0, 18, 24, 30, 48, 60, 75, 120, 144};
//	public List<string> fpsOptions = new List<string> {"None", "18", "24", "30", "48", "60", "75", "120", "144"};
	public int fpsTargetOptionSetting = 3;
	public int fpsLimitOptionSetting = 0;

	public GraphicsSettingsManager graphicsSettingsManager;
	public GameObject optionsWindow;
	public TMP_Text fpsText;
	public TMP_Text fpsTextFancy;
	public TMP_Text fpsTargetText;
	public TMP_Text fpsLimitText;
	public TMP_InputField fpsTargetInputField;
	public TMP_InputField fpsLimitInputField;
	public bool showFps = true;
	public bool useFpsTarget = false;
	
    // Start is called before the first frame update
    void Start()
    {
		base.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
		/*
		if (showFps && fpsText)
			fpsText.text = graphicsSettingsManager.fps.ToString();
			*/
    }
	
	public void toggleShowFps()
	{
		showFps = !showFps;
		fpsText.gameObject.SetActive(showFps);
	}
	
	public void toggleFpsTextFancy()
	{
		fpsTextFancy.gameObject.SetActive(!fpsTextFancy.gameObject.activeSelf);
	}
	public void toggleFpsTarget()
	{
		useFpsTarget = !useFpsTarget;
		fpsTargetText.gameObject.SetActive(useFpsTarget);
		graphicsSettingsManager.dynaScaling = useFpsTarget;
		if (useFpsTarget)
			graphicsSettingsManager.dynaRes.setScale(1.0f);
	}
	
	
	
	public void toggleOptionsWindow()
	{
		optionsWindow.SetActive(optionsWindow.gameObject.activeSelf);
	}
	
	public void increaseFPSTarget()
	{
		Debug.Log("increasing FPS Target");
		fpsTargetOptionSetting++;
		if (fpsTargetOptionSetting <= fpsOptions.Count)
		   	fpsTargetOptionSetting = 0;
		int target = fpsOptions[fpsTargetOptionSetting % fpsOptions.Count];

		if (target == 0)
			fpsTargetText.text = "None";
		else
			fpsTargetText.text = target.ToString();
		graphicsSettingsManager.setFpsTarget(target);
	}
	
	public void decreaseFPSTarget()
	{
		Debug.Log("decreasing FPS target");
		fpsTargetOptionSetting--;
		if (fpsTargetOptionSetting < 0)
			fpsTargetOptionSetting = fpsOptions.Count - 1;
		int target = fpsOptions[fpsTargetOptionSetting % fpsOptions.Count];
		if (target <= 0)
			fpsTargetText.text = "None";
		else
			fpsTargetText.text = target.ToString();
		graphicsSettingsManager.setFpsTarget(target);
	}
	
	public void increaseFPSLimit()
	{
		
		Debug.Log("increasing FPS Limit");
		fpsLimitOptionSetting++;
		int limit = fpsOptions[fpsLimitOptionSetting % fpsOptions.Count];

		if (limit == 0)
			fpsLimitText.text = "None";
		else
			fpsLimitText.text = limit.ToString();
		graphicsSettingsManager.setFpsLimit(limit);
	}
	
	public void decreaseFPSLimit()
	{
		Debug.Log("decreasing FPS Limit");
		fpsLimitOptionSetting--;
		if (fpsLimitOptionSetting < 0)
			fpsLimitOptionSetting = fpsOptions.Count - 1;
		int limit = fpsOptions[fpsLimitOptionSetting % fpsOptions.Count];
		if (limit <= 0)
			fpsLimitText.text = "None";
		else
			fpsLimitText.text = limit.ToString();
		graphicsSettingsManager.setFpsLimit(limit);
	}
}
