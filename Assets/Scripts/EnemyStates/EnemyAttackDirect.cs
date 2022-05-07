using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EnemyStates
{
    public class EnemyAttackDirect : EnemyState
    {
        EnemyStateMachine context;
        public override void EnterState(EnemyStateMachine enemyStateMachine)
        {
            context = enemyStateMachine;
            ShotConstantly();
            Debug.Log("enemy Attacking ");
        }


        private IEnumerator RotateToShot()
        {
            while (!context.PlayerAtGunpoint())
            {
                context.RotateToShot();
                yield return new WaitForEndOfFrame();
            }
        }

        private async void ShotConstantly()
        {
            while (context.PlayerAtGunpoint() && context.PlayerIsOpen())
            {
                context.unit.Shot();
                await Task.Delay(context.shotCdInMs);
            }
            if (!context.PlayerIsOpen())
                ExitState();
            else
                RotateToShot();
        }

        public override void ExitState()
        {

        }
    }
}