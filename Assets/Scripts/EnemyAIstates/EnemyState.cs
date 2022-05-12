using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.EnemyAIstates
{
    public abstract class EnemyState
    {
        public virtual void EnterState()
        {

        }
         
        public virtual void ExitState()
        {

        }

        public virtual void UpdateState()
        {

        }
    }
}
