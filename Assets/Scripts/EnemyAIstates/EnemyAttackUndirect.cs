
namespace Assets.Scripts.EnemyAIstates
{
    public class EnemyAttackUndirect : EnemyState
    {
        public override void EnterState(EnemyStateMachine enemyStateMachine)
        {
            base.EnterState(enemyStateMachine);
            unit.shot = true;
        }

        public override void ExitState()
        {
            unit.shot = false;
        }

        public override void UpdateState()
        {
            if (!context.CanHitRicochet)
            {
                context.ChangeState(context.enemyMove);
            }
        }
        public override string ToString()
        {
            return "enemy is attacking ricochet";
        }

    }
}
