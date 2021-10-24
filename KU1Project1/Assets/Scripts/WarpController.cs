using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpController : MonoBehaviour
{
    public string sceneName;
    public AudioSource warpSound;

    private void OnTriggerEnter(Collider other)
    {
        Invoke("LoadNextScene",1f);
        PlayerPrefs.SetString("PrevScene",sceneName);
        var player = other.gameObject.GetComponent<PlayerControllerRigidbody>();
        PlayerPrefs.SetInt("CoinCount",player.coinCount);
        warpSound?.Play();
    }
    void LoadNextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
