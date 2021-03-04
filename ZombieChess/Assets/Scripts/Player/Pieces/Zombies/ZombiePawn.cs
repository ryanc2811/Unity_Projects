using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePawn : Pawn
{
    public override List<Move> MoveLocations(Vector2Int gridPoint)
    {
        List<Move> moves = new List<Move>();
        Tile startPoint = Board.Instance.GetTileFromBoard(gridPoint);
        int forwardDirection = GameManager.instance.currentPlayer.forward;

        Vector2Int forward = new Vector2Int(gridPoint.x, gridPoint.y + forwardDirection);
        if (IsOnBoard(forward))
        {
            Move move = new Move();
            move.firstPos = startPoint;
            move.secondPos = Board.Instance.GetTileFromBoard(forward);
            moves.Add(move);
        }
        Vector2Int forwardRight = new Vector2Int(gridPoint.x + 1, gridPoint.y + forwardDirection);
        if (IsOnBoard(forwardRight))
        {
            Move move = new Move();
            move.firstPos = startPoint;
            move.secondPos = Board.Instance.GetTileFromBoard(forwardRight);
            moves.Add(move);
        }
        Vector2Int forwardLeft = new Vector2Int(gridPoint.x - 1, gridPoint.y + forwardDirection);
        if (IsOnBoard(forwardLeft))
        {
            Move move = new Move();
            move.firstPos = startPoint;
            move.secondPos = Board.Instance.GetTileFromBoard(forwardLeft);
            moves.Add(move);
        }
        Vector2Int Left = new Vector2Int(gridPoint.x - 1, gridPoint.y);
        if (IsOnBoard(Left))
        {
            Move move = new Move();
            move.firstPos = startPoint;
            move.secondPos = Board.Instance.GetTileFromBoard(Left);
            moves.Add(move);
        }
        Vector2Int Right = new Vector2Int(gridPoint.x +1, gridPoint.y);
        if (IsOnBoard(Right))
        {
            Move move = new Move();
            move.firstPos = startPoint;
            move.secondPos = Board.Instance.GetTileFromBoard(Right);
            moves.Add(move);
        }
        return moves;
    }
    //public override List<Move> MoveLocations(Tile gridPoint)
    //{
    //    List<Move> moves = new List<Move>();
    //    Vector2Int startPoint = gridPoint.Position;
    //    int forwardDirection = GameManager.instance.currentPlayer.forward;

    //    Vector2Int forward = new Vector2Int(startPoint.x, startPoint.y + forwardDirection);
    //    if (IsOnBoard(forward))
    //    {
    //        Move move = new Move();
    //        move.firstPos = gridPoint;
    //        move.secondPos = Board.Instance.GetTileFromBoard(forward);
    //        moves.Add(move);
    //    }
    //    Vector2Int forwardRight = new Vector2Int(startPoint.x + 1, startPoint.y + forwardDirection);
    //    if (IsOnBoard(forwardRight))
    //    {
    //        Move move = new Move();
    //        move.firstPos = gridPoint;
    //        move.secondPos = Board.Instance.GetTileFromBoard(forwardRight);
    //        moves.Add(move);
    //    }
    //    Vector2Int forwardLeft = new Vector2Int(startPoint.x - 1, startPoint.y + forwardDirection);
    //    if (IsOnBoard(forwardLeft))
    //    {
    //        Move move = new Move();
    //        move.firstPos = gridPoint;
    //        move.secondPos = Board.Instance.GetTileFromBoard(forwardLeft);
    //        moves.Add(move);
    //    }
    //    Vector2Int Left = new Vector2Int(startPoint.x - 1, startPoint.y);
    //    if (IsOnBoard(Left))
    //    {
    //        Move move = new Move();
    //        move.firstPos = gridPoint;
    //        move.secondPos = Board.Instance.GetTileFromBoard(Left);
    //        moves.Add(move);
    //    }
    //    Vector2Int Right = new Vector2Int(startPoint.x + 1, startPoint.y);
    //    if (IsOnBoard(Right))
    //    {
    //        Move move = new Move();
    //        move.firstPos = gridPoint;
    //        move.secondPos = Board.Instance.GetTileFromBoard(Right);
    //        moves.Add(move);
    //    }
    //    return moves;
    //}
}
