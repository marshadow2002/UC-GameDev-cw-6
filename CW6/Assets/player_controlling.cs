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


    public GameObject Bullet;
    public float Bspeed;
    public float shoot;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        xfacing = GetComponent<SpriteRenderer>();
        shoot -= 1;
    }

    // Update is called once per frame
    void Update()
    {
        movex();
        movey();
        ShootBullet();
        shoot -= 2; //time
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
        if (grounded && Player.velocity.x == 0 && Player.velocity.y == 0 && shoot<0) //shoot from time
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
    void ShootBullet()  //capital 1st letter 
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayAnim(WALK_ANIM); 
            shoot = 2;
            
            GameObject Bulletclone = Instantiate(Bullet, transform.position, Quaternion.identity); //2nd one means the place of the script holder, 3rd one my rotation 4 numbers
            if (isRight)
            {
                Bulletclone.GetComponent<Rigidbody2D>().velocity = new Vector2(Bspeed, 0);
            }
            else
            {
                Bulletclone.GetComponent<Rigidbody2D>().velocity = new Vector2(-Bspeed, 0);
            }

            Destroy(Bulletclone, 3);
        }
    }
    
}
