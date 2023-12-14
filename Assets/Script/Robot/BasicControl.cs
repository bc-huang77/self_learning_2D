using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

//Merge Test
public class BasicControl : MonoBehaviour
{
    //Basic
    public float moveSpeed = 5f; // ½ÇÉ«ÒÆ¶¯ËÙ¶È
    public float jumpForce = 5f; // ÌøÔ¾Á¦Á¿
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float faceRight = 1;
    private bool isGrounded; // ÓÃÓÚ¼ì²â½ÇÉ«ÊÇ·ñÔÚµØÃæÉÏ
    private bool canMove;
    private InputBuffer jumpBuffer;
    public float jumpBufferTime = 0.1f;
    public LayerMask groundLayer; // 用于过滤射线检测的 Layer
    public float groundCheckDistance = 1f; // 射线检测的距离
    public bool jumpable = true;

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
    public float hurtProtectTime = 0.5f;
    private float lastHurtTime = 0f;


    private enum States
    {
        Idle,
        Walking,
        Running,
        Jumping,
        // Ìí¼ÓÆäËû×´Ì¬...
    }
    private States states;

    void Start()
    {
        // Get Buffer component and initialize it
        jumpBuffer = GetComponent<InputBuffer>();
        jumpBuffer.bufferTime = jumpBufferTime;
        jumpBuffer.setCommand("Jump");

        rb = GetComponent<Rigidbody2D>(); // »ñÈ¡Rigidbody2D×é¼þ
        animator = GetComponent<Animator>();//»ñÈ¡Animator×é¼þ
        spriteRenderer = GetComponent<SpriteRenderer>();
        states = States.Idle;
        canMove = true;
        lastShotTime = Time.time;
    }

    void Update() 
    {
        CheckGround();  
        doShoot();
        heatUIUpdate();
    }

    private void FixedUpdate()
    {
        doMovement();
    }

    void CheckGround()
    {
        // 射线的起点为角色的位置，方向向下
        Vector2 position = transform.position + new Vector3(0.2f, 0f, 0f);
        Vector2 position2 = transform.position - new Vector3(0.2f, 0f, 0f);
        Vector2 direction = Vector2.down;
        

        RaycastHit2D hit = Physics2D.Raycast(position, direction, groundCheckDistance, groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(position2, direction, groundCheckDistance, groundLayer);
        jumpable = false;
        
        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            jumpable = true;
            animator.SetTrigger("landing");
        }
        else if (hit2.collider != null && hit2.collider.CompareTag("Ground"))
        {
            jumpable = true;
            animator.SetTrigger("landing");
        }
        else
        {
            animator.ResetTrigger("landing");
        }
        
        UnityEngine.Debug.DrawRay(position, direction * groundCheckDistance, Color.red);
        UnityEngine.Debug.DrawRay(position2, direction * groundCheckDistance, Color.red);
        
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //animator.SetTrigger("landing");
            animator.ResetTrigger("falling");
            animator.ResetTrigger("jumping"); 
            isGrounded = true; // Èç¹û½Ó´¥µ½µÄÊÇ¡°µØÃæ¡±¶ÔÏó£¬ÉèÖÃisGroundedÎªtrue
            animator.SetBool("onGround", true);
        }  
    }


    private void doMovement()
    {
        if (canMove)
        {
            
            if (!overHeat)
            {
                // ¼ì²âÌøÔ¾°´¼ü£¨Ä¬ÈÏÎª¡°¿Õ¸ñ¼ü¡±£©
                if (jumpBuffer.output && isGrounded && jumpable)
                {
                    states = States.Jumping;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Ìí¼Ó´¹Ö±ËÙ¶ÈÒÔÊµÏÖÌøÔ¾
                    animator.SetTrigger("jumping");
                    isGrounded = false;
                    animator.SetBool("onGround", false);
                    heatUsed += jumpCost;
                }
                if (Input.GetButtonUp("Jump"))
                {
                    //animator.ResetTrigger("jumping");
                }
            }


            if (!jumpable && rb.velocity.y < 0)
            {
                isGrounded = false;
                animator.SetBool("onGround", false);
                animator.ResetTrigger("landing");
                animator.SetTrigger("falling");
            }


            // Ë®Æ½ÒÆ¶¯
            float moveInput = Input.GetAxis("Horizontal"); // »ñÈ¡Ë®Æ½Öá£¨A/D»ò×óÓÒ¼ýÍ·£©ÊäÈë
            if (moveInput != 0 && faceRight * moveInput < 0)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
                faceRight *= -1;
                //Debug.Log("FlipX");
            }
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y); // ÉèÖÃË®Æ½ËÙ¶È
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
                bullet.GetComponent<Rigidbody2D>().velocity = faceRight * transform.right * bulletSpeed; // ÉèÖÃ×Óµ¯ËÙ¶È
                bullet.GetComponent<SpriteRenderer>().flipX = faceRight == 1 ? false : true;

                heatUsed += shootCost;
            }
        }

        if (Time.time - lastShotTime > 0.3)
        {
            animator.SetBool("shoot", false);
        }
    }
    private void heatUIUpdate()
    {   
        if(heatUsed >= heatLimit)
        {
            heatUsed = heatLimit;
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

    public void HeatHurt(float heat)
    {
        if(Time.time - lastHurtTime > hurtProtectTime)
        {
            heatUsed += heat;
            lastHurtTime = Time.time;
        }

    }


}
