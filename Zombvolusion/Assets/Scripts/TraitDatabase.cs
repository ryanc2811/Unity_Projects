using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitDatabase : MonoBehaviour
{
    public static TraitDatabase instance;
    public List<Trait> Traits;
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

    public Trait GetRandomTrait()
    {
        int index = Random.Range(0, Traits.Count);
        return Traits[index];
    }
}
