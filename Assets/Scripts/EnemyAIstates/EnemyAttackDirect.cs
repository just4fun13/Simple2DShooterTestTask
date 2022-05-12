
namespace Assets.Scripts.EnemyAIstates
{
    public class EnemyAttackDirect : EnemyState
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
            if (context.IsObstacleBetween())
            {
                context.ChangeState(context.enemyAimUndirect);
                return;
            }
            if (!context.PlayerAtGunpoint())
            {
                context.ChangeState(context.enemyAimDirect);
                return;
            }
        }
        public override string ToString()
        {
            return "enemy is attacking direct";
        }

    }
}
