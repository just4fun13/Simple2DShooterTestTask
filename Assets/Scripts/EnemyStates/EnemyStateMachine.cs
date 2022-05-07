using UnityEngine;

namespace Assets.Scripts.EnemyStates
{
    public class EnemyStateMachine : MonoBehaviour
    {
        [SerializeField] public SimpleShooterUnit unit;
        [SerializeField] private LayerMask whatToHit;
        public int shotCdInMs { get; private set; } = 500;

        private EnemyAttackFromSide enemyAim = new EnemyAttackFromSide();
        private EnemyMoveAwayFromPlayer enemyMove = new EnemyMoveAwayFromPlayer();
        private EnemyMoveToPlayer enemyHide = new EnemyMoveToPlayer();
        private EnemyAttackDirect enemyAttack = new EnemyAttackDirect();
        private EnemyState enemyState;
        private Transform playerTransform;
        private float smallOffset = 1f;
        private float aimEnough = 0.99f;

        public void ChangeState(EnemyState newState)
        {
            enemyState?.ExitState();
            enemyState = newState;
            enemyState.EnterState( this);
        }

        private void Start()
        {
            playerTransform = GameObject.FindObjectOfType<PlayerControl>().transform;
            ChangeState(enemyAttack);
        }

        private void Update()
        {
            enemyState.UpdateState();
        }

        private Vector2 ToPlayerDirection => playerTransform.position - transform.position;
        private Vector2 myPos => (Vector2)transform.position;
        private Quaternion ToPlayerRotation => Quaternion.Euler(0f, 0f, Mathf.Atan2(ToPlayerDirection.y, ToPlayerDirection.x));

        public bool PlayerIsOpen()
        {
            RaycastHit2D hit = Physics2D.Raycast(myPos, ToPlayerDirection, Mathf.Infinity, whatToHit);
            if (hit.collider != null && hit.transform.Equals(playerTransform))
                return true;
            else
                return false;
        }

        public bool PlayerAtGunpoint()
        {
            Vector2 toPlayerNormalized = ToPlayerDirection.normalized;
            if (Vector2.Dot(toPlayerNormalized, transform.right) >= aimEnough)
                return true;
            else
                return false;
        }

        public void RotateToShot()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, ToPlayerRotation, Time.deltaTime);
        }

    }
}
