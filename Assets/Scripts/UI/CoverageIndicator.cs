using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CoverageIndicator : MonoBehaviour
{
    private Image IndicatorBG;
    private Image IndicatorFill;

    private Text IndicatorLabel;

    private float percentage = 0;

    public void Awake()
    {
        IndicatorFill = transform.Find("Fill").GetComponent<Image>();
        IndicatorLabel = transform.Find("Text").GetComponent<Text>();

        SetPercentage(percentage);
    }

    public void SetFillColor(Color color)
    {
        IndicatorFill.color = color;
    }

    public void SetPercentage(float perc)
    {
        perc = math.clamp(perc, 0f, 1f);

        IndicatorFill.rectTransform.localScale = new Vector3(perc, perc, perc);
        IndicatorLabel.text = (perc * 100f).ToString("#") + "%";
        percentage = perc;
    }
}
