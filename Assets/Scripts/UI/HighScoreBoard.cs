using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreBoard : MonoBehaviour
{
    private HighScore[] highScores;

    public GameObject HighScoreDisplay;
    public Vector3 initialDisplayLocation;
    //The amount to add to the y value of initialDisplayLocation for each subsequent entry
    public float displayYOffset;


    //In order to display correctly all of the high scores need to be parented to this
    private Transform backgroundObject;

    private void Start()
    {
        backgroundObject = gameObject.transform.Find("Background");
        DisplayHighScores();
    }

    private void DisplayHighScores()
    {
        try
        {
            highScores = SaveManager.LoadHighScores();
        }
        catch (System.IO.FileNotFoundException exception)
        {
            Debug.LogWarning("No save file was found for high scores");
            Debug.LogWarning(exception);
            return;
        }

        Vector3 displayPosition = initialDisplayLocation;

        int index = 0;
        while (index < highScores.Length)
        {
            //if there are no more high scores we stop creating entries on the screen
            if (highScores[index].score == 0)
                break;

            //create an individual highScore entry on the screen
            GameObject currentDisplay = Instantiate(HighScoreDisplay, backgroundObject);
            currentDisplay.GetComponent<RectTransform>().anchoredPosition = displayPosition;

            //Changes all the text elements in currentDisplay to show the information from the current element of highScores
            setHighScoreDisplay(currentDisplay, highScores[index], index + 1);

            //update index and display position for the next iteration
            index++;
            displayPosition.y += displayYOffset;
        }
    }

    public void ResetHighScores()
    {
        SaveManager.ResetHighScores();
    }

    public void setHighScoreDisplay(GameObject displayToSet, HighScore highScore, int position)
    {
        displayToSet.transform.Find("Position").GetComponent<TMP_Text>().SetText(position + ":");
        displayToSet.transform.Find("Score").GetComponent<TMP_Text>().SetText(highScore.score.ToString());
        displayToSet.transform.Find("Name").GetComponent<TMP_Text>().SetText(highScore.name);
    }

}
