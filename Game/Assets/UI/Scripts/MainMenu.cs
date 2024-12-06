using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject mainmenu;
    public GameObject charactermenu;

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void GoToCharacterScene()
    {
        mainmenu.SetActive(false);
        charactermenu.SetActive(true);
    }
    public void GoToMainMenu()
    {
        mainmenu.SetActive(true);
        charactermenu.SetActive(false);
    }
    public void GoForward()
    {

    }
    public void GoBack() 
    {

    }
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has terminated");
    }
}
