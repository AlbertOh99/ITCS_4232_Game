using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Values : MonoBehaviour
{
    public float playerHealth;
    public int healthPots;

    public void newGame()
    {
        PlayerPrefs.SetFloat("PlayerHealth", playerHealth);
        PlayerPrefs.SetInt("HealthPot", healthPots);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
