using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFactory:MonoBehaviour,IZombieFactory
{
    private TraitDatabase tdb;
    void Start()
    {
        tdb = TraitDatabase.instance;
    }
    public GameObject CreateZombie(GameObject zombiePrefab,Transform newTransform)
    {
        GameObject newZombie = Instantiate(zombiePrefab, newTransform.position, newTransform.rotation, transform);
        Trait trait = tdb.GetRandomTrait();
        
        TraitInventory traitInventory = newZombie.GetComponent<TraitInventory>();
        //while (traitInventory.GetUniqueTraits().Contains(trait))
        //{
        //    trait = tdb.GetRandomTrait();
        //}
        traitInventory.AddTrait(trait);
        
        return newZombie;
    }
}
