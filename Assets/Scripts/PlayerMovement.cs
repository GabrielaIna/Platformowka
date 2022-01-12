using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    //[SerializeField] private int hp = 3;

    //dla u³atwienia nazwy identyczne do odpowiadaj¹cych im animacji
    private enum MovementState { idle, running, jumping, falling } 

    [SerializeField] private AudioSource jumpSoundEffect;

    [SerializeField] private AudioSource deathSoundEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        //ruch z uwzglêdnieniem skoku
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            //skok z uwzglêdnieniem ruchu
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }


        UpdateAnimationState();

        //if(hp==0)
        //{
        //    Die();
        //}
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            //obrót obiektu gdy idzie w lewo
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        //w³¹czenie skok animacji
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        //w³¹czenie animacji spadania
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    //aby skok by³ mo¿liwy tylko dgy obiekt ma zadeklarowan¹ warstwê jumpableGround
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    //jeœli dotknie do obiektu
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
