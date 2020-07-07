using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private Player player = default;

    [SerializeField]
    private AsteroidBehavior asteroidPrefab = default;

    [SerializeField]
    private int asteroidsSpawnLimit = 10;

    [SerializeField, Range(0.0001f, 0.5f)]
    private float xSpawnPaddingFactor = 0.1f;

    [SerializeField, Range(0.0001f, 0.5f)]
    private float ySpawnPaddingFactor = 0.1f;

    private float _xSpawnPadding = default;
    private float _ySpawnPadding = default;

    private List<AsteroidBehavior> _asteroidsContainer = default;

    private Vector3 _topRightBound = default; 
    private Vector3 _bottomLeftBound = default; 

    private void Start()
    {
        _asteroidsContainer = new List<AsteroidBehavior>();

        float zDistance = transform.position.z - Camera.main.transform.position.z;

        _bottomLeftBound = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, zDistance));
        _topRightBound = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, zDistance));

        _xSpawnPadding = xSpawnPaddingFactor * Mathf.Abs(_topRightBound.x - _bottomLeftBound.x);
        _ySpawnPadding = ySpawnPaddingFactor * Mathf.Abs(_topRightBound.y - _bottomLeftBound.y);

        SpawnAsteroids();
    }

    private void Update()
    {
        if (player.isActiveAndEnabled == true) {
            if (Input.GetAxisRaw("Vertical") > 0) {
                player.AddRelativeForce(ForceDirection.Up);
            }
            if (Input.GetAxisRaw("Horizontal") > 0) {
                player.AddRotation(RotationDirection.Right);
            }
            if (Input.GetAxisRaw("Vertical") < 0) {
                player.AddRelativeForce(ForceDirection.Down);
            }
            if (Input.GetAxisRaw("Horizontal") < 0) {
                player.AddRotation(RotationDirection.Left);
            }
            if (Input.GetButtonDown("Fire1")) {
                player.GetComponent<ShootBehavior>().Shoot();
            }        
        }
    }

    private void SpawnAsteroids()
    {
        Vector3 spawnPosition = new Vector3();
        spawnPosition.z = transform.position.z;

        float xIndent;
        float yIndent;
        for (int i = 0; i < asteroidsSpawnLimit; i++) {
            // Choose the side along to which we wil generate a position.
            // The generated position locates near chosen axis with some padding,
            // so there some area around the border to spawn objects.
            // if the RNG produce 0, generate the position along the y axis;
            // else (if the RNG produce 1), generate the position along the x axis.
            if (Random.Range(0, 2) == 0) {
                xIndent = Random.Range(-1 * _xSpawnPadding, _xSpawnPadding);
                if (xIndent > 0) {
                    spawnPosition.x = _bottomLeftBound.x + xIndent;
                }
                else {
                    // The indent is negative, so the addition move the objects position to the left
                    // of the screen.
                    spawnPosition.x = _topRightBound.x + xIndent;
                }

                spawnPosition.y = Random.Range(_bottomLeftBound.y, _topRightBound.y);
            }
            else {
                yIndent = Random.Range(-1 * _ySpawnPadding, _ySpawnPadding);
                if (yIndent > 0) {
                    spawnPosition.y = _bottomLeftBound.y + yIndent;
                }
                else {
                    // The indent is negative, so the addition move the objects position to the bottom
                    // of the screen.
                    spawnPosition.y = _topRightBound.y + yIndent;
                }

                spawnPosition.x = Random.Range(_bottomLeftBound.x, _topRightBound.x);
            }

            Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
