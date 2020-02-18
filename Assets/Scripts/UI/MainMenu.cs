using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitButton()
    {
        PlayerPrefs.Save();
        Application.Quit();
        Debug.Log("QuitGame button pressed");
    }
}
