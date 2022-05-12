using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.EnemyAIstates
{
    public class EnemyAimUndirect : EnemyState
    {
        public override void EnterState(EnemyStateMachine enemyStateMachine)
        {
            base.EnterState(enemyStateMachine);
            unit.RotateToDir(-context.transform.right);
        }

        public override void ExitState()
        {
            unit.rotating = false;
        }

        public override void UpdateState()
        {
            if (context.CanHitRicochet)
            {
                unit.rotating = false;
                context.ChangeState(context.enemyAttackUndirect);
                return;
            }

            if (!unit.rotating)
            {
                if (context.IsObstacleBetween())
                    context.ChangeState(context.enemyMove);
                else
                    context.ChangeState(context.enemyAimDirect);
            }            
        }

        public override string ToString()
        {
            return "enemy is aiming ricochet";
        }
    }
}
