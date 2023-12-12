using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSpot : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(Vector3 position)
    {
        transform.position = position; // 设置光点位置
        spriteRenderer.enabled = true; // 显示光点
    }

    public Vector3 Hide()
    {
        spriteRenderer.enabled = false; // 隐藏光点
        Vector3 p = transform.position;
        transform.position = originalPosition;
        return p;
    }
}
