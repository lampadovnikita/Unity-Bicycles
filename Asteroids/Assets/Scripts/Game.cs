using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            player.move(Direction.Up);
        }
        if (Input.GetKey(KeyCode.D)) {
            player.move(Direction.Right);
        }
        if (Input.GetKey(KeyCode.S)) {
            player.move(Direction.Bottom);
        }
        if (Input.GetKey(KeyCode.A)) {
            player.move(Direction.Left);
        }
    }
}
