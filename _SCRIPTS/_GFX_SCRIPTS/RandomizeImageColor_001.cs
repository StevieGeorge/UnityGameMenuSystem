using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeImageColor : MonoBehaviour
{

    //speed to cycle between colors, seconds between each step. 0 = do not cycle
    public float cyclingSpeed = 0f;
	public List<randomColored> randomColorImages = new List<randomColored>();


    void OnValidate()
    {

		for (int i = 0; i < randomColorImages.Count; i++)
			randomColorImages[i].colorRandom();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (cyclingSpeed != 0f)
        {
            cycle();
        }
    }


    void cycle()
    {
		for (int i = 0; i < randomColorImages.Count; i++)
        {
            if (randomColorImages[i].lerpColors)
    			randomColorImages[i].lerp += Time.deltaTime / cyclingSpeed;
            randomColorImages[i].lerpColor();
        }
    }

    
	[System.Serializable]
	public class randomColored
	{
		public bool lerpColors = false;
		public Image imageToColor;
		public List<Color> possibleColors = new List<Color>();
        [HideInInspector]
        public float lerp = 0f;
        [HideInInspector]
        public int indexColor = 0;

        public void lerpColor()
        {
            if (lerp > 1f)
            {
                lerp -= 1f;
                indexColor = nextIndexColor();
            }

            Color col = possibleColors[indexColor];
			Color nextCol = possibleColors[nextIndexColor()];
			imageToColor.color = Color.Lerp(col, nextCol, lerp);
        
            
        }
		//choose a random color between any two adjacent colors?
		public void colorRandom()
		{
            if (imageToColor && possibleColors.Count > 0)
            {
			    //random color from the list
			    indexColor = Random.Range(0, possibleColors.Count);
			    Color col = possibleColors[indexColor];

			    if (!lerpColors)
			    	imageToColor.color = col;
			    else
			    {
                    lerp = Random.value;
                    lerpColor();
			    }
            }
		}

        int nextIndexColor() // "Just use a modulus!" they said. I replied, "No! Readability!"
        {
            int next = indexColor + 1;
            if (next >= possibleColors.Count)
                next -= possibleColors.Count;
            return next;
        }


        
	}
}
