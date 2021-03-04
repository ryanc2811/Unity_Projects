using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMeat : MonoBehaviour
{
    public GameObject monsterMeatPrefab;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)&&PlayerStats.Instance.Meat>0)
        {
            Instantiate(monsterMeatPrefab, transform.position, Quaternion.identity);
            PlayerStats.Instance.RemoveMeat(1);
        }
    }
}
