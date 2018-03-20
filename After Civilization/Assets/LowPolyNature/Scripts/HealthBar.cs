using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public int Min;

    public int Max;

    private int mCurrentValue;

    private float mCurrentPercent;

	private RectTransform trans;
	private RawImage img;

    public void SetHealth(int health)
    {
        if(health != mCurrentValue)
        {
            if(Max - Min == 0)
            {
                mCurrentValue = 0;
                mCurrentPercent = 0;
            }
            else
            {
                mCurrentValue = health;
                mCurrentPercent = (float)mCurrentValue / (float)(Max - Min);
            }

			trans = GetComponent<RectTransform> ();
			img = GetComponent<RawImage> ();
			img.uvRect = new Rect (0, 0, mCurrentValue, 1);
			trans.sizeDelta = new Vector2(50f * mCurrentValue, 55.8f);
        }
    }

    public float CurrentPercent
    {
        get { return mCurrentPercent; }
    }

    public int CurrentValue
    {
        get { return mCurrentValue;  }
    }

}
