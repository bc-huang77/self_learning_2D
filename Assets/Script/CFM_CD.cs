using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CFM_CD : MonoBehaviour
{
    public Text cooldownText; // 引用UI Text组件
    public float cooldownTime = 5f; // 冷却时间总长
    private float cooldownTimer; // 冷却计时器

    void Start()
    {
        cooldownTimer = 0;
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownText.text = cooldownTimer.ToString("F1"); // 显示一位小数
        }
        else
        {
            cooldownText.text = "Ready!"; // 冷却完成
        }
    }

    public bool isReady()
    {
        return cooldownTimer <= 0;
    }


    public void ResetCooldown()
    {
        cooldownTimer = cooldownTime; // 重置冷却计时器
    }
}
