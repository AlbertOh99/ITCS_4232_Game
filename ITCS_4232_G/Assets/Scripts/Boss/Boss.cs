using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Animator animState;
    //[SerializeField] private BoxCollider2D meleeAttack;
    [SerializeField] private Transform magicSpawn;
    [SerializeField] private BoxCollider2D magicAttack;
    Transform player;
    Rigidbody2D rb;
    [SerializeField] private float spd = 3f;
    [SerializeField] private float range = 5f;
    private Vector2 initxScale;

    private int meleeCounter;
    private int magicCounter;

    [SerializeField] private Magic magics;

    private enum AnimationState { idle, running, attack, cast, magic, cooldown}


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        initxScale = transform.localScale;
        meleeCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bossAI();
    }


    private void bossAI()
    {
        //when melee is <= 3 do melee attacks
        if(meleeCounter <= 2)
        {
            magicCounter = 0;
            faceTarget();
            //move to playerrm
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, spd * Time.fixedDeltaTime);

            rb.MovePosition(newPos);

            if (Vector2.Distance(player.position, rb.position) <= range)
            {
                animState.SetTrigger("attack");
            }
        } else 
        {
            
            animState.SetTrigger("magic");
            if(magicCounter >= 2)
            {
                meleeCounter = 0;
            }
        }
    }

    public void locatePlayer()
    {
        magicSpawn.position = player.position;
        magicSpawn.position = new Vector2(player.position.x, player.position.y + 1);
    }

    public void enableMagic()
    {
        locatePlayer();
        magics.onEnable();
        magics.anim.SetTrigger("magicAttack");
    }

    private void onOut()
    {
        animState.ResetTrigger("Attack");
        animState.ResetTrigger("magicAttack");
    }
    private void meleeTrigger()
    {
        meleeCounter += 1;
    }
    private void magicTrigger()
    {
        magicCounter += 1;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (magicAttack.CompareTag("Player"))
        {

        }
    }

    public void faceTarget()
    {
        if (transform.position.x > target.position.x)
        {
            transform.localScale = new Vector2(Mathf.Abs(initxScale.x), initxScale.y);
        }
        else
        {
            transform.localScale = new Vector2(-Mathf.Abs(initxScale.x), initxScale.y);
        }
    }
}
