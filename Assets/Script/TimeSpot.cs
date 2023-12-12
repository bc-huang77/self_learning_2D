using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpot : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float duration = 2.0f; // ������ʱ��
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer.enabled)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Debug.Log("Game Overe");
                Hide ();
            }
        }
    }

    private void Hide()
    {
        spriteRenderer.enabled = false; // ���ع��
    }

    public void Show(Vector3 position)
    {
        transform.position = position; // ���ù��λ��
        spriteRenderer.enabled = true; // ��ʾ���
        timer = duration; // ���ü�ʱ��
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (spriteRenderer.enabled)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // ��������˽Ӵ������
                Hide(); // �������ع��
                other.GetComponent<callFutureMe>().travelBack(transform.position);
            }
        }
    }
}
