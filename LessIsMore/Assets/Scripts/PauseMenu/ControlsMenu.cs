using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    public GameObject ControlMenu;
    public void GoBack()
    {
        ControlMenu.SetActive(false);
    }
}
