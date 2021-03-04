using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgunner : Piece, IAttackWithoutMoving
{
    public List<Vector2Int> AttackLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();
        int forwardDirection = GameManager.instance.currentPlayer.forward;

        for (int i = 1; i < 2; i++)
        {
            Vector2Int forward = new Vector2Int(gridPoint.x, gridPoint.y + forwardDirection * i);
            Vector2Int forwardRight = new Vector2Int(gridPoint.x + 1, gridPoint.y + forwardDirection);
            Vector2Int forwardLeft = new Vector2Int(gridPoint.x - 1, gridPoint.y + forwardDirection);

            if (GameManager.instance.PieceAtGrid(forward))
            {
                locations.Add(forward);
            }
            if (GameManager.instance.PieceAtGrid(forwardLeft))
            {
                locations.Add(forwardLeft);
            }
            if (GameManager.instance.PieceAtGrid(forwardRight))
            {
                locations.Add(forwardRight);
            }
        }
        return locations;
    }


    public override List<Move> MoveLocations(Vector2Int gridPoint)
    {
        List<Move> moves = new List<Move>();

        int forwardDirection = GameManager.instance.currentPlayer.forward;
        Vector2Int forward = new Vector2Int(gridPoint.x, gridPoint.y + forwardDirection);
        if (IsOnBoard(forward)&& GameManager.instance.PieceAtGrid(forward) == false)
        {
            Move move = new Move();
            move.firstPos = Board.Instance.GetTileFromBoard(gridPoint);
            move.secondPos = Board.Instance.GetTileFromBoard(forward);
            moves.Add(move);
        }
        return moves;
    }
    //public override List<Move> MoveLocations(Tile gridPoint)
    //{
    //    List<Move> moves = new List<Move>();

    //    int forwardDirection = GameManager.instance.currentPlayer.forward;
    //    Vector2Int forward = new Vector2Int(gridPoint.Position.x, gridPoint.Position.y + forwardDirection);
    //    if (IsOnBoard(forward) && GameManager.instance.PieceAtGrid(forward) == false)
    //    {
    //        Move move = new Move();
    //        move.firstPos = gridPoint;
    //        move.secondPos = Board.Instance.GetTileFromBoard(forward);
    //        moves.Add(move);
    //    }
    //    return moves;
    //}
}
