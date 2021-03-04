using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackWithoutMoving
{
    List<Vector2Int> AttackLocations(Vector2Int gridPoint);
}
