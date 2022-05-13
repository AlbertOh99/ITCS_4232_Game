using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [SerializeField] SpriteRenderer visible;
    [SerializeField] BoxCollider2D hitbox;
    [SerializeField] PlayerHealth pHealth;
    [SerializeField] private float magicDamage;
    [SerializeField] public Animator anim;

    [SerializeField] private AudioClip lightning;
    // Start is called before the first frame update
    void Start()
    {
        visible.enabled = false;
        hitbox.enabled = false;
    }


    private void OnDisable()
    {
        visible.enabled = false;
        hitbox.enabled = false;
    }
    public void onEnable()
    {
        visible.enabled = true;
    }
    public void enableHitBox()
    {
        hitbox.enabled = true;
    }
    public void disableHitBox()
    {
        hitbox.enabled = false;
    }

    private void Triggers()
    {
        anim.SetTrigger("magic");
    }



    public void playLIghtningSound()
    {
        AudioManager.instance.play(lightning);
    }

    private void Update()
    {
        if (pHealth != null)
        {
            pHealth.playerDamaged(magicDamage);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            pHealth = other.GetComponent<PlayerHealth>();

            other.GetComponent<PlayerHealth>().playerDamaged(magicDamage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pHealth = null;
        }
    }

}
