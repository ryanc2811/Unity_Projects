using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDatabase : MonoBehaviour
{
    public static SpellDatabase instance;
    public List<Spell> Skills;
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
    void Start()
    {
        
    }
    public Spell GetRandomTrait()
    {
        int index = Random.Range(0, Skills.Count);
        return Skills[index];
    }
}
