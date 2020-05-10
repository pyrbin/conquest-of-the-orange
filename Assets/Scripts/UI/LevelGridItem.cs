using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGridItem : MonoBehaviour
{
    public int Id;
    public Color CompleteColor = Color.green;

    private Image ItemFill;
    private Image ItemBorder;

    private bool completed = false;
    private Color originalBorderColor = Color.white;

    public void Awake()
    {
        ItemFill = transform.Find("Fill").GetComponent<Image>();
        ItemBorder = transform.Find("Border").GetComponent<Image>();

        originalBorderColor = ItemBorder.color;
        SetActive(false);
    }

    public void SetActive(bool status)
    {
        if (ItemFill == null || ItemBorder == null)
        {
            ItemFill = transform.Find("Fill").GetComponent<Image>();
            ItemBorder = transform.Find("Border").GetComponent<Image>();
        }

        if (status)
        {
            ItemBorder.color = CompleteColor;
        }
        else
        {
            ItemBorder.color = originalBorderColor;
        }
    }

    public void SetCompleteColor(Color color)
    {
        CompleteColor = color;
    }

    public void SetComplete()
    {
        ItemFill.color = CompleteColor;
        completed = true;
    }
}
