using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinCombat : MonoBehaviour
{
    [Header ("Numbers")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float attackBox;
    [SerializeField] private int damage;

    [Header ("Object_Variables")]
    [SerializeField] private BoxCollider2D coll;
    [SerializeField] private LayerMask layerPlayer;
    [SerializeField] private PlayerHealth pHealth;
    private SpriteRenderer sprite;

    [Header("Dagger Swing")]
    [SerializeField] private AudioClip daggerSound;

    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {

                cooldownTimer = 0;
                anim.SetTrigger("Attack");
            }
        }
        
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center + transform.right * transform.localScale.x * range, targetRange(), 0, Vector2.left, 0, layerPlayer);
        if(hit.collider != null)
        {
            pHealth = hit.transform.GetComponent<PlayerHealth>();
        }

        return hit.collider != null;

    }

    //adjust the targetting range of enemy
    private Vector2 targetRange()
    {
        return new Vector2(coll.bounds.size.x * attackBox, coll.bounds.size.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pHealth.playerDamaged(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(coll.bounds.center + transform.right * transform.localScale.x * range, targetRange());
    }

    private void DamagePlayer()
    {
        //player is in melee
        if (PlayerInSight())
        {
            pHealth.playerDamaged(damage);
        }
    }

    public void playDaggerSound()
    {
        AudioManager.instance.play(daggerSound);
    }
}
