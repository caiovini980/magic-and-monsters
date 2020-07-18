using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedScript : MonoBehaviour
{
    private float shotTimer;
    private bool canShoot = true;

    public Transform player;
    public float shotCooldown;
    public float stoppingDistance;
    public float retreatDistance;
    public float moveSpeed;
    public float health;

    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shotTimer = shotCooldown;
    }

    // Update is called once per frame
    void Update()
    {   
        if (player != null)
        {
            //FAR FROM THE SHOOT DISTANCE
            if(Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }

            //ON THE SHOOT DISTANCE
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;

                if (canShoot)
                {
                    Shoot();
                }
            }

            //NEAR THE PLAYER
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
            }
        }

        if (shotTimer <= 0)
            {
                canShoot = true;
            }
            else
            {
                canShoot = false;
                shotTimer = shotTimer - Time.deltaTime;
            }
    }

    void Shoot()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
        shotTimer = shotCooldown;
    }

    public void TakeDamage(float damage)
    {
        health = health - damage;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
