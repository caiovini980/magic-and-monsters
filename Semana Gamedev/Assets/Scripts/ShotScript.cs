using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{

    public int shotDamage = 1;
    public float shotSpeed = 20f;
    public float shotLifeTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, shotLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * shotSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            collision.GetComponent<EnemyScript>().TakeDamage(shotDamage);
        }

        if (collision.CompareTag("EnemyRanged"))
        {
            Destroy(gameObject);
            collision.GetComponent<EnemyRangedScript>().TakeDamage(shotDamage);
        }

        if (collision.CompareTag("Boss"))
        {
            Destroy(gameObject);
            collision.GetComponent<BossScript>().TakeDamage(shotDamage);
        }
    }
}
