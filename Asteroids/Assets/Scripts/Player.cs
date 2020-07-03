using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _playerRigidbody2D;

    private float maxAxisVelocity = 42f;
    
    [SerializeField]
    private float mainForce = 200f;

    [SerializeField]
    private float rotationForce = 100f;

    private void Awake()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void AddRelativeForce(ForceDirection forceDirection)
    {
        _playerRigidbody2D.AddRelativeForce(mainForce * Time.deltaTime * forceDirection.GetForce());
        
        if (Mathf.Abs(_playerRigidbody2D.velocity.x) > maxAxisVelocity) {
            float newVelocity = (_playerRigidbody2D.velocity.x > 0) ? maxAxisVelocity : -1 * maxAxisVelocity;
            _playerRigidbody2D.velocity = new Vector2(newVelocity, _playerRigidbody2D.velocity.y);
        }

        if (Mathf.Abs(_playerRigidbody2D.velocity.y) > maxAxisVelocity) {
            float newVelocity = (_playerRigidbody2D.velocity.y > 0) ? maxAxisVelocity : -1 * maxAxisVelocity;
            _playerRigidbody2D.velocity = new Vector2(_playerRigidbody2D.velocity.x, newVelocity);
        }        
    }

    public void AddRotation(RotationDirection rotationDirection)
    {
        _playerRigidbody2D.rotation += rotationForce * Time.deltaTime * rotationDirection.GetRoataion();
    }
}
