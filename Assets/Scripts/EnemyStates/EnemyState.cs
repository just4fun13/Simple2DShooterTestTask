using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.EnemyStates
{
    public abstract class EnemyState
    {
        public virtual void EnterState(EnemyStateMachine enemyStateMachine)
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