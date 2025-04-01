using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class MainManagerScript : MonoBehaviour
{
    public GameObject gameOverScreen;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }
}
