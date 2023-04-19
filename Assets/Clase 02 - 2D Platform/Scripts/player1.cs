using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1 : MonoBehaviour
{
    public float velocity;
    public float jump;

    public bool isGrounded = true;

    private Rigidbody2D playerRb;
    private Animator playerAnim;
    void Start()
    {
        playerAnim = GetComponent<Animator>();
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
            playerAnim.SetBool("jump", true);
        }

        if (movX == 0) {
            playerAnim.SetBool("run", false);
        }

        if (movX > 0)
        {
            playerAnim.SetBool("run", true);
            transform.localScale = new Vector3(5f, 5f, 1f);
        }
        if (movX < 0)
        {
            playerAnim.SetBool("run", true);
            transform.localScale = new Vector3(-5f, 5f, 1f);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            playerAnim.SetBool("jump", false);
            isGrounded = true;
        }
    }
}
