using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GameName;
    public GameObject MainMenu;
    public GameObject SettingsMenu;


    void Start()
    {

        GameName.SetActive(true);
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);

        

    }


    public void PlayGame()
    {

        SceneManager.LoadScene("MaxTestingScene");

    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();

    }

    public void OpenSettings()
    {

        GameName.SetActive(true);
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);

    }
    public void CloseSettings()
    {
        GameName.SetActive(true);
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }


}
