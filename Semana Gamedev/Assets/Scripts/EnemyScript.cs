using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public Transform player;
    public bool canMove = true;
    public int damage;
    public float moveSpeed;
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collisiion)
    {
        if (collisiion.CompareTag("Player"))
        {
            player.GetComponent<PlayerScript>().TakeDamage(damage);
            StartCoroutine(FreezeMovement(2));
        }
    }

    IEnumerator FreezeMovement(float freezeTime)
    {
        canMove = false;

        yield return new WaitForSeconds(freezeTime);

        canMove = true;
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
