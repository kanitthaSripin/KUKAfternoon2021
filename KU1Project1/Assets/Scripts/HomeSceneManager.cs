using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour
{
    public void GoToPlaygroung()
    {
        PlayerPrefs.SetInt("CoinCount",0);
        SceneManager.LoadScene("Playground1");
    }
    public void ContinuePlay()
    {
        if(PlayerPrefs.HasKey("PrevScene"))
        {
            string prevSceneName = PlayerPrefs.GetString("PrevScene");
            SceneManager.LoadScene(prevSceneName);
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
