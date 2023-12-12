using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callFutureMe : MonoBehaviour
{
    public GameObject futureMe;
    public GameObject backSpot;
    public float cd = 5f;
    public float radius = 5f;

    private bool actived = false;
    private Vector3 mouseDownPosition;
    private bool isDragging = false;
    private CFM_CD cdDisplayer;


    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;
    private Rigidbody2D rb2D;
    private BasicControl bc;

    // Start is called before the first frame update
    void Start()
    {
        actived = false;
        cdDisplayer = GetComponent<CFM_CD>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        bc = GetComponent<BasicControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(cdDisplayer.isReady())
            {
                doActive();
            }
            else
            {
                Debug.Log("CFM is still in CD");
            }

        }

        if(actived && Input.GetMouseButtonDown(0))
        {
            mouseDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDownPosition.z = 0;

            if (Vector3.Distance(transform.position, mouseDownPosition) <= radius)
            {
                isDragging = true;
            }
            else
            {
                actived = false;
            }
        }

        if (isDragging)
        {
           //to be continued (UI...)
        }

        if(isDragging && Input.GetMouseButtonUp(0))
        {
            Vector3 mouseUpPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseUpPosition.z = 0;
            futureMeAssist fma = futureMe.GetComponent<futureMeAssist>();
            if(fma  != null)
            {
                fma.call(mouseDownPosition, mouseUpPosition);
            }

            cdDisplayer.ResetCooldown();

            isDragging = false;
            actived = false;
        }


    }

    public void travelBack(Vector3 position)
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.0f);
        collider2D.enabled = false;
        rb2D.velocity = Vector3.zero; 
        rb2D.isKinematic = true;
        bc.moveLimit();
        backSpot.GetComponent<BackSpot>().Show(position);
        // 开始延时
        StartCoroutine(DisappearDelay());
    }

    IEnumerator DisappearDelay()
    {
        yield return new WaitForSeconds(2f); // 等待2秒
        ExitDisappearState();
    }
    void ExitDisappearState()
    {
        // 恢复显示和物理
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        collider2D.enabled = true;
        rb2D.isKinematic = false;
        bc.removeMoveLimit();
        transform.position = backSpot.GetComponent<BackSpot>().Hide();
    }


    void doActive()
    {
        actived = true;
        //to be continued (UI...)
    }


}
