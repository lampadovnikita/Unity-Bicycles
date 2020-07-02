using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _playerRigidbody2D;

    [SerializeField]
    private float movementSpeed = 200f;

    [SerializeField]
    private float rotationSpeed = 100f;

    private void Awake()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void AddRelativeForce(ForceDirection forceDirection)
    {
        _playerRigidbody2D.AddRelativeForce(movementSpeed * Time.deltaTime * forceDirection.GetForce());
    }

    public void AddRotation(RotationDirection rotationDirection)
    {
        _playerRigidbody2D.rotation += rotationSpeed * Time.deltaTime * rotationDirection.GetTorque();
    }
}
