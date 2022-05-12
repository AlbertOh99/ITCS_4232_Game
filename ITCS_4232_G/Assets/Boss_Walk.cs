using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Walk : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float spd = 3f;
    public float range = 5f;
    Boss boss;
    //onstateenter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    //onstateupdate is called on each update frame between onstateenter and onstateexit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        boss.faceTarget();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, spd * Time.fixedDeltaTime);

        rb.MovePosition(newPos);

        if(Vector2.Distance(player.position, rb.position) <= range)
        {
            animator.SetTrigger("attack");
        }

    }

    //onstateexit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        animator.ResetTrigger("attack");
    }
}
