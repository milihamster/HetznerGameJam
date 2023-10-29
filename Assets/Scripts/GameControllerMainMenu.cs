using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GameName;
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject Credits;

    public float popDuration = 1; 
    public Vector3 startScale = Vector3.zero;
    public Vector3 endScale = Vector3.one;

    void Start()
    {

        
        SettingsMenu.SetActive(false);
        Credits.SetActive(false);
        Animation(GameName, 0.5f);
        Animation(MainMenu, 1f);




    }

    public void Animation(GameObject targetObject, float delay)
    {
        targetObject.transform.localScale = startScale;
        targetObject.SetActive(true);

        LeanTween.scale(targetObject, endScale, 0.3f).setDelay(delay);




    }

    public void PlayGame()
    {
        LeanTween.scale(gameObject, endScale, 0.5f);
        SceneManager.LoadScene("MaxDONTTOUCHTHIS");

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
        SettingsMenu.transform.localScale = startScale;
        LeanTween.scale(MainMenu, startScale, 0.2f).setDelay(0.1f);
        LeanTween.scale(SettingsMenu, endScale, 0.2f).setDelay(0.1f);



    }
    public void CloseSettings()
    {
        GameName.SetActive(true);
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        SettingsMenu.transform.localScale = startScale;
        LeanTween.scale(SettingsMenu, startScale, 0.2f).setDelay(0.1f);
        LeanTween.scale(MainMenu, endScale, 0.2f).setDelay(0.1f);

    }


    public void OpenCredits()
    {

        GameName.SetActive(true);
        MainMenu.SetActive(false);
        Credits.SetActive(true);
        Credits.transform.localScale = startScale;
        LeanTween.scale(MainMenu, startScale, 0.2f).setDelay(0.1f);
        LeanTween.scale(Credits, endScale, 0.2f).setDelay(0.1f);



    }
    public void CloseCredits()
    {
        GameName.SetActive(true);
        MainMenu.SetActive(true);
        Credits.SetActive(false);
        Credits.transform.localScale = startScale;
        LeanTween.scale(Credits, startScale, 0.2f).setDelay(0.1f);
        LeanTween.scale(MainMenu, endScale, 0.2f).setDelay(0.1f);

    }


}
