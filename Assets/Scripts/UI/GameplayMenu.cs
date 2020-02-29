using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayMenu : MonoBehaviour
{
    private BGMController bgmController;
    //used for null exception protection
    private bool hasBGMController;

    private void Awake()
    {
        bgmController = GameObject.FindObjectOfType<BGMController>();
        //bgmController = GameObject.Find("BGMController").GetComponent<BGMController>();
        if (bgmController != null)
        {
            hasBGMController = true;
        }
        else
        {
            hasBGMController = false;
            Debug.LogWarning("Gameplay Menu did not find a BGMController");
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ChangeMusic(int trackNumber)
    {
        //Only changes the music if we have a reference to a BGMController so we can't accidentally cause a null reference exception
        if(hasBGMController)
            bgmController.ChangeMusicTrack(trackNumber);
    }

    public void QuitButton()
    {
        PlayerPrefs.Save();
        Application.Quit();
        Debug.Log("QuitGame button pressed");
    }

    //
    public void SaveHighScore(HighScore newHighScore)
    {
        HighScore[] highScores = SaveManager.LoadHighScores();
        highScores = HighScore.SortHighScores(newHighScore, highScores);
        SaveManager.SaveHighScores(highScores);
    }

    

    
}
