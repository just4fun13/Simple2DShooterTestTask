using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] private SimpleShooterUnit myUnit;

        private void Update()
        {
            myUnit.MoveForward(Input.GetAxis("Vertical"));
            myUnit.Rotate(Input.GetAxis("Horizontal"));
            if (Input.GetKeyDown(KeyCode.Space))
                myUnit.Shot();
        }

    }
}
