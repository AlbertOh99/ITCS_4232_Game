using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    [SerializeField] Animator attack;
    [SerializeField] Transform attackBox;
    [SerializeField] LayerMask enemy;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] GameObject Player;
    [Header("Sword Sound")]
    [SerializeField] private AudioClip swordSound;

    [Header("Hurt Sounds")]
    [SerializeField] private AudioClip[] hurtSounds;

    [Header("Death Sounds")]
    [SerializeField] private AudioClip[] deathSounds;

    [SerializeField] SpriteRenderer sprite;
    public int playerAttack = 20;
    float attackTimer = 0f;
    [SerializeField] float attackRate = 3f;

    private void Start()
    {
        Player = GetComponent<GameObject>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(Time.time >= attackTimer && attack.GetInteger("state") == 0) 
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Attack();
                attackTimer = Time.time + 1f / attackRate;
            }
        } else if (Time.time >= attackTimer && attack.GetInteger("state") == 1)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Attack();
                attackTimer = Time.time + 1f / attackRate;
            }
        }
        
    }

    void Attack()
    {

        //attacking stuff
        attack.SetTrigger("attack");
        if (sprite.flipX == true)
        {
            attackBox.localPosition = new Vector2(-attackRange, 0);
        }
        else
        {
            attackBox.localPosition = new Vector2(attackRange, 0);
        }
    }


    public void playerMelee()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackBox.position, attackRange, enemy);
        foreach (Collider2D enemies in hitEnemies)
        {
            enemies.GetComponent<EnemyDamage>().Damaged(playerAttack);
        }
    }

    public void playSwordSound()
    {
        AudioManager.instance.play(swordSound);
    }

    public void playHurtSound()
    {
        int rand = Random.Range(0, 10);
        AudioManager.instance.play(hurtSounds[rand]);
    }

    public void playDeathSound()
    {
        int rand = Random.Range(0, 10);
        AudioManager.instance.play(deathSounds[rand]);
    }
    private void OnDrawGizmosSelected()
    {
        if (attackBox == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackBox.position, attackRange);
    }
}
