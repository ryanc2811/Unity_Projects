using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MiniMax
{
    int maxDepth = 2;

    List<Move> _moves = new List<Move>();
    List<Tile> _tilesWithPieces = new List<Tile>();
    List<Tile> _humanPieces = new List<Tile>();
    List<Tile> _zombiePieces = new List<Tile>();
    Stack<Move> moveStack = new Stack<Move>();
    //Weights _weight = new Weights();
    Tile[,] _localBoard = new Tile[8, 8];
    int _zombieScore = 0;
    int _humanScore = 0;
    Move bestMove;

    Board _board;

    public Move GetMove()
    {
        _board = Board.Instance;
        bestMove = _CreateMove(_board.GetTileFromBoard(new Vector2Int(0, 0)), _board.GetTileFromBoard(new Vector2Int(0, 0)));
        AlphaBeta(maxDepth, -10000000, 10000000, true);
        return bestMove;
    }

    int AlphaBeta(int depth, int alpha, int beta, bool max)
    {
        _GetBoardState();

        if (depth == 0)
        {
            return _Evaluate();
        }
        if (max)
        {
            int score = -10000000;
            List<Move> allMoves = _GetMoves(max);
            foreach (Move move in allMoves)
            {
                if (move != null)
                {
                    moveStack.Push(move);

                    _DoFakeMove(move.firstPos, move.secondPos);

                    score = AlphaBeta(depth - 1, alpha, beta, false);

                    _UndoFakeMove();

                    if (score > alpha)
                    {
                        move.score = score;
                        if (move.score > bestMove.score && depth == maxDepth)
                        {
                            bestMove = move;
                        }
                        alpha = score;
                    }
                    if (score >= beta)
                    {
                        break;
                    }
                }
               
            }
            return alpha;
        }
        else
        {
            int score = 10000000;
            List<Move> allMoves = _GetMoves(max);
            foreach (Move move in allMoves)
            {
                if (move != null)
                {
                    moveStack.Push(move);

                    _DoFakeMove(move.firstPos, move.secondPos);

                    score = AlphaBeta(depth - 1, alpha, beta, true);

                    _UndoFakeMove();

                    if (score < beta)
                    {
                        move.score = score;
                        beta = score;
                    }
                    if (score <= alpha)
                    {
                        break;
                    }
                }
                
            }
            return beta;
        }
    }

    void _UndoFakeMove()
    {
        Move tempMove = moveStack.Pop();
        Tile movedTo = tempMove.secondPos;
        Tile movedFrom = tempMove.firstPos;
        Piece pieceKilled = tempMove.pieceKilled;
        Piece pieceMoved = tempMove.pieceMoved;

        movedFrom.CurrentPiece = movedTo.CurrentPiece;

        if (pieceKilled != null)
        {
            movedTo.CurrentPiece = pieceKilled;
        }
        else
        {
            movedTo.CurrentPiece = null;
        }
    }

    void _DoFakeMove(Tile currentTile, Tile targetTile)
    {
        targetTile.SwapFakePieces(currentTile.CurrentPiece);
        currentTile.CurrentPiece = null;
    }

    List<Move> _GetMoves(bool isMax)
    {
        List<Move> turnMove = new List<Move>();
        List<Tile> pieces = new List<Tile>();

        if (isMax)
            pieces = _zombiePieces;
        else
            pieces = _humanPieces;

        foreach (Tile tile in pieces)
        {
            if (tile.CurrentPiece != null)
            {
                List<Move> pieceMoves = GameManager.instance.MovesForPiece(tile.CurrentPiece.gameObject);
                foreach (Move move in pieceMoves)
                {
                    Move newMove = _CreateMove(move.firstPos, move.secondPos);
                    turnMove.Add(newMove);
                }
            }
        }
        return turnMove;
    }

    int _Evaluate()
    {
        float pieceDifference = 0;
        float zombieWeight = 0;
        float humanWeight = 0;

        foreach (Tile tile in _zombiePieces)
        {
            zombieWeight += BoardEvaluator.instance.Evaluate(PlayerType.zombie);
        }
        foreach (Tile tile in _humanPieces)
        {
            humanWeight += BoardEvaluator.instance.Evaluate(PlayerType.human);
        }
        pieceDifference = (_humanScore + (humanWeight / 100)) - (_zombieScore + (zombieWeight / 100));
        return Mathf.RoundToInt(pieceDifference * 100);
    }

    void _GetBoardState()
    {
        _humanPieces.Clear();
        _zombiePieces.Clear();
        _humanScore = 0;
        _zombieScore = 0;
        _tilesWithPieces.Clear();

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                _localBoard[x, y] = _board.GetTileFromBoard(new Vector2Int(x, y));
                if (_localBoard[x, y].CurrentPiece != null && _localBoard[x, y].CurrentPiece.type != PieceType.Unknown)
                {
                    
                    _tilesWithPieces.Add(_localBoard[x, y]);
                }
            }
        }
        foreach (Tile tile in _tilesWithPieces)
        {
            if (GameManager.instance.GetPlayerFromGrid(tile.Position) == PlayerType.human)
            {
                _humanScore += BoardEvaluator.instance.GetScore(tile.CurrentPiece.type);
                _humanPieces.Add(tile);
            }
            else if(GameManager.instance.GetPlayerFromGrid(tile.Position) == PlayerType.zombie)
            {
                
                _zombieScore += BoardEvaluator.instance.GetScore(tile.CurrentPiece.type);
                _zombiePieces.Add(tile);
            }
        }
    }

    Move _CreateMove(Tile firstPosition, Tile secondPosition)
    {
        Move tempMove = new Move();
        tempMove.firstPos = firstPosition;
        tempMove.pieceMoved = firstPosition.CurrentPiece;
        tempMove.secondPos = secondPosition;

        if (secondPosition!=null)
        {
            tempMove.pieceKilled = secondPosition.CurrentPiece;
        }
        return tempMove;
    }
}
