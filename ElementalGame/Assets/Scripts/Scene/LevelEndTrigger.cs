using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    bool levelEnded = false;
    void Start()
    {
        EventManager.instance.OnLevelEndedTrigger += OnLevelEnd;
    }
    void OnLevelEnd()
    {
        levelEnded = true;
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == PlayerController.instance.Mover&&!collider.isTrigger)
        {
            if (levelEnded)
                SceneSwitcher.instance.LoadNextScene();
        }
    }
    void OnDestroy()
    {
        EventManager.instance.OnLevelEndedTrigger -= OnLevelEnd;
    }
}
