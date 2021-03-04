using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override List<Move> MoveLocations(Vector2Int gridPoint)
    {
        List<Move> moves = new List<Move>();
        
        int forwardDirection = GameManager.instance.currentPlayer.forward;
        Vector2Int forward = new Vector2Int(gridPoint.x, gridPoint.y + forwardDirection);
        if (GameManager.instance.PieceAtGrid(forward) == false&&IsOnBoard(forward))
        {
            Move move = new Move();
            move.firstPos = Board.Instance.GetTileFromBoard(gridPoint); 
            move.secondPos = Board.Instance.GetTileFromBoard(forward);
            moves.Add(move);
        }

        Vector2Int forwardRight = new Vector2Int(gridPoint.x + 1, gridPoint.y + forwardDirection);
        if (GameManager.instance.PieceAtGrid(forwardRight) && IsOnBoard(forwardRight))
        {
            Move move = new Move();
            move.firstPos = Board.Instance.GetTileFromBoard(gridPoint);
            move.secondPos = Board.Instance.GetTileFromBoard(forwardRight);
            moves.Add(move);
        }

        Vector2Int forwardLeft = new Vector2Int(gridPoint.x - 1, gridPoint.y + forwardDirection);
        if (GameManager.instance.PieceAtGrid(forwardLeft) && IsOnBoard(forwardLeft))
        {
            Move move = new Move();
            move.firstPos = Board.Instance.GetTileFromBoard(gridPoint);
            move.secondPos = Board.Instance.GetTileFromBoard(forwardLeft);
            moves.Add(move);
        }
        return moves;
    }
    //public override List<Move> MoveLocations(Tile gridPoint)
    //{
    //    List<Move> moves = new List<Move>();

    //    int forwardDirection = GameManager.instance.currentPlayer.forward;
    //    Vector2Int forward = new Vector2Int(gridPoint.Position.x, gridPoint.Position.y + forwardDirection);
    //    if (GameManager.instance.PieceAtGrid(forward) == false && IsOnBoard(forward))
    //    {
    //        Move move = new Move();
    //        move.firstPos = gridPoint;
    //        move.secondPos = Board.Instance.GetTileFromBoard(forward);
    //        moves.Add(move);
    //    }

    //    Vector2Int forwardRight = new Vector2Int(gridPoint.Position.x + 1, gridPoint.Position.y + forwardDirection);
    //    if (GameManager.instance.PieceAtGrid(forwardRight) == false && IsOnBoard(forwardRight))
    //    {
    //        Move move = new Move();
    //        move.firstPos = gridPoint;
    //        move.secondPos = Board.Instance.GetTileFromBoard(forwardRight);
    //        moves.Add(move);
    //    }

    //    Vector2Int forwardLeft = new Vector2Int(gridPoint.Position.x - 1, gridPoint.Position.y + forwardDirection);
    //    if (GameManager.instance.PieceAtGrid(forwardLeft) && IsOnBoard(forwardLeft))
    //    {
    //        Move move = new Move();
    //        move.firstPos = gridPoint;
    //        move.secondPos = Board.Instance.GetTileFromBoard(forwardLeft);
    //        moves.Add(move);
    //    }
    //    return moves;
    //}
}
