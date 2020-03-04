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

    /*
    public static void SaveHighScore(HighScore newHighScore)
    {
        HighScore[] highScores = SaveManager.LoadHighScores();
        highScores = HighScore.SortHighScores(newHighScore, highScores);
        SaveManager.SaveHighScores(highScores);
    }*/

    //Deactivate all other objects tagged UIPanel and activate UIPanelToOpen
    public void OpenUIPanel(GameObject UIPanelToOpen)
    {
        GameObject[] UIPanels = GameObject.FindGameObjectsWithTag("UIPanel");
        int index = 0;
        //If there are no open ui panels we skip closing any
        if(UIPanels != null)
        {
            Debug.LogWarning("UIPanel was not null.");
            //close all UIPanels that are not the panel we intend to open in case it is already open
            while (index < UIPanels.Length)
            {
                if (UIPanels[index] != UIPanelToOpen)
                {
                    UIPanels[index].SetActive(false);
                }
                index++;
            }
        }
        
        UIPanelToOpen.SetActive(true);
    }
    

    
}
