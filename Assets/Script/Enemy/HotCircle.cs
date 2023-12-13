using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class HotCircle : MonoBehaviour
{
    public float damage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
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
            FrozenCircle();
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

        GetComponent<Animator>().SetTrigger("Frozen");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().isTrigger = false;
        Vector3 p = GetComponent<Patrol>().HotCircleFrozen();
        GetComponent<Rigidbody2D>().velocity = (new Vector2(p.x, p.y) - new Vector2(transform.position.x, transform.position.y)).normalized * 0.5f;    
    }

    public void FrozenAnimEnd()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.21961f, 0.9096f, 0.9451f, 1f);
        GetComponent<Animator>().SetTrigger("FrozenEnd");
    }

}
