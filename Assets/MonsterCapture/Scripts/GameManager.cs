using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().gameObject;
    }
    
    void Update()
    {
        if (player.transform.position.y < -10)
        { 
            HighScoreManager.instance.AddHighScore(HighScoreManager.instance.CurrentScore); 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
