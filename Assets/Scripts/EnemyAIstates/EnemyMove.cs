using UnityEngine;

namespace Assets.Scripts.EnemyAIstates
{
    public class EnemyMove : EnemyState
    {
        private float someTimeToTryRicochet = 5f;
        private float timeToTryRicochet;
        public override void EnterState(EnemyStateMachine enemyStateMachine)
        {
            base.EnterState(enemyStateMachine);
            timeToTryRicochet = Time.time + someTimeToTryRicochet;
            unit.Move = true;
        }

        public override void ExitState()
        {
            unit.Move = false;
            unit.rotating = false;
        }

        public override void UpdateState()
        {
            if (!context.IsObstacleBetween())
            {
                context.ChangeState(context.enemyAttackDirect);
                return;
            }
            if (Time.time > timeToTryRicochet)
            {
                context.ChangeState(context.enemyAimUndirect);
                return;
            }
            unit.RotateToDir(unit.aIdestinationSetter.velocity);

        }

        public override string ToString()
        {
            return "enemy is moving";
        }
    }
}
