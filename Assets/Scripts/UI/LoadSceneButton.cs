using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    private BGMController bgmController;

    private void Awake()
    {
        bgmController = GameObject.Find("BGMController").GetComponent<BGMController>();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ChangeMusic(int trackNumber)
    {
        bgmController.ChangeMusicTrack(trackNumber);
    }

    public void QuitButton()
    {
        PlayerPrefs.Save();
        Application.Quit();
        Debug.Log("QuitGame button pressed");
    }
}
