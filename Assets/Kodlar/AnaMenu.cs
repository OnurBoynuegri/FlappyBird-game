using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnaMenu : MonoBehaviour
{
    public Text ScoreText,RekorText;
    void Start()
    {
        int rekor = PlayerPrefs.GetInt("RekorKayit");
        int score = PlayerPrefs.GetInt("ScoreKayit");
        RekorText.text = "Rekor = " + rekor;
        ScoreText.text = "Score = " + score;
    }
    
    void Update()
    {
        
    }
    public void OyunaGit()
    {
        SceneManager.LoadScene("FlappyBird");
    }
    public void OyundanCik()
    {
        Application.Quit();
    }
}
