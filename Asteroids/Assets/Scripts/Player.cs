using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _playerRigidbody2D;

    private void Awake()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void AddRelativeForce(ForceDirection forceDirection)
    {
        _playerRigidbody2D.AddRelativeForce(30f * forceDirection.GetForce());
    }

    public void AddRotation(RotationDirection rotationDirection)
    {
        _playerRigidbody2D.rotation += 3f * rotationDirection.GetTorque();
    }
}
