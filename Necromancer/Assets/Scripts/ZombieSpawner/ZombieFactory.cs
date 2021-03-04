using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFactory:MonoBehaviour,IZombieFactory
{
    private CurseDatabase cdb;
    void Start()
    {
        cdb = CurseDatabase.instance;
    }
    public GameObject CreateZombie(GameObject zombiePrefab,Transform newTransform)
    {
        GameObject newZombie = Instantiate(zombiePrefab, newTransform.position, newTransform.rotation, transform);
        Curse curse = cdb.GetRandomTrait();
        
        CurseInventory curseInventory = newZombie.GetComponent<CurseInventory>();
        
        curseInventory.UnlockCurse(curse);
        
        return newZombie;
    }
}
