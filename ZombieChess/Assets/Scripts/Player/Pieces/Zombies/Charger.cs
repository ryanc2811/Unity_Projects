using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : Piece
{
    public override List<Move> MoveLocations(Vector2Int gridPoint)
    {
        List<Move> moves = new List<Move>();

        foreach(Vector2Int direction in RookDirections)
        {
            for(int i = 1; i < 8; i++)
            {
                Vector2Int nextGridPoint = new Vector2Int(gridPoint.x+i*direction.x,gridPoint.y+i*direction.y);
                if (IsOnBoard(nextGridPoint))
                {
                    Move move = new Move();
                    move.firstPos = Board.Instance.GetTileFromBoard(gridPoint);
                    move.secondPos = Board.Instance.GetTileFromBoard(nextGridPoint);
                    moves.Add(move);
                }
                if (GameManager.instance.PieceAtGrid(nextGridPoint))
                    break;
            }
        }
        return moves;
    }
    //public override List<Move> MoveLocations(Tile gridPoint)
    //{
    //    List<Move> moves = new List<Move>();

    //    foreach (Vector2Int direction in RookDirections)
    //    {
    //        for (int i = 1; i < 8; i++)
    //        {
    //            Vector2Int nextGridPoint = new Vector2Int(gridPoint.Position.x + i * direction.x, gridPoint.Position.y + i * direction.y);
    //            if (IsOnBoard(nextGridPoint))
    //            {
    //                Move move = new Move();
    //                move.firstPos = gridPoint;
    //                move.secondPos = Board.Instance.GetTileFromBoard(nextGridPoint);
    //                moves.Add(move);
    //            }
    //            if (GameManager.instance.PieceAtGrid(nextGridPoint))
    //                break;
    //        }
    //    }
    //    return moves;
    //}
}
