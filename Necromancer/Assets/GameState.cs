using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;
    public bool paused { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
    }
    public void UnPauseGame()
    {
        paused = false;
        Time.timeScale = 1;
    }
}
