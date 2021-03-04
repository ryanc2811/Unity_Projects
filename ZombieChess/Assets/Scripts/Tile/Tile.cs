using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile 
{
    private Vector2Int _position = Vector2Int.zero;
    public Vector2Int Position
    {
        get { return _position; }
        set { Position = value; }
    }
    public Tile(int x, int y)
    {
        _position.x = x;
        _position.y = y;
    }
    private Piece _currentPiece = null;
    public Piece CurrentPiece
    {
        get { return _currentPiece; }
        set { _currentPiece = value; }
    }
    public void SwapFakePieces(Piece newPiece)
    {
        _currentPiece = newPiece;
    }
}
