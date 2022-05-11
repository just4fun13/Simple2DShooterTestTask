using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EnemyStates
{
    public class EnemyStateMachine : MonoBehaviour
    {
        enum EnemyStates { attackFront, movingAround, attackSide, moveingAway, idle }

        EnemyStates state = EnemyStates.idle;

        [SerializeField] public SimpleUnitWithAutoMotions unit;
        [SerializeField] private LayerMask whatToHit;
        public int shotCdInMs { get; private set; } = 500;

        private Transform playerTransform;
        private float smallOffset = 1f;
        private float angleEqualParameter = 0.99f;

        private void Start()
        {
            playerTransform = GameObject.FindObjectOfType<PlayerControl>().transform;
            BulletDirectionDrawer.SetOriginTransform(transform);
            WhatToDo();
        }

        private Vector2 ToPlayerDirection => playerTransform.position - transform.position;
        private Vector2 AwayFromPlayerDirection => -ToPlayerDirection;
        private Vector2 LeftAroundPlayer => new Vector2(ToPlayerDirection.y, -ToPlayerDirection.x);
        private Vector2 RightAroundPlayer => new Vector2(-ToPlayerDirection.y, ToPlayerDirection.x);

        private Vector2 myPos => (Vector2)transform.position;

        public RaycastHit2D HitInPlayerDirection =>
            Physics2D.Raycast(myPos, ToPlayerDirection, Mathf.Infinity, whatToHit);

        public bool IsObstacleBetween()
        {
            if (HitInPlayerDirection.collider.gameObject.CompareTag("Ground"))
                return true;
            else
                return false;
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

        private bool CanHitDirect => !IsObstacleBetween() && PlayerAtGunpoint();
        private bool CanHitRicochet => BulletDirectionDrawer.CanHitRicochet();
        void WhatToDo()
        {
            // Если игрок в открытой местности, то стреляем,
            // если игрок прикрыт, то пытаемся прицелиться через рикошет
            // если не получается обходим препятсвие и пробуем все заново
            if (!IsObstacleBetween())
            {
                RotateToShot();
                StartCoroutine(Shooting());
            }
            else
            {
                TryHitAside();
            }

        }

        void TryHitAside()
        {
            unit.RotateToDir(-transform.right);
            while (unit.rotating)
            {
                if (CanHitRicochet)
                {
                    unit.rotating = false;
                    StartCoroutine(Shooting());
                }
            }
            if (!unit.shot)
                StartCoroutine(Moving());
        }
        IEnumerator Moving()
        {
            int sign = 1;
            if (Random.Range(0, 2) > 0)
            {
                sign = -1;
            }
            while (IsObstacleBetween())
            {
                Vector2 dir = new Vector2(sign * -ToPlayerDirection.y, sign * ToPlayerDirection.x);
                unit.RotateToDir(dir);
                unit.MoveForward(1f);
                yield return new WaitForEndOfFrame();
            }
            unit.MoveForward(0);
            WhatToDo();
        }

        IEnumerator Shooting()
        {
            unit.shot = true;
            while (CanHitDirect || CanHitRicochet)
            {
                yield return new WaitForEndOfFrame();
            }
            unit.shot = false;
            WhatToDo();
        }

    }
}
