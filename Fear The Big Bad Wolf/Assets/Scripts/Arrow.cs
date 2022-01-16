using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector2 projectileVelocity = new Vector2(0f, 0f);

    private void Update()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + projectileVelocity * Time.deltaTime;

        Debug.DrawRay(currentPosition, newPosition, Color.red);

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.collider.gameObject);
            if(hit.transform.tag != "Enemy")
            {
                Destroy(gameObject);
            }
        }
        transform.position = newPosition;
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.transform.tag == "Enemy")
            {
                Debug.Log("hit");
                EnemiAI enemy = collision.gameObject.GetComponent<EnemiAI>();
                enemy.TakeDamage(1f);
            }

            if (collision.transform.tag != "Player")
            {
                Destroy(gameObject);
            }

        }

    }
    */
}
