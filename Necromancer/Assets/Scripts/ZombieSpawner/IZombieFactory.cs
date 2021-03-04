using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IZombieFactory 
{
    GameObject CreateZombie(GameObject prefab,Transform newTransform);
}
