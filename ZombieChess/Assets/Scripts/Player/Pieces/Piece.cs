using System.Collections.Generic;
using UnityEngine;

public enum PieceType {Pawn,Charger,Tank,Shotgunner,Unknown};

public abstract class Piece : MonoBehaviour
{
    public PieceType type;
    protected Vector2Int[] RookDirections = {new Vector2Int(0,1), new Vector2Int(1, 0), 
        new Vector2Int(0, -1), new Vector2Int(-1, 0)};
    protected Vector2Int[] BishopDirections = {new Vector2Int(1,1), new Vector2Int(1, -1), 
        new Vector2Int(-1, -1), new Vector2Int(-1, 1)};

    public abstract List<Move> MoveLocations(Vector2Int gridPoint);
    //public abstract List<Move> MoveLocations(Tile gridPoint);
    protected bool IsOnBoard(Vector2Int gridPoint)
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if (gridPoint == new Vector2Int(i, j))
                    return true;
            }
        }
        return false;
    }

}
