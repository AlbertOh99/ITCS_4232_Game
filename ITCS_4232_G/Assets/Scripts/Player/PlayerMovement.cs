using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("WalkSounds")]
    [SerializeField] private AudioClip[] walk;

    // Start is called before the first frame update
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator animState;
    private SpriteRenderer sprite;
    [SerializeField] private PlayerHealth pHealth;
    public bool isFlipped;
    private bool move;
    [SerializeField] private LayerMask ground;

    [SerializeField] float jumpHeight = 14f;
    [SerializeField] float moveSpeed = 7f;
    private float dirX = 0f;

    private enum AnimationState {idle, running, jumping, falling}
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animState = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(pHealth.dead == true)
        {
            return;
        }
        if (move == false) return;
        
        dirX = Input.GetAxisRaw("Horizontal"); //left = -1, right = 1
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && grounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
        updateAnimationState();


    }

    private void updateAnimationState()
    {
        AnimationState state;
        //animation state
        if (dirX > 0f)
        {
            state = AnimationState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = AnimationState.running;
            sprite.flipX = true;
        }
        else
        {
            state = AnimationState.idle;
        }

        if (rb.velocity.y > .01f)
        {
            state = AnimationState.jumping;
        } else if (rb.velocity.y < -.01f)
        {
            state = AnimationState.falling;
        }

        animState.SetInteger("state", (int)state);
    }
    private bool grounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, ground);
    }

    public void walkSound()
    {
        int rand = Random.Range(0, 5);
        AudioManager.instance.play(walk[rand]);
    }


    private void stopMove()
    {
        move = false;
    }

    private void letMove()
    {
        move = true;
    }
}