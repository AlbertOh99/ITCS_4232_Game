using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeMovement : MonoBehaviour
{
    private Animator anim;
    //tracking movement
    [Header("Tracking")]
    [SerializeField] private Transform target;
    [SerializeField] private Transform playerLoc;
    [SerializeField] float minimumDis;
    [SerializeField] float maximumDis;
    //normal movement
    [Header("Basic Movement")]
    [SerializeField] private float speed;
    private Vector2 startPos;
    private Vector2 initxScale;

    [Header("Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;
    private float dashTimer;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    private SpriteRenderer sprite;
    private bool movingLeft;
    private bool savePosition = true;
    private bool dashing = false;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        initxScale = enemy.localScale;
        startPos = transform.position;
        anim = GetComponent<Animator>();
        dashTimer = dashTime;
        dashCooldownTimer = 0;
        
    }
    // Update is called once per frame
    void Update()
    {
        tracking();
    }

    private void tracking()
    {
        dashCooldownTimer -= Time.deltaTime;
        if (Vector2.Distance(transform.position, target.position) > minimumDis && Vector2.Distance(transform.position, target.position) < maximumDis)
        {
            //anim.SetBool("Running", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            //facing player
            faceTarget();
            if(dashing == true)
            {
                dashCooldownTimer = dashCooldown;
                dashTimer = dashTime;
                savePosition = true;
                dashing = false;
            }

            return;

        }
        else if (Vector2.Distance(transform.position, target.position) < minimumDis)
        {
            //attack player, this is done in goblin combat
            //this else if is to stop the enemy from moving
            //disable movement animation
            dash();
            return;
        }
        else
        {
            //go back to original position
            facePosition();
            transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
            if (dashing == true)
            {
                dashCooldownTimer = dashCooldown;
                dashTimer = dashTime;
                savePosition = true;
                dashing = false;
            }
            return;
        }
    }



    private void dash()
    {
        dashing = true;
        faceDash();
        if (dashCooldownTimer >= 0)
        {
            return;
        }
        if(dashTimer > 0)
        {
            if (savePosition == true)
            {
                playerLoc.position = target.position;
                print("save position");
                savePosition = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, playerLoc.position, dashSpeed * Time.deltaTime);
            dashTimer -= Time.deltaTime;
            //print("Dash");


            if(transform.position == playerLoc.position)
            {
                dashCooldownTimer = dashCooldown;
                dashTimer = dashTime;
                savePosition = true;
                dashing = false;
            }
            return;
        }
        dashCooldownTimer = dashCooldown;
        dashTimer = dashTime;
        savePosition = true;
        dashing = false;


    }
    private Transform getDashDirection()
    {
        Transform tempPos = target;
        return tempPos;
    }

    private void faceTarget()
    {
        if (transform.position.x < target.position.x)
        {
            enemy.localScale = new Vector2(Mathf.Abs(initxScale.x), initxScale.y);
        }
        else
        {
            enemy.localScale = new Vector2(-Mathf.Abs(initxScale.x), initxScale.y);
        }
    }

    private void faceDash()
    {
        if (transform.position.x < playerLoc.position.x)
        {
            enemy.localScale = new Vector2(Mathf.Abs(initxScale.x), initxScale.y);
        }
        else
        {
            enemy.localScale = new Vector2(-Mathf.Abs(initxScale.x), initxScale.y);
        }
    }

    private void facePosition()
    {
        if (transform.position.x < startPos.x)
        {
            enemy.localScale = new Vector2(Mathf.Abs(initxScale.x), initxScale.y);
        }
        else
        {
            enemy.localScale = new Vector2(-Mathf.Abs(initxScale.x), initxScale.y);
        }
    }
}
