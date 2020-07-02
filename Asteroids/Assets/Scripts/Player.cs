﻿using UnityEngine;

public class Player : MonoBehaviour
{
    private static int halfHeight = Screen.height / 2;
    private static int halfWidth = Screen.width / 2;

    private Rigidbody2D _playerRigidbody2D;

    private void Awake()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnBecameInvisible()
    {
        Vector2 position = _playerRigidbody2D.position;
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
