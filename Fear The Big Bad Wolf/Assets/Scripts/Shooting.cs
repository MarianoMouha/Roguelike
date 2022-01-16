using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform shootPoint;
    public float arrowForce;

    public void Shoot()
    {
        Vector2 lookDir = GetComponent<PlayerMovement>().lookDir;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion bowRotation = Quaternion.Euler(0, 0, angle);         
        GameObject Arrow = Instantiate(arrowPrefab, transform.position, bowRotation);
        Rigidbody2D rb = Arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(-shootPoint.up * arrowForce, ForceMode2D.Impulse);
    }
}
