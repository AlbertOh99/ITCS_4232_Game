using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GoToMainMenu : MonoBehaviour
{
    public void instructions()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
