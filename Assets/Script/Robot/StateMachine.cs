using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Assets.Script.Robot
{
    public class StateMachine : MonoBehaviour
    {
        Dictionary<string, MyState> states;
        MyState currentState;
        public bool faceLeft;

        public float moveSpeed = 5f; // ��ɫ�ƶ��ٶ�
        public float jumpForce = 5f; // ��Ծ����

        public Rigidbody2D rb;
        public Animator animator;
        public SpriteRenderer spriteRenderer;
        public bool isGrounded; // ���ڼ���ɫ�Ƿ��ڵ�����

        // Start is called before the first frame update
        void Start()
        {
            //initial all states
            states = new Dictionary<string, MyState>();
            states["IDLE"] = new IdleState(this);
            states["RUN"] = new RunState(this);
            currentState = states["IDLE"];
            faceLeft = false;
        }

        // Update is called once per frame
        void Update()
        {
            currentState.OnUpdate();
        }

        public void SetCurrentState(string sKey)
        {
            currentState = states[sKey];
            currentState.OnEnter();
        }

        public void FlipX(bool faceTOLeft)
        {
            if (faceTOLeft != faceLeft) {
                spriteRenderer.flipX = !spriteRenderer.flipX;
                faceLeft = faceTOLeft;
            }
        }
    }
}

