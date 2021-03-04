using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPatrolAI
{
    Transform WallDetection { get; }

    Transform GroundDetection { get; }
}
