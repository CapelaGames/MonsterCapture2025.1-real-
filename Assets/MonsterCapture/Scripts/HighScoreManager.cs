using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;

    private List<string> names = new List<string>();
    private List<int> scores = new List<int>();
    public int maxScoresCount = 10;

    private int currentScore = 0;
    public int CurrentScore
    {
        get => currentScore;
        private set
        {
            currentScore = value;
            uiTextScore.text = "Score: " + currentScore;
        }
    }

    public int highScore = 0;

    public TMP_Text uiTextScore;
    public TMP_Text uiTextHighscore;
    
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        HighscoreData data = JsonSaveLoad.Load();
        scores = data.scores.ToList();
        names = data.names.ToList();
        RefreshScoreDisplay();
    }

    public void OnDestroy()
    {
        HighscoreData data = new HighscoreData(scores.ToArray(), names.ToArray());
        JsonSaveLoad.Save(data);
    }

    public void IncreaseScore(int amount)
    {
        CurrentScore += amount;
    }

    public void RefreshScoreDisplay()
    {
        for (int i = 0; i < scores.Count; i++)
        {
            Debug.Log(names[i] + " got " + scores[i]);
        }
        
    }

    //AddHighScore is overloaded
    public void AddHighScore(int score)
    {
        string[] possibleNames = new[] {"Jim", "Jim man", "Idea man", "Good man", "Soup man", "Ghost Man", "Hat man", "Rock man" };
        string randomName = possibleNames[Random.Range(0, possibleNames.Length)];
        
        AddHighScore(randomName, score);
    }
    
    public void AddHighScore(string name, int score)
    {
        for (int i = 0; i < scores.Count; i++)
        {
            if (score > scores[i])
            {
                scores.Insert(i, score);
                names.Insert(i, name);

                return;
            }
        }
        
        //if(scores.Count <)
        scores.Add(score);
        names.Add(name);
    }
}
