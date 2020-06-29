using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void AddForce(ForceDirection direction) {
        _rigidbody2D.AddForce(direction.GetForce());
    }
}
