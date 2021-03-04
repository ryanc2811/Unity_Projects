using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScanner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.OnScanRequiredTrigger += ScanPath;
    }

    void ScanPath()
    {
        GetComponent<AstarPath>().Scan();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy()
    {
        EventManager.instance.OnScanRequiredTrigger -= ScanPath;
    }
}
