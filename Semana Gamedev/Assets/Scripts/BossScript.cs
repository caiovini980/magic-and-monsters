using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public bool rageMode;
    public float ragePercent;
    private float rageHealth;

    public Animator bossAnim;
    public Transform player;
    public bool canMove = true;
    public int damage;
    public float moveSpeed = 20f;
    public float health = 100f;
    public GameObject bossProjectile;
    public float projectileCount;
    
    public float shotCooldown;
    private float shotTimer;
    private bool canShoot;

    private Vector2 targetSpot;
    public Vector2[] movePoints;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //FIND THE PLAYER
        GetNextSpot();
        rageHealth = (ragePercent * health) / 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) 
        {
            if (transform.position.x != targetSpot.x && transform.position.y != targetSpot.y)
            {
                if (canMove)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetSpot, moveSpeed * Time.deltaTime);
                }
            }
            else
            {
                GetNextSpot();
            }

            if (shotTimer <= 0)
            {
                Shoot();
            }
            else
            {
                shotTimer = shotTimer - Time.deltaTime;
            }

            if (health <= rageHealth && !rageMode)
            {
                Enrage();
            }
        }
    }

    void Enrage()
    {
        rageMode = true;
        moveSpeed = moveSpeed * 3;
        shotCooldown = shotCooldown * 0.9f;
        projectileCount = projectileCount * 1.5f;
        bossAnim.SetTrigger("Rage");
    }

    void Shoot()
    {
        shotTimer = shotCooldown;
        float angleStep = 360f / projectileCount;
        float angle = 0f;

        for (int i = 0; i <= projectileCount - 1; i++)
        {
            float xPosition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * 360;
            float yPosition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * 360;

            Vector2 projectileDirection = new Vector2 (xPosition, yPosition);

            var projectile = Instantiate(bossProjectile, transform.position, Quaternion.identity);
            projectile.GetComponent<BossProjectile>().SetDirection(projectileDirection * 3);

            angle = angle + angleStep;
        }
    }

    private void GetNextSpot()
    {
        int randomSpot = Random.Range(0, movePoints.Length);
        targetSpot = movePoints[randomSpot];
    }

    public void TakeDamage(float damage)
    {
        health = health - damage;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScript>().TakeDamage(damage);
        }
    }
}
