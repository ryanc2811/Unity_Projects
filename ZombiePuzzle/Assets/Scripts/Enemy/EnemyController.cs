using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onPlayerMoveTrigger += OnPlayerMove;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnPlayerMove(int id)
    {
        Debug.Log("Player "+id+ " Moved");
    }
    private void OnDestroy()
    {
        GameEvents.current.onPlayerMoveTrigger -= OnPlayerMove;
        Debug.Log("Enemy Dead");
    }
}
