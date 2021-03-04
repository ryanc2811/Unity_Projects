using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public Tile firstPos;
    public Tile secondPos;
    public Piece pieceMoved;
    public Piece pieceKilled;
    public int score = -10000000;
}
