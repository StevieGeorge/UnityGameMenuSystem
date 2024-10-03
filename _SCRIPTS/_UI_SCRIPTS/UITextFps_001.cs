using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
    A simple Fps text displayer. Filters over five frames, a sweet spot chosen from testing
*/

public class UITextFps : MonoBehaviour
{
    public TMP_Text textFPS;
	List<float> frameTimes = new List<float>{0f,0f,0f,0f,0f,0f,0f,0f,0f,0f};
	int frameTimeIndex = 0;
 //   int frame = 0;

    public float fps = 0f;
    
    
    void OnEnable()
    {
        fps = 1f;
        frameTimes = new List<float>{1f,1f,1f,1f,1f,1f,1f,1f,1f,1f};
    }

    // Update is called once per frame
    void Update()
    {
//        frame++;
        frameTimeKeeping();
        textFPS.text = fps.ToString();
    }

    void frameTimeKeeping()
	{
		frameTimes[frameTimeIndex] = Time.unscaledDeltaTime;
		frameTimeIndex ++;
		if (frameTimeIndex >= frameTimes.Count)
			frameTimeIndex = 0;
		
		float totalFrameTimes = 0.00001f;
        for (int i = 0; i < frameTimes.Count; i++)
		{
			totalFrameTimes += frameTimes[i];
		}
		fps = Mathf.Round(1f / (totalFrameTimes / (float)frameTimes.Count));
	}

    public void toggleObject()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
