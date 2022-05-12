using UnityEngine;
using System.Collections;
using Pathfinding;

namespace Assets.Scripts
{
    public class SimpleUnitWithAutoMotions : SimpleShooterUnit
    {
        [SerializeField] private float shotCd = 0.5f;
        [SerializeField] public  AIPath aIdestinationSetter;
        private float angleEqualParameter = 0.99f;
        public bool Busy { get; private set; } = false;

        public bool shot = false;
        public bool rotating = false;

        private bool moving;

        public bool Move
        {
            get { return moving; }
            set 
            { 
                moving = value;
                aIdestinationSetter.enabled = moving;
            }
        }

        private void Start()
        {
            StartCoroutine(ShotContinuous());
        }

        private IEnumerator ShotContinuous()
        {
            while (true)
            {
                if (shot)
                    Shot();
                yield return new WaitForSeconds(shotCd);
            }
        }

        public void RotateToDir(Vector2 dir)
        {
            StartCoroutine(RotateToDirCoroutine(dir));
        }

        IEnumerator RotateToDirCoroutine(Vector2 dir)
        {
            rotating = true;
            Vector2 direction = dir.normalized;
            Quaternion myInitRotation = transform.rotation;
            Quaternion desiredRotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x));
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime;
                if (!rotating) break;
                transform.rotation = Quaternion.Slerp(myInitRotation, desiredRotation, t);
                yield return new WaitForEndOfFrame();
            }
            rotating = false;
        }

    }
}
