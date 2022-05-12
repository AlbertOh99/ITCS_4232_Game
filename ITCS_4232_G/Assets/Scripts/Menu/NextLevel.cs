using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public float playerHealth;
    public int healthPots;

    public void newGame()
    {
        PlayerPrefs.SetFloat("PlayerHealth", playerHealth);
        PlayerPrefs.SetInt("HealthPot", healthPots);
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            nextLevel();
        }
    }
}
