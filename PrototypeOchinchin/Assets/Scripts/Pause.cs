using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static Pause Instance { get; private set; }
    public bool GameIsEnd = false;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public  bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    
    

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void End()
    {
        Time.timeScale = 0f;
        GameIsEnd = true;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

}
