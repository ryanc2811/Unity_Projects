using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewindable : MonoBehaviour,IRewindable
{
    public List<Vector3> positions;
    List<Vector3> rotations;
    bool rewinding = false;
    int posCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector3>();
        rotations = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (positions.Count <=1)
        {
            rewinding = false;
        }
    }
    void FixedUpdate()
    {
        if (!rewinding)
        {
            positions.Add(transform.position);
            rotations.Add(transform.localEulerAngles);
        } 
        else
        {
            transform.position = positions[positions.Count - 1];
            positions.RemoveAt(positions.Count - 1);
            transform.localEulerAngles = rotations[rotations.Count - 1];
            rotations.RemoveAt(positions.Count - 1);
        }
    }
    public void Rewind()
    {
        if (!rewinding)
        {
            rewinding = true;
            posCount = positions.Count;
        }
            
        
    }
}
