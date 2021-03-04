using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitch : MonoBehaviour
{
    bool hideButtons = false;

    public Button[] buttonArray;
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        Debug.Log(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void HideButtons()
    {
        hideButtons = !hideButtons;
        if (hideButtons)
            foreach (Button button in buttonArray)
            {
                button.gameObject.SetActive(false);
            }
        else
            foreach (Button button in buttonArray)
            {
                button.gameObject.SetActive(true);
            }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            HideButtons();
        }
    }
}
