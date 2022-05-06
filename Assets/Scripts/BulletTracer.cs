using UnityEngine;

namespace Assets.Scripts
{
    public class BulletTracer : MonoBehaviour
    {
        [SerializeField] private float bulletMoveSpeed = 20f;
        [SerializeField] private float bulletLifetime = 5f;
        [SerializeField] private Rigidbody2D bullletRb;

        private void Start()
        {
            bullletRb.velocity = transform.up * bulletMoveSpeed;
            if (bulletLifetime != 0f)
                Destroy(gameObject, bulletLifetime);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log($"Collided with {col.gameObject.name}");
//            Destroy(gameObject);
        }

    }
}
