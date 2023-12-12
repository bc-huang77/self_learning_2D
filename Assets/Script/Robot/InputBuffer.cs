using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBuffer : MonoBehaviour
{
    public bool output = false;
    public float bufferTime = 0.1f;
    private string command;
    private float bufferTimer = 0f;
    // private bool isInputBuffered false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(command))
        {
            bufferTimer = bufferTime;
            // isInputBuffered = true;
        }
        if (bufferTimer > 0f)
        {
            bufferTimer -= Time.deltaTime;
            output = true;

        }
        else
        {
            output = false;
        }



    }
    public void setCommand(string cm)
    {
        command = cm;
    }
}
