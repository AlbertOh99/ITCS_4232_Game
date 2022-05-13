using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    // Start is called before the first frame update


    public void nextLevel()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case ("Lose"):
                SceneManager.LoadScene("Level_1");
                return;
            case ("Win"):
                SceneManager.LoadScene("PostLevel1");
                return;
            case ("PostLevel1"):
                SceneManager.LoadScene("Explain2");
                return;
            default:
                break;

        }

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
