using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void AddForce(ForceDirection forceDirection)
    {
        _rigidbody2D.AddRelativeForce(forceDirection.GetForce());
    }

    public void AddTorque(TorqueDirection torqueDirection)
    {
        _rigidbody2D.AddTorque(torqueDirection.GetTorque());
    }
}
