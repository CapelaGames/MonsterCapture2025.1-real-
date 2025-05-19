using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class HighScoreManager : MonoBehaviour
{
    #region Variables
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

    public GameObject scoresParent;
    public TMP_Text scorePrefab;
    #endregion

    #region Unity Functions
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
        CleanUpHighScores();
        RefreshScoreDisplay();
    }

    public void OnDestroy()
    {
        AddHighScore(CurrentScore);
        HighscoreData data = new HighscoreData(scores.ToArray(), names.ToArray());
        JsonSaveLoad.Save(data);
    }
    #endregion
    public void IncreaseScore(int amount)
    {
        CurrentScore += amount;
    }

    public void RefreshScoreDisplay()
    {
        DestroyAllChildren(scoresParent);
        for (int i = 0; i < scores.Count; i++)
        {
            TMP_Text uiText = Instantiate(scorePrefab,scoresParent.transform);
            uiText.text = names[i] + "  " + scores[i];
        }
    }
    
    private void DestroyAllChildren(GameObject parent)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>(true);
        for (int i = children.Length - 1; i >= 0 ; i--)
        {
            if(children[i] == parent.transform) continue;
            Destroy(children[i].gameObject);
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
        CleanUpHighScores();
        
        for (int i = 0; i < scores.Count; i++)
        {
            if (score > scores[i])
            {
                scores.Insert(i, score);
                names.Insert(i, name);

                return;
            }
        }
        if (scores.Count < maxScoresCount)
        {
            scores.Add(score);
            names.Add(name);
        }
    }

    void CleanUpHighScores()
    {
        for (int i = maxScoresCount; i < scores.Count; i++)
        {
            names.RemoveAt(i);
            scores.RemoveAt(i);
        }
    }
}
