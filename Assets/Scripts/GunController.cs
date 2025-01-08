using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform bulletSpawnPoint; // Postion where the bullet will be spawned
    public GameObject bulletPrefab;
    public float bulletSpeed = 10.0f; // Speed factor of the bullet
    /*void Update()
    {
        Shoot();
    }*/
    public void Shoot()
    {
            var bullet = Instantiate(bulletPrefab); // Create a new bullet
            bullet.transform.position = bulletSpawnPoint.position; // Set the bullet position to the bulletSpawnPoint position
            // Shoot the bullet by adding velocity to the rigigbody component of the bullet
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed; 
    }
}
