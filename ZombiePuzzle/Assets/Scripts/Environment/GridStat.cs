using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStat : MonoBehaviour
{
    public int visited = -1;
    public int x = 0;
    public int y = 0;
    public bool occupied = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        occupied = true;
    }
    void OnTriggerExit(Collider other)
    {
        occupied = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
