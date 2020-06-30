using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private Player _player = default;

    private void FixedUpdate()
    {
        if (Input.GetAxisRaw("Vertical") > 0) {
            _player.AddForce(ForceDirection.Up);
        }
        if (Input.GetAxisRaw("Horizontal") > 0) {
            _player.AddTorque(TorqueDirection.Right);
        }
        if (Input.GetAxisRaw("Vertical") < 0) {
            _player.AddForce(ForceDirection.Down);
        }
        if (Input.GetAxisRaw("Horizontal") < 0) {
            _player.AddTorque(TorqueDirection.Left);
        }
    }
}
