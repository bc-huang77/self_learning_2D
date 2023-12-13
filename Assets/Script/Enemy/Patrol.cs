using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] waypoints; // 路径点数组
    public float speed = 2f; // 移动速度
    public bool moveable = true;  
    private int waypointIndex = 0; // 当前目标路径点索引

    // Start is called before the first frame update
    void Start()
    {
        transform .position = waypoints[waypointIndex].position; // 初始位置为第一个路径点
    }

    // Update is called once per frame
    void Update()
    {
        if (moveable)
        {
            MoveTowardsWaypoint();
        }
        
    }

    void MoveTowardsWaypoint()
    {
        // 向当前路径点移动
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, speed * Time.deltaTime);

        // 检测是否到达当前路径点
        if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) < 0.001f)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length; // 移动到下一个路径点
        }
    }

    public Vector3 HotCircleFrozen()
    {
        moveable = false;
        return waypoints[waypointIndex].position;
    }
}
