using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire2 : MonoBehaviour
{
    [SerializeField] private float fireDamage;
    [SerializeField] private PlayerHealth pHealth;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("damage");
        pHealth.playerDamaged(fireDamage);

    }
}
