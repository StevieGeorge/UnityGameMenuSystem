using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;

public class DynamicResolutionAgent : MonoBehaviour
{
    public TMP_Text screenText;

    FrameTiming[] frameTimings = new FrameTiming[3];

    public float maxResolutionWidthScale = 1.0f;
    public float maxResolutionHeightScale = 1.0f;
    public float minResolutionWidthScale = 0.5f;
    public float minResolutionHeightScale = 0.5f;
    public float scaleWidthIncrement = 0.1f;
    public float scaleHeightIncrement = 0.1f;
	public float scaleDivisor = 1f;

    float m_widthScale = 1.0f;
    float m_heightScale = 1.0f;
	
	bool scaleUp = false;
	bool scaleDown = false;
	

    // Variables for dynamic resolution algorithm that persist across frames
    uint m_frameCount = 0;

    const uint kNumFrameTimings = 2;

    double m_gpuFrameTime;
    double m_cpuFrameTime;


    // Use this for initialization
    void Start()
    {

        int rezWidth = (int)Mathf.Ceil(ScalableBufferManager.widthScaleFactor * Screen.currentResolution.width);
        int rezHeight = (int)Mathf.Ceil(ScalableBufferManager.heightScaleFactor * Screen.currentResolution.height);
        if (screenText)
            screenText.text = string.Format("Scale: {0:F3}x{1:F3}\nResolution: {2}x{3}\n",
                m_widthScale,
                m_heightScale,
                rezWidth,
                rezHeight);
    }

    // Update is called once per frame
    void Update()
    {
        float oldWidthScale = m_widthScale;
        float oldHeightScale = m_heightScale;

        if (scaleDown)
        {
			scaleDown = false;
			scaleDivisor ++;
        }
		else if (scaleUp)
        {
			scaleUp = false;
			scaleDivisor --;
			scaleDivisor = Mathf.Max(1f, scaleDivisor);
        }
		
		m_heightScale = Mathf.Max(minResolutionHeightScale, 1f / scaleDivisor);
		m_widthScale = Mathf.Max(minResolutionWidthScale, 1f / scaleDivisor);

        if (m_widthScale != oldWidthScale || m_heightScale != oldHeightScale)
        {
            ScalableBufferManager.ResizeBuffers(m_widthScale, m_heightScale);
        }
        DetermineResolution();
        int rezWidth = (int)Mathf.Ceil(ScalableBufferManager.widthScaleFactor * Screen.currentResolution.width);
        int rezHeight = (int)Mathf.Ceil(ScalableBufferManager.heightScaleFactor * Screen.currentResolution.height);
        if (screenText)
            screenText.text = string.Format("Scale: {0:F3}x{1:F3}\nResolution: {2}x{3}\nScaleFactor: {4:F3}x{5:F3}\nGPU: {6:F3} CPU: {7:F3}",
                m_widthScale,
                m_heightScale,
                rezWidth,
                rezHeight,
                ScalableBufferManager.widthScaleFactor,
                ScalableBufferManager.heightScaleFactor,
                m_gpuFrameTime,
                m_cpuFrameTime);
			
	    var urp = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
		            urp.renderScale = m_widthScale;
    }
	
	public void downscale()
	{
	//	Debug.Log("scaling Down");
		scaleDown = true;
	}
	
	public void upscale()
	{
	//	Debug.Log("scaling Up");
		scaleUp = true;
	}
	public void setScale(float scaleToSet)
	{
		Debug.Log("setting scale: " + scaleToSet);
		scaleDivisor = 1.0f / scaleToSet;
		m_heightScale = Mathf.Max(minResolutionHeightScale, scaleToSet);
		m_widthScale = Mathf.Max(minResolutionWidthScale, scaleToSet);

	}

    // Estimate the next frame time and update the resolution scale if necessary.
    private void DetermineResolution()
    {
        ++m_frameCount;
        if (m_frameCount <= kNumFrameTimings)
        {
            return;
        }
        FrameTimingManager.CaptureFrameTimings();
        FrameTimingManager.GetLatestTimings(kNumFrameTimings, frameTimings);
        if (frameTimings.Length < kNumFrameTimings)
        {
            Debug.LogFormat("Skipping frame {0}, didn't get enough frame timings.",
                m_frameCount);

            return;
        }

        m_gpuFrameTime = (double)frameTimings[0].gpuFrameTime;
        m_cpuFrameTime = (double)frameTimings[0].cpuFrameTime;
    }
}