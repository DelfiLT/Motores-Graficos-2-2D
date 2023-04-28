using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player1 : MonoBehaviour
{
    public float velocity;
    public float jump;

    public bool isGrounded = true;

    private Rigidbody2D playerRb;
    private Animator playerAnim;
    private shoot shootScript;

    void Start()
    {
        shootScript = GameObject.FindGameObjectWithTag("shoot").GetComponent<shoot>();
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
    }

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
            playerAnim.SetBool("shoot", false);
        }

        if(!isGrounded)
        {
            shootScript.canFire = false;
        }

        if (movX == 0) 
        {
            playerAnim.SetBool("shootRun", false);
            playerAnim.SetBool("run", false);
        }

        if (movX > 0)
        {
            playerAnim.SetBool("shoot", false);
            playerAnim.SetBool("run", true);
            transform.localScale = new Vector3(5f, 5f, 1f);
        }
        if (movX < 0)
        {
            playerAnim.SetBool("shoot", false);
            playerAnim.SetBool("run", true);
            transform.localScale = new Vector3(-5f, 5f, 1f);
        }

        if (Input.GetMouseButton(0) && shootScript.canFire)
        {
            playerAnim.SetBool("shoot", true);
            shootScript.canFire = false;
            Instantiate(shootScript.bullet, shootScript.bulletTransform.position, Quaternion.identity);
        }

        if ((Input.GetKey(KeyCode.A) && Input.GetMouseButton(0)) || ((Input.GetKey(KeyCode.D) && Input.GetMouseButton(0)))) {
            playerAnim.SetBool("shootRun", true);
        }
        if ((movX > 0 && !(Input.GetMouseButton(0))) || (movX < 0 && !(Input.GetMouseButton(0))))
        {
            playerAnim.SetBool("shootRun", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            shootScript.canFire = true;
            playerAnim.SetBool("jump", false);
        }

        if(collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("mapEnd"))
        {
            SceneManager.LoadScene("End");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ship"))
        {
            SceneManager.LoadScene("End");
        }
    }
}
