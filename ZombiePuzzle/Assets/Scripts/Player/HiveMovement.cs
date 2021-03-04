using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMovement : MonoBehaviour
{
    HiveMind hive;
    string up = "Up", down = "Down", left = "Left", right = "Right";
    // Start is called before the first frame update
    void Start()
    {
        hive = GetComponent<HiveMind>();
    }

    // Update is called once per frame
    void Update()
    {
        //    List<Zombie> zombies = hive.Zombies;
        //    if (Input.GetKey(KeyCode.W))
        //    {
        //        for(int i = 0; i < zombies.Count; i++)
        //        {
        //            zombies[i].Move(up);
        //            Debug.Log("Up");
        //        }
        //    }
        //    else if (Input.GetKey(KeyCode.S))
        //    {
        //        for (int i = 0; i < zombies.Count; i++)
        //        {
        //            zombies[i].Move(down);
        //            Debug.Log("Down");
        //        }
        //    }
        //   else if (Input.GetKey(KeyCode.A))
        //    {
        //        for (int i = 0; i < zombies.Count; i++)
        //        {
        //            zombies[i].Move(left);
        //            Debug.Log("Left");
        //        }
        //    }
        //   else if (Input.GetKey(KeyCode.D))
        //    {
        //        for (int i = 0; i < zombies.Count; i++)
        //        {
        //            zombies[i].Move(right);
        //            Debug.Log("Right");
        //        }
        //    }
    }
}
