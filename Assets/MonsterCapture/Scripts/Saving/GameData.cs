using UnityEngine;

[System.Serializable]
public class GameData
{
    public string data = "HELLO WORLD";
    public int funnyNumber = 1337;
    public double floaty = 420.42f;

    public Float3 position = new Float3();
}

[System.Serializable]
public class Float3
{
    public float x, y, z;

    public void FromVector(Vector3 v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
    }

    public Vector3 ToVector()
    {
        return new Vector3(x, y, z);
    }
}

[System.Serializable]
public class HighscoreData
{
    public int[] scores;
    public string[] names;

    // Constructor
    public HighscoreData()
    {
        scores = new[] {99, 10, 1};
        names = new[] { "Andrew", "Tom", "Steve" };
    }

    // Overloaded Constructor
    public HighscoreData(int[] scores, string[] names)
    {
        this.scores = scores;
        this.names = names;
        
    }
}