using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiAI : MonoBehaviour
{
    public Transform player;
    public float hp, moveSpeed, timeBtweenShots, startTimeBtweenShots;
    public float MIN_SHOOT_DISTANCE;
    public Animator anim;
    public bool canWalk = true;
    public GameObject arrowPrefab;
    public float arrowForce;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        hp = 5f;
        timeBtweenShots = startTimeBtweenShots;
    }
    void Update()
    {
        if (Vector2.Distance(player.position, transform.position)> MIN_SHOOT_DISTANCE && canWalk)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }        

        Vector2 lookDir = player.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (hp <= 0f)
        {
            Destroy(gameObject);
        }

        Shoot();

    }

    public void TakeDamage(float hpDamage)
    {
        if (hp > 0f)
        {
            hp-= hpDamage;
            Debug.Log(hp);
        }
        
    }

    void Shoot()
    {
        if(Vector2.Distance(player.position, transform.position) < MIN_SHOOT_DISTANCE + 2)
        {
          

            if(timeBtweenShots > 0)
            {
                timeBtweenShots -= Time.deltaTime;
                canWalk = false;
            }
            else
            {
                GameObject Arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                Arrow prefab = Arrow.GetComponent<Arrow>();
                prefab.projectileVelocity = player.position * arrowForce;
                timeBtweenShots = startTimeBtweenShots;
                canWalk = true;
            }
        }
        else
        {
            canWalk = true;
        }
    }
}