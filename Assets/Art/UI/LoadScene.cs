using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    // Start is called before the first frame update
    public void StartGame()
    {
        Debug.Log("Button gots pressed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RevealWarnings()
    {
        //reveal dem
       gameObject.SetActive(true);

    }
    public void UnRevealWarnings()
    {
        //reveal dem
        gameObject.SetActive(false);

    }

    public void QuitGame()
    {
        Debug.Log("quit hit");
        Application.Quit();
        
    }
}