using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private Player _player = default;

    private void Update()
    {
        if (Input.GetAxisRaw("Vertical") > 0) {
            _player.AddRelativeForce(ForceDirection.Up);
        }
        if (Input.GetAxisRaw("Horizontal") > 0) {
            _player.AddRotation(RotationDirection.Right);
        }
        if (Input.GetAxisRaw("Vertical") < 0) {
            _player.AddRelativeForce(ForceDirection.Down);
        }
        if (Input.GetAxisRaw("Horizontal") < 0) {
            _player.AddRotation(RotationDirection.Left);
        }
    }
}
