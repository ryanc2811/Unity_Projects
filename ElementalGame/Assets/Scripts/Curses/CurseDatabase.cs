using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseDatabase : MonoBehaviour
{
    public static CurseDatabase instance;
    public List<Curse> Curses;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Curse GetRandomTrait()
    {
        int index = Random.Range(0, Curses.Count);
        return Curses[index];
    }
}
