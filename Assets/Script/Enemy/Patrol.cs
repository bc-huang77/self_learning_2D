using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] waypoints; // ·��������
    public float speed = 2f; // �ƶ��ٶ�

    private int waypointIndex = 0; // ��ǰĿ��·��������

    // Start is called before the first frame update
    void Start()
    {
        transform .position = waypoints[waypointIndex].position; // ��ʼλ��Ϊ��һ��·����
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        // ��ǰ·�����ƶ�
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, speed * Time.deltaTime);

        // ����Ƿ񵽴ﵱǰ·����
        if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) < 0.001f)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length; // �ƶ�����һ��·����
        }
    }
}
