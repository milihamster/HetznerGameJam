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

    public float popDuration = 1; 
    public Vector3 startScale = Vector3.zero;
    public Vector3 endScale = Vector3.one;

    void Start()
    {


        SettingsMenu.SetActive(false);
        Animation(GameName, 1);
        Animation(MainMenu, 2);




    }

    public void Animation(GameObject targetObject, float delay)
    {
        targetObject.transform.localScale = startScale;
        targetObject.SetActive(true);
        
        LeanTween.scale(targetObject, endScale, popDuration)
            .setEase(LeanTweenType.easeInOutElastic).setDelay(delay); 



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
