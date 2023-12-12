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

        public float moveSpeed = 5f; // 角色移动速度
        public float jumpForce = 5f; // 跳跃力量

        public Rigidbody2D rb;
        public Animator animator;
        public SpriteRenderer spriteRenderer;
        public bool isGrounded; // 用于检测角色是否在地面上

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

