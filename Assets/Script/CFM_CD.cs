using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CFM_CD : MonoBehaviour
{
    public Text cooldownText; // ����UI Text���
    public float cooldownTime = 5f; // ��ȴʱ���ܳ�
    private float cooldownTimer; // ��ȴ��ʱ��

    void Start()
    {
        cooldownTimer = 0;
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownText.text = cooldownTimer.ToString("F1"); // ��ʾһλС��
        }
        else
        {
            cooldownText.text = "Ready!"; // ��ȴ���
        }
    }

    public bool isReady()
    {
        return cooldownTimer <= 0;
    }


    public void ResetCooldown()
    {
        cooldownTimer = cooldownTime; // ������ȴ��ʱ��
    }
}
