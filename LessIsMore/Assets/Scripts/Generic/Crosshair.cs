using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    CharacterController2D chrctrl;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject Player = GameObject.FindGameObjectWithTag("Player");
        //chrctrl = Player.GetComponent<CharacterController2D>();
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        chrctrl = g.GetComponent<CharacterController2D>();
        rend =transform.GetComponent<Renderer>();
    }
    void Aim()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePos.x, mousePos.y);
        Vector3 aimDirection = (mousePos - chrctrl.transform.position).normalized;
        chrctrl.direction = aimDirection;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(InventoryUI.InventoryActive);
        //if the mouse is over ignored UI
        //if (UIHandler.Instance.IsMouseOverIgnoredUI()||InventoryUI.InventoryActive)
        //{
        //    //display the cursor
        //    Cursor.visible = true;
        //    //stop rendering the crosshair
        //    rend.enabled = false;
        //}

        //else if(!UIHandler.Instance.IsMouseOverIgnoredUI()&& !PauseMenu.gamePaused&&!InventoryUI.InventoryActive)
        //{
        //   
        //}
        Aim();
          Cursor.visible = false;
          rend.enabled = true;
    }
}
