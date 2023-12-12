using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatUIControl : MonoBehaviour
{
    public RectTransform targetUIElement; // Ŀ�� UI Ԫ�ص� RectTransform
    public float maxWidth = 70f; // �����ֵ
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

    // ���ݸ����İٷֱȵ��� UI ��ȵķ���
    public void SetWidthPercentage(float percentage)
    {
        if (targetUIElement != null)
        {
            // �����¿��
            float newWidth = maxWidth * Mathf.Clamp01(percentage); // ȷ���ٷֱ��� 0 �� 1 ֮��
            // �����¿��
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
