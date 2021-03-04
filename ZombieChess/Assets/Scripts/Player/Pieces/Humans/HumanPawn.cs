using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPawn : Pawn,IAttackWithoutMoving
{
    public List<Vector2Int> AttackLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();
        int forwardDirection = GameManager.instance.currentPlayer.forward;
        
        for (int i = 1; i < 3; i++)
        {
            Vector2Int forward = new Vector2Int(gridPoint.x, gridPoint.y + forwardDirection*i);
            
            if (GameManager.instance.PieceAtGrid(forward))
            {
                locations.Add(forward);
            }
        }
        return locations;
    }
}
