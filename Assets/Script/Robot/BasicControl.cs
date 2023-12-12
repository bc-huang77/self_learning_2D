using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicControl : MonoBehaviour
{
    //Basic
    public float moveSpeed = 5f; // 角色移动速度
    public float jumpForce = 5f; // 跳跃力量
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float faceRight = 1;
    private bool isGrounded; // 用于检测角色是否在地面上
    private bool canMove;
    private InputBuffer jumpBuffer;
    public float jumpBufferTime = 0.1f;

    //Shoot
    public float bulletSpeed = 2f;
    public GameObject bulletPrefab;
    private float lastShotTime;

    //Heat
    public GameObject heatUI;
    public float heatLimit = 100f;
    public float jumpCost = 20f;
    public float shootCost = 10f;
    public float reduceSpeed = 10f;
    public float overHeatReduceSpeed = 5f;
    private float heatUsed = 0;
    private bool overHeat;
    public Color overHeatColor;


    private enum States
    {
        Idle,
        Walking,
        Running,
        Jumping,
        // 添加其他状态...
    }
    private States states;

    void Start()
    {
        // Get Buffer component and initialize it
        jumpBuffer = GetComponent<InputBuffer>();
        jumpBuffer.bufferTime = jumpBufferTime;
        jumpBuffer.setCommand("Jump");

        rb = GetComponent<Rigidbody2D>(); // 获取Rigidbody2D组件
        animator = GetComponent<Animator>();//获取Animator组件
        spriteRenderer = GetComponent<SpriteRenderer>();
        states = States.Idle;
        canMove = true;
        lastShotTime = Time.time;
    }

    void Update() 
    {
        doMovement();
        doShoot();
        heatUIUpdate();
    }

    // 用于检测角色是否接触地面
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetTrigger("landing");
            animator.ResetTrigger("falling");
            animator.ResetTrigger("jumping"); 
            isGrounded = true; // 如果接触到的是“地面”对象，设置isGrounded为true
        }  
    }

    /*
    void OnCollisionExit2D(Collision2D collision)
    {*/

    private void doMovement()
    {
        if (canMove)
        {
            
            if (!overHeat)
            {
                // 检测跳跃按键（默认为“空格键”）
                if (jumpBuffer.output && isGrounded)
                {
                    states = States.Jumping;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce); // 添加垂直速度以实现跳跃
                    animator.SetTrigger("jumping");
                    animator.ResetTrigger("landing");
                    isGrounded = false;
                    heatUsed += jumpCost;
                }
                if (Input.GetButtonUp("Jump"))
                {
                    //animator.ResetTrigger("jumping");
                }
            }


            if (!isGrounded && rb.velocity.y < 0)
            {
                animator.SetTrigger("falling");
            }

            // 水平移动
            float moveInput = Input.GetAxis("Horizontal"); // 获取水平轴（A/D或左右箭头）输入
            if (moveInput != 0 && faceRight * moveInput < 0)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
                faceRight *= -1;
                //Debug.Log("FlipX");
            }
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y); // 设置水平速度
            animator.SetFloat("speed", Math.Abs(rb.velocity.x));



        }
    }

    private void doShoot()
    {
        if (!overHeat)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                animator.SetBool("shoot", true);
                lastShotTime = Time.time;
                Vector2 bias = faceRight == 1 ? new Vector2(0.55f, -0.15f) : new Vector2(-0.6f, -0.15f);
                GameObject bullet = Instantiate(bulletPrefab, rb.position + bias, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = faceRight * transform.right * bulletSpeed; // 设置子弹速度
                bullet.GetComponent<SpriteRenderer>().flipX = faceRight == 1 ? false : true;

                heatUsed += shootCost;
            }
        }

        if (Time.time - lastShotTime > 0.3)
        {
            animator.SetBool("shoot", false);
        }
    }

    public void jumpEnd()
    {
        animator.SetTrigger("falling");
    }
    
    public void shootEnd()
    {
        animator.SetBool("shoot", false);
    }

    public void moveLimit()
    {
        canMove = false;
    }
    
    public void removeMoveLimit()
    {
        canMove = true;
    }

    private void heatUIUpdate()
    {   
        if(heatUsed >= heatLimit)
        {
            overHeat = true;
            GetComponent<SpriteRenderer>().color = overHeatColor;
            heatUI.GetComponent<HeatUIControl>().UseOverHeatColor();
        }
        if (overHeat)
        {
            if(heatUsed < 1)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                overHeat = false;
                heatUI.GetComponent<HeatUIControl>().UseNormalColor();
            }
        }
        float rS = overHeat ? overHeatReduceSpeed : reduceSpeed;
        heatUsed = Math.Max(0, heatUsed - rS * Time.deltaTime);

        heatUI.GetComponent<HeatUIControl>().SetWidthPercentage(heatUsed / heatLimit);
    }
}
