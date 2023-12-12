using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class futureMeAssist : MonoBehaviour
{
    private Vector3 originalPosition;
    public GameObject timeSpot;
    public float calledDuration = 50f;
    private float calledTime;
    private bool isCalled;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        isCalled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCalled)
        {
            if(Time.time - calledTime > calledDuration)
            {
                timeSpot.GetComponent<TimeSpot>().Show(transform.position);

                transform.position = originalPosition;
                GetComponent<SpriteRenderer>().flipX = false;
                isCalled = false;
            }
        }
    }

    public void call(Vector3 downPosition, Vector3 upPosition)
    {
        calledTime = Time.time;
        isCalled = true;
        Vector3 direction = (upPosition - downPosition).normalized;
        transform.position = downPosition;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // ����϶���������תSprite
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;

                // ������ת�ǶȲ���תGameObject�����Ƿ�ת
                float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            else
            {
                spriteRenderer.flipX = false;

                // ������ת�ǶȲ���תGameObject
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }

    }
}
