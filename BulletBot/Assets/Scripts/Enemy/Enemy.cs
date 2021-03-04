using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected EnemyController controller;
    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<EnemyController>();
        Initialize();
    }
    protected abstract void Initialize();
    // Update is called once per frame
    void Update()
    {
        
    }
}
