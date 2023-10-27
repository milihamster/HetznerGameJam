using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject inGameMenu;

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
        pauseScreen.SetActive(true);
        inGameMenu.SetActive(false);
        Time.timeScale = 0f;
        GameIsRunning = false;
    }
    public void Resume(){
        pauseScreen.SetActive(false);
        inGameMenu.SetActive(true);
        Time.timeScale = 1f;
        GameIsRunning = true;
    
    }    
}
