using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] private SimpleShooterUnit myUnit;

        private void Update()
        {
            if (Input.GetAxis("Vertical") != 0f)
            myUnit.MoveForward(Input.GetAxis("Vertical"));
            if (Input.GetAxis("Horizontal") != 0f)
            myUnit.Rotate(Input.GetAxis("Horizontal"));
            if (Input.GetKeyDown(KeyCode.Space))
                myUnit.Shot();
        }

    }
}
