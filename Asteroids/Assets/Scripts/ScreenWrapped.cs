using UnityEngine;

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

        float rightBound = _topRight.x + _spriteWidth;
        float leftBound = _bottomLeft.x - _spriteWidth;

        float upperBound = _topRight.y + _spriteHeight;
        float bottomBound = _bottomLeft.y - _spriteHeight;

        // Position should be swapped to a bound coordinate instead of inverted position of an object
        // because that approach prevent bug with endless position inversion of an object that out of the screen area
        if (position.x > rightBound) {
            _rigidbody2D.position = new Vector2(leftBound, position.y);
        }
        else if (position.x < leftBound) {
            _rigidbody2D.position = new Vector2(rightBound, position.y);
        }
        else if (position.y > upperBound) {
            _rigidbody2D.position = new Vector2(position.x, bottomBound);
        }
        else if (position.y < bottomBound) {
            _rigidbody2D.position = new Vector2(position.x, upperBound);
        }
    }
}
