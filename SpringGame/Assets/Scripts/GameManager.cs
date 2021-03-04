using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuGO;
    private PauseMenu pauseMenu;
    [SerializeField]
    private GameObject pauseButton;
    private bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        if (pauseMenuGO)
        {
            pauseMenu = pauseMenuGO.GetComponentInChildren<PauseMenu>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenuGO)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = !paused;
                pauseMenuGO.SetActive(paused);
                pauseButton.SetActive(!paused);
                if (paused)
                {
                    pauseMenu.PauseGame();
                }
                else
                {
                    pauseMenu.ResumeGame();
                }
            }
        }
    }
}
