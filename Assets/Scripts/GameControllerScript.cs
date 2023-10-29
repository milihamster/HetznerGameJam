using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject inGameMenu;
    public GameObject gameOver;

    public float popDuration = 1;
    public Vector3 startScale = Vector3.zero;
    public Vector3 endScale = Vector3.one;

    public bool GameIsRunning = true;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            ToggleGameMenu();
            
        }
        
    }
    
    public void ToggleGameMenu(){
        if(GameIsRunning){
            Pause();
        }
        else {
            Resume();
        } 
        
    
    }
    
    public void Pause(){
        Cursor.visible = true;
        pauseScreen.SetActive(true);
        inGameMenu.SetActive(false);

        inGameMenu.transform.localScale = startScale;
        pauseScreen.transform.localScale = endScale;

        LeanTween.scale(pauseScreen, startScale, 0.1f).setDelay(0.1f);
        LeanTween.scale(inGameMenu, endScale, 0.1f).setDelay(0.1f);

        Time.timeScale = 0f;
        GameIsRunning = false;
    }
    public void Resume(){
        Cursor.visible = false;
        pauseScreen.SetActive(false);
        inGameMenu.SetActive(true);

        inGameMenu.transform.localScale = startScale;
        pauseScreen.transform.localScale = endScale;

        LeanTween.scale(pauseScreen, startScale, 0.1f).setDelay(0.1f);
        LeanTween.scale(inGameMenu, endScale, 0.1f).setDelay(0.1f);

        Time.timeScale = 1f;
        GameIsRunning = true;
    
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
        Cursor.visible = true;
    }



    public void Reset()
    {
        SceneManager.LoadScene("Game");
        Cursor.visible = false;
    }

    public void GameOver()
    {

        Cursor.visible = true;
        inGameMenu.SetActive(false);
        gameOver.SetActive(true);

        gameOver.transform.localScale = startScale;
        inGameMenu.transform.localScale = endScale;

        LeanTween.scale(inGameMenu, startScale, 0.1f).setDelay(0.1f);
        LeanTween.scale(gameOver, endScale, 0.1f).setDelay(0.1f);

        Time.timeScale = 1f;
        GameIsRunning = true;


    }

}
