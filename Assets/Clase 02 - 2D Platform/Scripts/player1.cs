using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1 : MonoBehaviour
{
    public float velocity;
    public float jump;

    public bool isGrounded = true;

    private Rigidbody2D playerRb;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float movX = Input.GetAxis("Horizontal") * velocity;
        movX *= Time.deltaTime;
        transform.Translate(movX, 0, 0);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRb.AddForce(Vector3.up * jump);
            isGrounded = false;
        }

        if (movX > 0)
        {
            transform.localScale = new Vector3(1f, 2f, 1f);
        }
        if (movX < 0)
        {
            transform.localScale = new Vector3(-1f, 2f, 1f);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }
}
