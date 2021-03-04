using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static bool loaded = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (loaded)
        {
            Destroy(gameObject);
            return;
        }
        loaded = true;
        DontDestroyOnLoad(gameObject);
    }
}
