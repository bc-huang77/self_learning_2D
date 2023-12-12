using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatUIControl : MonoBehaviour
{
    public RectTransform targetUIElement; // 目标 UI 元素的 RectTransform
    public float maxWidth = 70f; // 最大宽度值
    RawImage image;
    public Color normalColor;
    public Color overHearColor;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RawImage>();
        SetWidthPercentage(0);
        UseNormalColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 根据给定的百分比调整 UI 宽度的方法
    public void SetWidthPercentage(float percentage)
    {
        if (targetUIElement != null)
        {
            // 计算新宽度
            float newWidth = maxWidth * Mathf.Clamp01(percentage); // 确保百分比在 0 到 1 之间
            // 设置新宽度
            targetUIElement.sizeDelta = new Vector2(newWidth, targetUIElement.sizeDelta.y);
        }
    }
    
    public void UseOverHeatColor()
    {
        image.color = overHearColor;
    }
    public void UseNormalColor()
    {
        image.color = normalColor;
    }
}
