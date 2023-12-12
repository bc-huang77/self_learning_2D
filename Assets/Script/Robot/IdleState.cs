using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Robot
{
    public class IdleState : MyState
    {
        public IdleState(StateMachine machine) : base(machine)
        {

        }
        public override void OnEnter()
        {
            base.OnEnter();
            m_machine.animator.SetFloat("speed", 0);
        }


        public override void OnUpdate()
        {
            float moveInput = Input.GetAxis("Horizontal"); // 获取水平轴（A/D或左右箭头）输入
            if(moveInput != 0)
            {
                m_machine.FlipX(moveInput < 0);
                m_machine.SetCurrentState("RUN");
                return;
            }
            
            // 检测跳跃按键（默认为“空格键”）
            if (Input.GetButtonDown("Jump"))
            {
                m_machine.SetCurrentState("JUMP");
                return;
            }

        }
    }
}
