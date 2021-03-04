using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour
{
    private Grid<bool> grid;
    // Start is called before the first frame update
    void Start()
    {
        //grid = new Grid<bool>(10, 3,2f,new Vector3(-10,0),(Grid<bool>g,int x,int y)=>true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetGridObject(GetWorldPosition(),true);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetGridObject(GetWorldPosition()));
        }
    }

    private static Vector3 GetWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    private static Vector3 GetMouseWorldPositionWithZ(Vector3 mousePosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(mousePosition);
        return worldPosition;
    }
}
