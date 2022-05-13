using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float maxPlayerHP;
    public float currPlayerHP;
    private Animator anim;
    private Rigidbody2D rb;

    [Header("IFrames")]
    [SerializeField] private float invulDuration;
    [SerializeField] private int invulFlash;
    private SpriteRenderer sprite;
    private bool invul;

    public bool dead { get; private set; } = false;

    private void Awake()
    {
        currPlayerHP = maxPlayerHP;
        sprite = GetComponent<SpriteRenderer>();
        invul = false;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void playerDamaged(float damage)
    {
        if(invul == true)
        {
            return;
        }
        currPlayerHP = Mathf.Clamp(currPlayerHP - damage, 0, maxPlayerHP);
        if(currPlayerHP > 0)
        {
            anim.SetTrigger("hurt");
            //create iframes
        } else
        {
            if (!dead)
            {
                Die();
            }
        }
    }
    private void Die()
    {
        anim.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Static;
        dead = true;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void invernerable()
    {
        if(invul == true)
        {
            invul = false;
        } else if (invul == false)
        {
            invul = true;
        }
    }
    
}