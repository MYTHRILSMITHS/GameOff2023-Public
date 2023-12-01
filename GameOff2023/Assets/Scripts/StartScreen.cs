using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{

    private void Start()
    {
        AudioManager.instance.Play("Background2");
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
