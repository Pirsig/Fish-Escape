using UnityEngine;
using BaseVariables;
using TMPro;

public class FloatVariableDisplay : MonoBehaviour
{
    [SerializeField]
    private FloatVariable variableToDisplay;

    //the text box we want to display in
    private TMP_Text textBox;

    private void Start()
    {
        textBox = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        textBox.text = variableToDisplay.ToString();
    }
}
