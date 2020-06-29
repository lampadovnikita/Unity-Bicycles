using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            _player.move(Direction.Up);
        }
        if (Input.GetKey(KeyCode.D)) {
            _player.move(Direction.Right);
        }
        if (Input.GetKey(KeyCode.S)) {
            _player.move(Direction.Bottom);
        }
        if (Input.GetKey(KeyCode.A)) {
            _player.move(Direction.Left);
        }
    }
}
