using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EnemyAIstates
{
    public class EnemyStateMachine : MonoBehaviour
    {
        [SerializeField] public SimpleUnitWithAutoMotions unit;
        [SerializeField] private LayerMask whatToHit;
        public int shotCdInMs { get; private set; } = 500;

        private Transform playerTransform;
        private float angleEqualParameter = 0.99f;
        private EnemyState state;
        public EnemyAimDirect enemyAimDirect { get; private set; } = new EnemyAimDirect();
        public EnemyAimUndirect enemyAimUndirect { get; private set; } = new EnemyAimUndirect();
        public EnemyAttackUndirect enemyAttackUndirect { get; private set; } = new EnemyAttackUndirect();
        public EnemyAttackDirect enemyAttackDirect { get; private set; } = new EnemyAttackDirect();
        public EnemyMove enemyMove { get; private set; } = new EnemyMove();

        private void Start()
        {
            playerTransform = GameObject.FindObjectOfType<PlayerControl>().transform;
            BulletDirectionDrawer.SetOriginTransform(transform);
            ChangeState(enemyAimDirect);
        }

        public void ChangeState(EnemyState newState)
        {
            state?.ExitState();
            state = newState;
            EnemyLogger.Log(state.ToString());
            state.EnterState(this);
        }

        private void Update()
        {
            state.UpdateState();
        }

        public Vector2 ToPlayerDirection => playerTransform.position - transform.position;
        private Vector2 myPos => (Vector2)transform.position;
        public bool IsObstacleBetween()
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(myPos, ToPlayerDirection, Mathf.Infinity, whatToHit);
            if (!raycastHit2D.collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool PlayerAtGunpoint()
        {
            Vector2 toPlayerNormalized = ToPlayerDirection.normalized;
            if (Vector2.Dot(toPlayerNormalized, transform.right) >= angleEqualParameter)
                return true;
            else
                return false;
        }
        public void RotateToShot() => unit.RotateToDir(ToPlayerDirection);
        public bool CanHitDirect => !IsObstacleBetween() && PlayerAtGunpoint();
        public bool CanHitRicochet => BulletDirectionDrawer.CanHitRicochet();



    }
}
