using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Vector2 direction;
    private WandScript wand;
    private bool recovering;
    private bool dodge;
    private float recoveryCounter;
    private float dodgeCounter;
    private bool facingRight = true;

    public float recoveryTime;
    public float dodgeTime;
    public int health;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator playerAnim;
    public GameScript gameScript;

    // Start is called before the first frame update
    void Start()
    {
        wand = FindObjectOfType<WandScript>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        playerAnim.SetFloat("Speed", Mathf.Abs(direction.x) + Mathf.Abs(direction.y));

        Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

        if (dir.x < 0 && !facingRight || dir.x > 0 && facingRight)
        {
            Flip();
        }

        if (Input.GetMouseButton(0))
        {
            wand.Shoot();
        }

        if (recovering)
        {
            recoveryCounter = recoveryCounter + Time.deltaTime;
            if (recoveryCounter >= recoveryTime)
            {
                recoveryCounter = 0;
                recovering = false;
            }
        }
    }

    private void FixedUpdate() 
        {
            rb.MovePosition(rb.position + (direction * moveSpeed * Time.fixedDeltaTime));
        }

    void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
    void Flip() 
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void TakeDamage(int damage)
    {
        if (!recovering)
        {
            recovering = true;
            health = health - damage;
            UpdateHealthUI(health);

            if(health <= 0)
            {
                Destroy(gameObject);
                gameScript.Die();
            }
        }
    }
}
