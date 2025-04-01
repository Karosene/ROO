using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainClickScript : MonoBehaviour
{
    public GameObject gameOverScreen;

    public void PlayAgain(){
        gameOverScreen.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
