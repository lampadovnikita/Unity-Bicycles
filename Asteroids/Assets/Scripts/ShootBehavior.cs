﻿using UnityEngine;

public class ShootBehavior : MonoBehaviour
{
    [SerializeField]
    private BulletBehavior bulletPrefab = default;

    public void Shoot() {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}