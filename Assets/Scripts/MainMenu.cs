using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void ExitGame()
    {
        Debug.Log("exiting game");
        Application.Quit();
    }
}
