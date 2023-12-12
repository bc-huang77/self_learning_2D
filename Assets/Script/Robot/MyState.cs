using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Robot
{
    public abstract class MyState
    {
        public StateMachine m_machine;

        public MyState(StateMachine machine)
        {
            m_machine = machine;
        }

        public virtual void OnEnter() { }
        public virtual void OnUpdate(){ }
        public virtual void OnExit() { }
    }
}
