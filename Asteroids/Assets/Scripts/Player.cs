using UnityEngine;

public class Player : MonoBehaviour
{
    public void move(Direction direction) {
        transform.position += direction.GetMovement();
    }
}
