using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private Player player = default;

    [SerializeField]
    private int asteroidsLimit = 10;

    [SerializeField]
    private AsteroidBehavior asteroidPrefab = default;

    private List<AsteroidBehavior> _asteroidsContainer = default;


    private void Start()
    {
        _asteroidsContainer = new List<AsteroidBehavior>();

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
        for (int i = 0; i < asteroidsLimit; i++) {
            Instantiate(asteroidPrefab);
        }
    }
}
