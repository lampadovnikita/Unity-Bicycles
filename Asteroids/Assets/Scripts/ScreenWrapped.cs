﻿using UnityEngine;

public class ScreenWrapped : MonoBehaviour
{
    private Vector3 _topRight;
    private Vector3 _bottomLeft;

    private float _spriteWidth;
    private float _spriteHeight;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = transform.GetComponent<Rigidbody2D>();

        float zDistance = transform.position.z - Camera.main.transform.position.z;

        _bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, zDistance));
        _topRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, zDistance));

        _spriteWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
        _spriteHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y * 0.5f;
    }

    private void LateUpdate()
    {
        Vector3 position = transform.position;

        float padding = 0.1f;

        if (Mathf.Abs(position.x) > _topRight.x + _spriteWidth + padding) {
            float newX = -1 * position.x;
            newX += (position.x >= _topRight.x) ? padding : -padding;
            
            _rigidbody2D.position = new Vector2(newX, position.y);
        }
        else if (Mathf.Abs(position.y) > _topRight.y + _spriteHeight + padding) {
            float newY = -1 * position.y;
            newY += (position.y > _topRight.y) ? padding : -padding;

            _rigidbody2D.position = new Vector2(position.x, newY);
        }
    }
}
