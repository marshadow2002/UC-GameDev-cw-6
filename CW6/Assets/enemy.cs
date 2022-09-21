using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class enemy : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public Transform groundcheck;
    public float rad;
    public LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        patrol();
    }
   


    void patrol()
    {
        rb.velocity = new Vector2(speed, 0);
        bool isgrounded = Physics2D.OverlapCircle(groundcheck.position, rad, ground);
        if (!isgrounded)
        {
            transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y); ///AAA
            speed *= -1;
        }
    }
     void OnTriggerEnter2D(Collider2D collision)

    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(gameObject); ////small capital
        }
    }
 private void OnCollisionEnter2D(Collision2D collision)
{
        if (collision.gameObject.tag == "Player")

        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }

}
