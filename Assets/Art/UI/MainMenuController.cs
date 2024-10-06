using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    
    public void PlayGame()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("button start pressed");
        
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit hit");
    }
}
