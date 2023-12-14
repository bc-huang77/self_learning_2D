using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate1 : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    private bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        if (!isOpen)
        {
            isOpen = true;
            left.transform.position += new Vector3(-0.6f, 0, 0);
            right.transform.position += new Vector3(0.6f, 0, 0);
        }
    }
}
