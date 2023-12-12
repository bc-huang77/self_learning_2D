using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpot : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float duration = 2.0f; // 光点持续时间
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
        spriteRenderer.enabled = false; // 隐藏光点
    }

    public void Show(Vector3 position)
    {
        transform.position = position; // 设置光点位置
        spriteRenderer.enabled = true; // 显示光点
        timer = duration; // 重置计时器
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (spriteRenderer.enabled)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // 如果机器人接触到光点
                Hide(); // 立即隐藏光点
                other.GetComponent<callFutureMe>().travelBack(transform.position);
            }
        }
    }
}
