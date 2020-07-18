using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandScript : MonoBehaviour
{
    public Transform player;
    public float offset = 1f;
    public bool canShoot;
    public Transform shotOrigin;
    public float shotCooldown = 0.3f;
    public GameObject magicShot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position); 
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector3 playerToMouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.position;
        playerToMouseDirection.z = 0;
        transform.position = player.position + (offset * playerToMouseDirection.normalized);
    }

    public void Shoot() 
    {
        if (canShoot == true) 
        {
            Instantiate(magicShot, shotOrigin.position, shotOrigin.rotation);

            StartCoroutine(ShotCooldown());
        } 
    }

    IEnumerator ShotCooldown()
    {
        canShoot = false;

        yield return new WaitForSeconds(shotCooldown);

        canShoot = true;
    }
}
