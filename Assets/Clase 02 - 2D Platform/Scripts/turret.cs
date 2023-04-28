using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class turret : MonoBehaviour
{
    public GameObject player;
    public GameObject particlePfab;
    public float distanceBeetween;
    private float timer;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    public float timeBetweenFire;
    private float distance;

    private void Start()
    {
        canFire = true;
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < distanceBeetween)
        {
            if(canFire)
            {
                canFire = false;
                Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            }
            
        }

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFire)
            {
                canFire = true;
                timer = 0;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            GameObject particleVFX = Instantiate(particlePfab, transform.position, Quaternion.identity);
            Destroy(particleVFX, 2f);
            Destroy(this.gameObject);
        }
    }
}
