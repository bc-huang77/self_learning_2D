using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Robot
{
    public class RunState : MyState
    {
        public RunState(StateMachine machine) : base(machine)
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();
            if (m_machine.faceLeft)
            {
                m_machine.rb.velocity = new Vector2( -1 * m_machine.moveSpeed, m_machine.rb.velocity.y); // 设置水平速度
            }
            else
            {
                m_machine.rb.velocity = new Vector2(1 * m_machine.moveSpeed, m_machine.rb.velocity.y); // 设置水平速度
            }
            m_machine.animator.SetFloat("speed", Math.Abs(m_machine.rb.velocity.x));
        }

        public override void OnUpdate()
        {
            float moveInput = Input.GetAxis("Horizontal"); // 获取水平轴（A/D或左右箭头）输入
            if(Math.Abs(moveInput) < 0.05)
            {
                m_machine.SetCurrentState("IDLE");
                return;
            }
            m_machine.FlipX(moveInput < 0);

            m_machine.rb.velocity = new Vector2(moveInput * m_machine.moveSpeed, m_machine.rb.velocity.y); // 设置水平速度
            m_machine.animator.SetFloat("speed", Math.Abs(m_machine.rb.velocity.x));
        }
    }
}
