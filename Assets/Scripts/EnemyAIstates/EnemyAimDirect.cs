using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.EnemyAIstates
{
    public class EnemyAimDirect : EnemyState
    {
        public override void EnterState(EnemyStateMachine enemyStateMachine)
        {
            base.EnterState(enemyStateMachine);
        }

        public override void ExitState()
        {
            unit.rotating = false;
        }

        public override void UpdateState()
        {
            if (context.IsObstacleBetween())
            {
                context.ChangeState(context.enemyAimUndirect);
                return;
            }
            if (context.PlayerAtGunpoint())
            {
                context.ChangeState(context.enemyAttackDirect);
                return;
            }
            unit.RotateToDir(context.ToPlayerDirection);
        }
        public override string ToString()
        {
            return "enemy is aiming direct";
        }

    }
}
