using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
	A script to modify the unity in-game graphics settings for performance.
	Note: "GraphicsSettings" already exists in unity.
*/
public class GraphicsSettingsManager : MonoBehaviour
{
	

	public DynamicResolutionAgent dynaRes;
	public bool dynaScaling = true;
	public float fps = 0f;
	public float fpsTarget = 60f;
	public int fpsLimit = 0;
	List<float> frameTimes = new List<float>();
	public int frameAvgIndex = 0;
	public int fpsIndex = 0;
	public int framesToAvg = 5;
	[SerializeField] float scaleFactor;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < framesToAvg; i++)
		{
			frameTimes.Add(1f / (fpsTarget * 1.1f));
		}
    }

    // Update is called once per frame
    void Update()
    {
		frameTimeKeeping();
		if (dynaScaling)
		{
			dynaResUpdate();
		}
    }
	
	void frameTimeKeeping()
	{
		frameTimes[frameAvgIndex] = Time.unscaledDeltaTime;
		frameAvgIndex ++;
		if (frameAvgIndex >= frameTimes.Count)
		{
			frameAvgIndex = 0;
		}
		fpsIndex ++;
		if (fpsIndex >= fps)
		{
			fpsIndex = 0;
		}
		
		float totalFrameTimes = 0f;
        for (int i = 0; i < frameTimes.Count; i++)
		{
			totalFrameTimes += frameTimes[i];
		}
		fps = Mathf.Round(100f / (totalFrameTimes / (float)frameTimes.Count)) * 0.01f;
	}
	
	void dynaResUpdate()
	{
		if (fpsIndex == 0)
		{
			if (fps < fpsTarget * 0.97f)
			{
				dynaRes.downscale();
			}
			else if (fps > fpsTarget * 0.99f)
			{
				dynaRes.upscale();
			}
		}
		scaleFactor = dynaRes.scaleDivisor;
	}
	
	/*
	public void limitFps()
	{
		Application.targetFrameRate = (int)fpsLimit;
		if ((int)fpsLimit > 0)
		{
			QualitySettings.vSyncCount = 0; 
		}
		else
		{
			QualitySettings.vSyncCount = 1; 
		}
	}
	*/
	
	public void setFpsTarget(int target)
	{
		fpsTarget = (float)target;
		if (target <= 0)
		{
			dynaRes.setScale(1.0f);
			dynaScaling = false;
		}
	}
	
	public void setFpsLimit(int limit)
	{
		limit = Mathf.Abs(limit);
		int targetFrameRate = limit;
		Application.targetFrameRate = targetFrameRate;
		if (limit > 0)
			QualitySettings.vSyncCount = 0; 
		else
			QualitySettings.vSyncCount = 1; 
	}
}
