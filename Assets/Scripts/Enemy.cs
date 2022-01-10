using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;


    private Animator anim;
    private Rigidbody2D rig;
    public Transform rightCol;
    public Transform leftCol;

    public Transform headPoint;

    public LayerMask whatIsGround;

    private bool colliding;

    public BoxCollider2D boxCollider;
    public CircleCollider2D circleCollider;




    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);

        // Detectando se inimigo está colidindo com algum objeto
        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, whatIsGround);



        if (colliding)
        {
            speed = -speed;
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
        }
    }

    bool playerDestroyed;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float height = collision.contacts[0].point.y - headPoint.position.y;

            if (height > 0 && !playerDestroyed)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                speed = 0;
                anim.SetTrigger("die");

                boxCollider.enabled = false;
                circleCollider.enabled = false;
                rig.bodyType = RigidbodyType2D.Static;

                Destroy(gameObject, 0.5f);
            }
            else
            {
                playerDestroyed = true;
                GameController.instance.ShowGameOver();
                Destroy(collision.gameObject);
            }
        }
    }
}
