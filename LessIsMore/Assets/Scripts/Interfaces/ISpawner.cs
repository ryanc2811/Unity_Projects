using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    /// <summary>
    /// SPAWNS THE GAME OBJECT INTO THE WORLD
    /// </summary>
    /// <param name="SpawnableObject"></param>
    void Spawn(GameObject SpawnableObject);
}
