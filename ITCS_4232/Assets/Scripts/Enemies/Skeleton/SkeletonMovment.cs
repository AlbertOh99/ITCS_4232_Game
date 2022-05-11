using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovment : MonoBehaviour
{
    private Animator anim;
    //tracking movement
    [Header("Tracking")]
    [SerializeField] private Transform target;
    [SerializeField] float minimumDis;
    [SerializeField] float maximumDis;
    //normal movement
    [Header("Basic Movement")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private float speed;
    private Vector2 startPos;
    private Vector2 initxScale;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    private bool stateAttacking;
    private SpriteRenderer sprite;
    private bool movingLeft;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        initxScale = enemy.localScale;
        startPos = transform.position;
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        tracking();
    }

    private void tracking()
    {
        if (Vector2.Distance(transform.position, target.position) > minimumDis && Vector2.Distance(transform.position, target.position) < maximumDis)
        {
            if (stateAttacking == true) return;
            anim.SetBool("Running", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            //keep goblin on y scale
            transform.position = new Vector2(transform.position.x, startPos.y);
            //facing player
            faceTarget();

        }
        else if (Vector2.Distance(transform.position, target.position) < minimumDis)
        {
            //attack player, this is done in goblin combat
            //this else if is to stop the enemy from moving
            //disable movement animation
            faceTarget();
            OnDisable();
        }
        else
        {
            anim.SetBool("Running", true);
            //simple patrolling
            simpleMovement();
            //after going to tracking the object should go back
        }
    }

    private void faceTarget()
    {
        if (stateAttacking == true)
        {
            return;
        }
        if (transform.position.x < target.position.x)
        {
            enemy.localScale = new Vector2(Mathf.Abs(initxScale.x), initxScale.y);
        }
        else
        {
            enemy.localScale = new Vector2(-Mathf.Abs(initxScale.x), initxScale.y);
        }
    }

    //basic patrolling movement
    private void simpleMovement()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                moveDirection(-1);
            }
            else
            {
                direction();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                moveDirection(1);
            }
            else
            {
                direction();
            }
        }
    }

    private void direction()
    {
        movingLeft = !movingLeft;
    }

    private void moveDirection(int dir)
    {
        //facing
        enemy.localScale = new Vector2(Mathf.Abs(initxScale.x) * dir, initxScale.y);
        //move in direction
        enemy.position = new Vector2(enemy.position.x + Time.deltaTime * dir * speed, enemy.position.y);
    }

    private void OnDisable()
    {
        anim.SetBool("Running", false);
    }
    private void attacking()
    {
        if (stateAttacking == false)
        {
            stateAttacking = true;
        } else
        {
            stateAttacking = false;
        }

    }

    private void stateDefault()
    {
        stateAttacking = false;
    }



}
