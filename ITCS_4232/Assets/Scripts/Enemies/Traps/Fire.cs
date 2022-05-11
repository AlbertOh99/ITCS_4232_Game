using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float fireDamage;

    private PlayerHealth pHealth;

    private void Start()
    {

    }

    private void Update()
    {
        if(pHealth != null)
        {
            pHealth.playerDamaged(fireDamage);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "Player")
        {
            pHealth = other.GetComponent<PlayerHealth>();
            
            other.GetComponent<PlayerHealth>().playerDamaged(fireDamage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            pHealth = null;
        }
    }

}
