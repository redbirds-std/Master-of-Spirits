using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : Singleton<GameControl>
{
    // Singletone
    public static GameControl _instance;

    //// Init
    void Awake()
    {
        _instance = this;
    }

    public void StartBattle()
    {
        SceneManager.LoadScene("Battle", LoadSceneMode.Single);
        return;
    }
}