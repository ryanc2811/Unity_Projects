using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MorphSpawner : MonoBehaviour
{
    public static MorphSpawner Instance;

    
    public MorphObject[]morphObjects;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void Spawn(MorphType morph, Vector2 position)
    {
        Instantiate(FindMorph(morph), position, Quaternion.identity);
        
    }

    private GameObject FindMorph(MorphType morph)
    {
        
        foreach(MorphObject morphObject in morphObjects)
        {
            if (morphObject.morphType == morph)
            {
                return morphObject.Prefab;
            }
        }
        return null;
    }
}
