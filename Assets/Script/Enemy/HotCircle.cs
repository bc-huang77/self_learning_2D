using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class HotCircle : MonoBehaviour
{
    public float damage = 10f;
    private bool isFrozen = false;
    public float kickSpeed = 5f; 
    private Rigidbody2D rb2d;
    private float lastKickTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<BasicControl>().addHearUsed(damage);
        }*/
        if (collision.gameObject.CompareTag("iceBullet"))
        {
            if(!isFrozen)
            {
                FrozenCircle();
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFrozen)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (Time.time - lastKickTime > 0.3f)
                {
                    lastKickTime = Time.time;
                    Debug.Log("Kick");
                    Vector2 v = collision.GetContact(0).normal;
                //Vector2 v2 = collision.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
                    rb2d.velocity = new Vector2(v.x * kickSpeed, kickSpeed); 
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<BasicControl>().HeatHurt(damage);
        }
    }

    private void FrozenCircle()
    {
        isFrozen = true;
        gameObject.tag = "FrozenCircle";
        GetComponent<Animator>().SetTrigger("Frozen");
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().isTrigger = false;
        Vector3 p = GetComponent<Patrol>().HotCircleFrozen();
        rb2d.velocity = (new Vector2(p.x, p.y) - new Vector2(transform.position.x, transform.position.y)).normalized * 0.5f;    
    }

    public void FrozenAnimEnd()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.21961f, 0.9096f, 0.9451f, 1f);
        GetComponent<Animator>().SetTrigger("FrozenEnd");
    }

}
