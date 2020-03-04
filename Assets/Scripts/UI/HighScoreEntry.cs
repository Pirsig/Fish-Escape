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
        Debug.LogWarning("High score array before new AddNewHighScore");
        DebugMessages.ArrayVariableOutput<HighScore>(this, highScores, nameof(highScores));
        highScores = HighScore.AddNewHighScore(highScore, highScores);
        Debug.LogWarning("High score array after new AddNewHighScore");
        DebugMessages.ArrayVariableOutput<HighScore>(this, highScores, nameof(highScores));
        SaveManager.SaveHighScores(highScores);
        DebugMessages.MethodInClassDestroyObject(this, entryDisplay);
        Destroy(entryDisplay);
    }
}
