using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class BaseStat
{
    [SerializeField] private float curr;
    [SerializeField] private float max;
    [SerializeField] private bool level;
    public Image statImage;
    public Text statText;
    public Image statImageUI;
    public Text statTextUI;


    public float Curr
    {
        get { return curr; }
        set
        {
            if (value < 0)
                curr = 0;
            else if (value >= max)
                curr = max;
            else
                curr = value;
        }
    }

    public float Max
    {
        get { return max; }
        set { max = value; }
    }

    public float Percent(bool t)
    {
        return t ? curr - Mathf.FloorToInt(curr) : curr / max;
    }

    public override string ToString()
    {
        if(level)
            return string.Format("{0}", Mathf.FloorToInt(curr).ToString("N0"));
        else
            return string.Format("{0} / {1}", curr.ToString("N0"), max);
    }

    public void UpdateStat()
    {
        if (statImage != null)
        {
            statImage.fillAmount = Percent(false);
        }
        if (statText != null)
        {
            statText.text = ToString();
        }

        if (statImageUI != null)
        {
            statImageUI.fillAmount = Percent(false);
        }
        if (statTextUI != null)
        {
            statTextUI.text = ToString();
        }



    }

}
