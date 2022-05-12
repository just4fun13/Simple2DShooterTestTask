
namespace Assets.Scripts.EnemyAIstates
{
    public abstract class EnemyState
    {
        protected EnemyStateMachine context;
        protected SimpleUnitWithAutoMotions unit => context.unit;
        public virtual void EnterState(EnemyStateMachine enemyStateMachine)
        {
            context = enemyStateMachine;
        }
         
        public virtual void ExitState()
        {

        }

        public virtual void UpdateState()
        {

        }
    }
}
