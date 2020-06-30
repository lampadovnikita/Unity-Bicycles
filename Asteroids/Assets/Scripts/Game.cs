using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private Player _player = default;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            _player.AddForce(ForceDirection.Up);
        }
        if (Input.GetKey(KeyCode.D)) {
            _player.AddTorque(TorqueDirection.Right);
        }
        if (Input.GetKey(KeyCode.S)) {
            _player.AddForce(ForceDirection.Down);
        }
        if (Input.GetKey(KeyCode.A)) {
            _player.AddTorque(TorqueDirection.Left);
        }
    }
}
