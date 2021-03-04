using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSoundLineSpawner:MonoBehaviour
{
    float timeSinceLastStep;
    [SerializeField] private float soundDelay = 3f;
    [SerializeField] private SoundLineSpawner lineSpawner;
    void Update()
    {
        if (Time.time > timeSinceLastStep + soundDelay)
        {
            timeSinceLastStep = Time.time;
            lineSpawner.SpawnLines();
        }
    }
}
