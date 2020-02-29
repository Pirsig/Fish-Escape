using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HighScore
{
    public string name;
    public float score;
    
    /*public HighScore()
    {
        name = "";
        score = 0;
    }*/

    public HighScore(float newScore)
    {
        name = "";
        score = newScore;
    }

    public HighScore(string newName, float newScore)
    {
        name = newName;
        score = newScore;
    }

    public static HighScore[] SortHighScores(HighScore newHighScore, HighScore[] highScores)
    {
        HighScore scoreToCompare = newHighScore;
        int index = 0;
        while (index < highScores.Length)
        {
            if (scoreToCompare.score > highScores[index].score)
            {
                HighScore temp = highScores[index];
                highScores[index] = scoreToCompare;
                scoreToCompare = temp;
            }
            index++;
        }
        return highScores;
    }

    /*
     * Returns true if the newHighScore is larger than any of the scores in highScores, otherwise returns false
     */
    public static bool IsHighScoreNew(HighScore newHighScore, HighScore[] highScores)
    {
        HighScore scoreToCompare = newHighScore;
        bool newScoreIsBigger = false;
        int index = 0;
        while (index < highScores.Length)
        {
            if (scoreToCompare.score > highScores[index].score)
            {
                newScoreIsBigger = true;
                break;
            }
            index++;
        }
        return newScoreIsBigger;
    }

    //Overload to take in a plain float value for newHighScore
    public static bool IsHighScoreNew(float newHighScore, HighScore[] highScores)
    {
        bool newScoreIsBigger = false;
        int index = 0;
        while (index < highScores.Length)
        {
            if (newHighScore > highScores[index].score)
            {
                newScoreIsBigger = true;
                break;
            }
            index++;
        }
        return newScoreIsBigger;
    }

    public override string ToString()
    {
        return name + ' ' + score.ToString();
    }
}
