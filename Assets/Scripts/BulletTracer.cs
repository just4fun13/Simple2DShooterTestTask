using UnityEngine;

namespace Assets.Scripts
{
    public class BulletTracer : MonoBehaviour
    {
        [SerializeField] private float bulletMoveSpeed = 100f;
        [SerializeField] private float bulletLifetime = 5f;
        [SerializeField] private Rigidbody2D bullletRb;

        private void Start()
        {
            bullletRb.velocity = transform.right * bulletMoveSpeed;
            if (bulletLifetime != 0f)
                Destroy(gameObject, bulletLifetime);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.gameObject.CompareTag("Ground"))
            {
                if (col.gameObject.CompareTag("Enemy"))
                    GameManager.PlayerWin();
                else
                    GameManager.EnemyWin();
                Destroy(gameObject);
            }
        }

    }
}
