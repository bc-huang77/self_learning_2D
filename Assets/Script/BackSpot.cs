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
        transform.position = position; // ���ù��λ��
        spriteRenderer.enabled = true; // ��ʾ���
    }

    public Vector3 Hide()
    {
        spriteRenderer.enabled = false; // ���ع��
        Vector3 p = transform.position;
        transform.position = originalPosition;
        return p;
    }
}
