using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizePrefab : MonoBehaviour
{	public List<randomEnabled> randomEnabledObjects = new List<randomEnabled>();
	public List<randomScaled> randomScaledObjects = new List<randomScaled>();

	public List<randomColored> randomColorImages = new List<randomColored>();

	[System.Serializable]
	public class randomEnabled
	{
		//maximum number of enabled objects. Set negative for no limit.
		public int maxEnabled = 1;
		public float enableChance = 1f;
		public List<GameObject> possibleObjects = new List<GameObject>();
		public void enableRandom()
		{
			/*
			randomizes the order of objects, and roll chance for only the first maxEnabled of them
			this way every object has an equal chance of being enabled and there is no bias toward one end of the list
			*/

			int enableable = maxEnabled;
			var shuffledList = possibleObjects.OrderBy( x => Random.value ).ToList( ); //Don't fear Linq!
			if (maxEnabled < 0)
				enableable = int.MaxValue;
			
			for (int i = 0; i < shuffledList.Count; i++)
			{
				shuffledList[i].SetActive(false);
				if (i >= enableable)
					break;
				else if(Random.value <= enableChance) //chance of 1.0f should always return true. Chance of 0f is not relevant.
				{
					enableable --;
					shuffledList[i].SetActive(true);
				}
			}
		}
	}
	[System.Serializable]
	public class randomScaled
	{
		public Vector3 scaleMin = new Vector3(1f,1f,1f);
		public Vector3 scaleMax = new Vector3(1f,1f,1f);
		public List<GameObject> scaleObjects = new List<GameObject>();
		public void scaleRandom()
		{
			/*
			scales all objects in list by the random scale value. preserves any scaling originally applied.
			*/
			Vector3 scale = Vector3.Lerp(scaleMin, scaleMax, Random.value);
			for (int i = 0; i < scaleObjects.Count; i++)
			{
				scaleObjects[i].transform.localScale = Vector3.Scale(scaleObjects[i].transform.localScale, scale);
			}
		}
	}
	[System.Serializable]
	public class randomColored
	{
		//choose a random color between any two adjacent colors?
		public bool lerpColors = false;
		public Image imageToColor;
		public List<Color> possibleColors = new List<Color>();
		public void colorRandom()
		{
			//random color from the list
			int rIndex = Random.Range(0, possibleColors.Count);
			Color col = possibleColors[rIndex];

			if (!lerpColors)
			{
				imageToColor.color = col;
			}
			else
			{
				//next color in the list
				int nIndex = (rIndex + 1) % possibleColors.Count;
				Color nextCol = possibleColors[nIndex];

				imageToColor.color = Color.Lerp(col, nextCol, Random.value);
			}
		}
	}

    // Start is called before the first frame update
    void Start()
    {
		//seed the random with the instance ID. That way results are consistent
		Random.InitState(this.GetInstanceID());
		for (int i = 0; i < randomEnabledObjects.Count; i++)
			randomEnabledObjects[i].enableRandom();

		for (int i = 0; i < randomScaledObjects.Count; i++)
			randomScaledObjects[i].scaleRandom();

		for (int i = 0; i < randomColorImages.Count; i++)
			randomColorImages[i].colorRandom();
        
    }
}
