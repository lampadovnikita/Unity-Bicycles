using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private Player player = default;

    private void Update()
    {
        if (Input.GetAxisRaw("Vertical") > 0) {
            player.AddRelativeForce(ForceDirection.Up);
        }
        if (Input.GetAxisRaw("Horizontal") > 0) {
            player.AddRotation(RotationDirection.Right);
        }
        if (Input.GetAxisRaw("Vertical") < 0) {
            player.AddRelativeForce(ForceDirection.Down);
        }
        if (Input.GetAxisRaw("Horizontal") < 0) {
            player.AddRotation(RotationDirection.Left);
        }
        if (Input.GetButtonDown("Fire1")) {
            player.GetComponent<ShootBehavior>().Shoot();
        }
    }
}
