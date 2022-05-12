using UnityEngine;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class SimpleShooterUnit : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] public Rigidbody2D rb ;
        [SerializeField] private float maxRotAngle = 180f;
        [SerializeField] private float maxSpeed = 10f;
        private float unitRadius = 1.3f;

        public void Shot()
        {
            Instantiate(bulletPrefab, transform.position + transform.right * unitRadius, transform.rotation);
        }

        public void Rotate(float rotationInput)
        {
            transform.Rotate(0f, 0f, -rotationInput * maxRotAngle * Time.deltaTime);
        }

        public void MoveForward(float moveInput)
        {
          rb.velocity = (Vector2) transform.right * moveInput * maxSpeed ;
        }

    }
}