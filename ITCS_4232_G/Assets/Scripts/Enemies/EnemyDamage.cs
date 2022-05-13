using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int maxHealth = 100;
    [SerializeField] Animator anim;
    [SerializeField] BoxCollider2D bc;
    [SerializeField] AudioClip hurt;

    private bool invul = false;
    private int currHealth;
    private Rigidbody2D rb;

    void Start()
    {
        currHealth = maxHealth;
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Damaged(int damage)
    {
        if (invul == true) return;

        currHealth -= damage;
        AudioManager.instance.play(hurt);
        anim.SetTrigger("Hit");
        if(currHealth <= 0)
        {
            EnemyDie();
        }
    }

    void EnemyDie()
    {
        Debug.Log("Enemy Died!");
        anim.SetTrigger("Death");
        bc.enabled = false;
        //rb.bodyType = RigidbodyType2D.Static;
        Destroy(bc.gameObject, 1f);
    }

    private void invulnerable()
    {
        if (invul == true)
        {
            invul = false;
        }
        else if (invul == false)
        {
            invul = true;
        }
    }

    private void vulnerable()
    {
        invul = false;
    }
    private void disableShield()
    {
        invul = false;
    }

}
