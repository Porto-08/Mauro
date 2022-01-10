using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float Speed;
    public float JumpForce;
    public float DoubleJumpForce;

    private bool isJumping;
    private bool doubleJumping;

    // variaveis de componetes da Unity
    private Rigidbody2D rig;
    private Animator anim;




    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        //Vector3 movement = new Vector3(movement, 0f, 0f);

        // move o persoangem em uma posição horizontal (não usa fisica)
        // transform.position += movement * Time.deltaTime * Speed;

        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * Speed, rig.velocity.y);

        
        if (movement > 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0, 0, 0);

        }

        if (movement < 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (movement == 0f)
        {
            anim.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);

                doubleJumping = true;

                anim.SetBool("jump", true);
            }
            else
            {
                if (doubleJumping)
                {
                    rig.AddForce(new Vector2(0f, JumpForce * DoubleJumpForce), ForceMode2D.Impulse);
                    doubleJumping = false;

                    anim.SetBool("jump", true);

                }
            }
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }

        if (collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
            
            GameController.instance.ShowGameOver();
        }

        // if (collision.gameObject.layer == 11)
        // {
        //     Destroy(gameObject);

        //     GameController.instance.ShowGameOver();
        // }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }
}
