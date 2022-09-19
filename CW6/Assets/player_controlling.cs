using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controlling : MonoBehaviour
{
    Animator animator;
    Rigidbody2D Player;
    SpriteRenderer xfacing;
    public float jspeed;
    public float speed;
    bool grounded;
    public Transform groundcheck; //insert empty on foot
    public float rad;
    public LayerMask ground;
    bool isRight = true;

    string currentanim;
    const string IDLE_ANIM = "idle";//the name of the animation   //all the letters are capital so people that they are constant and must not be chamged
    const string WALK_ANIM = "walk";
    const string JUMP_ANIM = "jump";
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        xfacing = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movex();
        movey();
    }
    void movex()
    {
        Player.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Player.velocity.y);
        if (!isRight && Input.GetKey(KeyCode.D))
        {
            isRight = true;
            xfacing.flipX = false;
        }
        else if (isRight && Input.GetKey(KeyCode.A))
        {
            isRight = false;
            xfacing.flipX = true;
        }
        if (grounded && Player.velocity.x == 0 && Player.velocity.y == 0)
        {

            PlayAnim(IDLE_ANIM);

        }

        else if (grounded && Player.velocity.x != 0 && Player.velocity.y == 0)

        {

            PlayAnim(WALK_ANIM);

        }

    }
    void movey()
    {
        
        grounded = Physics2D.OverlapCircle(groundcheck.position, rad, ground);
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Player.AddForce(new Vector2(0, jspeed), ForceMode2D.Impulse);
            PlayAnim(JUMP_ANIM);
        }
   

    }
    void PlayAnim(string nextAnim)
    {
        if (currentanim == nextAnim)
        {
            return;


        }
        animator.Play(nextAnim); /////play
        currentanim = nextAnim;


    }
}
