using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using BaseVariables;

public class HighScoreEntry : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TMP_InputField playerNameInput;
    [SerializeField]
    private FloatReference score;
    [SerializeField]
    private GameObject entryDisplay;

    public void Start()
    {
        //playerNameInput = GetComponentInParent<TMP_InputField>();
        
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.LogWarning(playerNameInput.text);
        HighScore highScore = new HighScore(playerNameInput.text, score);
        HighScore[] highScores = SaveManager.LoadHighScores();
        highScores = HighScore.AddNewHighScore(highScore, highScores);
        SaveManager.SaveHighScores(highScores);
        DebugMessages.MethodInClassDestroyObject(this, entryDisplay);
        Destroy(entryDisplay);
    }
}
