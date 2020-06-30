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
        _playerRigidbody2D.AddRelativeForce(forceDirection.GetForce());
    }

    public void AddRotation(RotationDirection torqueDirection)
    {
        _playerRigidbody2D.rotation += torqueDirection.GetTorque();
    }
}
