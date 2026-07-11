using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public bool PausedGame;
    public GameObject PauseGameMenu;
    public bool PlayerIsDead;
    public GameObject PlayerDead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (PausedGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        PausedGame = false;
    }

    public void Pause()
    {
        PauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        PausedGame = true;
    }

   /* public void Dead()
    {
        PlayerDead.SetActive(false);
        Time.timeScale = 1f;
        PausedGame = false;
    }*/

   
}
