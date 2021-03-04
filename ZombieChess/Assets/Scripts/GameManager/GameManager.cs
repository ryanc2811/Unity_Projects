using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Board board;

    public GameObject zombiePawn;
    public GameObject charger;
    public GameObject humanPawn;
    public GameObject tank;
    public GameObject shotgunner;

    //private GameObject[,] pieces;

    private Player zombie;
    private Player human;
    public Player currentPlayer;
    public Player otherPlayer;
    private List<Vector2Int> movesMade;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //pieces = new GameObject[8, 8];
        movesMade = new List<Vector2Int>();
        zombie = new Player("zombie", true);
        human = new Player("human", false);

        currentPlayer = human;
        otherPlayer = zombie;
        board.SetupBoard();
        InitialSetup();
        
    }

    private void InitialSetup()
    {
        AddPiece(charger, zombie, 0, 0);
        AddPiece(charger, zombie, 7, 0);
        AddPiece(charger, human, 0, 7);
        AddPiece(charger, human, 7, 7);

        AddPiece(shotgunner, human, 6, 7);
        AddPiece(shotgunner, human, 1, 7);
        AddPiece(shotgunner, zombie, 6, 0);
        AddPiece(shotgunner, zombie, 1, 0);

        AddPiece(tank, zombie, 5, 0);
        AddPiece(tank, zombie, 2, 0);
        AddPiece(tank, human, 5, 7);
        AddPiece(tank, human, 2, 7);

        AddPiece(zombiePawn, zombie, 3, 0);
        AddPiece(zombiePawn, zombie, 4, 0);
        AddPiece(humanPawn, human, 4, 7);
        AddPiece(humanPawn, human, 3, 7);
        for (int i = 0; i < 8; i++)
        {
            AddPiece(zombiePawn, zombie, i, 1);
        }

        for (int i = 0; i < 8; i++)
        {
            AddPiece(humanPawn, human, i, 6);
        }
    }
    public void CapturePieceAt(Vector2Int gridPoint)
    {
        GameObject pieceToCapture = PieceAtGrid(gridPoint);
        currentPlayer.capturedPieces.Add(pieceToCapture);
        Destroy(pieceToCapture);
    }
    public void AddPiece(GameObject prefab, Player player, int col, int row)
    {
        GameObject pieceObject = board.AddPiece(prefab, col, row);
        player.pieces.Add(pieceObject);
    }

    public void SelectPieceAtGrid(Vector2Int gridPoint)
    {
        GameObject selectedPiece = board.GetTileFromBoard(gridPoint).CurrentPiece.gameObject;
        if (selectedPiece)
        {
            board.SelectPiece(selectedPiece);
        }
    }
    public List<Tile> TilesWithPiece()
    {
        List<Tile> tiles= new List<Tile>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if(PieceAtGrid(new Vector2Int(i, j)))
                {
                    tiles.Add(board.GetTileFromBoard(new Vector2Int(i, j)));
                }
            }
        }
        return tiles;
    }
    public void SelectPiece(GameObject piece)
    {
        board.SelectPiece(piece);
    }

    public void DeselectPiece(GameObject piece)
    {
        board.DeselectPiece(piece);
    }

    public GameObject PieceAtGrid(Vector2Int gridPoint)
    {
        if (gridPoint.x > 7 || gridPoint.y > 7 || gridPoint.x < 0 || gridPoint.y < 0)
        {
            return null;
        }

        Piece piece = board.GetTileFromBoard(gridPoint).CurrentPiece;
        if(piece!=null)
            return piece.gameObject;

        return null;
    }

    public Tile TileForPiece(GameObject piece)
    {
        if(piece)
        {
            return board.GetTileFromPiece(piece);
        }
        else
            return null;
    }

    public bool FriendlyPieceAt(Vector2Int gridPoint)
    {
        GameObject piece = PieceAtGrid(gridPoint);

        if (piece == null)
        {
            return false;
        }

        if (otherPlayer.pieces.Contains(piece))
        {
            return false;
        }
        return true;
    }

    public bool DoesPieceBelongToCurrentPlayer(GameObject piece)
    {
        return currentPlayer.pieces.Contains(piece);
    }
    public PlayerType GetPlayerFromGrid(Vector2Int tile)
    {
        GameObject piece = PieceAtGrid(tile);
        if (piece)
        {
            if (currentPlayer.name == "zombie")
                if (currentPlayer.pieces.Contains(piece))
                    return PlayerType.zombie;

            if (currentPlayer.name == "human")
                if (currentPlayer.pieces.Contains(piece))
                    return PlayerType.human;
        }

        return PlayerType.unknown;
    }
    public void Move(GameObject piece, Tile gridPoint)
    {
        Tile startGridPoint = TileForPiece(piece);
        startGridPoint.CurrentPiece = null;
        board.MovePiece(piece, gridPoint.Position);
    }

    public void SwapPieces(Move move)
    {
        Tile firstTile = move.firstPos;
        Tile secondTile = move.secondPos;

        if (PieceAtGrid(secondTile.Position)&&!FriendlyPieceAt(secondTile.Position))
        {
            GameObject pieceToCapture = PieceAtGrid(secondTile.Position);
            currentPlayer.capturedPieces.Add(pieceToCapture);
            move.pieceKilled = pieceToCapture.GetComponent<Piece>();
            Destroy(PieceAtGrid(secondTile.Position));
        }
        move.pieceMoved = firstTile.CurrentPiece;
        Move(move.pieceMoved.gameObject, secondTile);
        NextPlayer();
    }

    public List<Move> MovesForPiece(GameObject pieceObject)
    {
        Piece piece = pieceObject.GetComponent<Piece>();
        Tile gridPoint = TileForPiece(pieceObject);
        List<Move> moves = piece.MoveLocations(gridPoint.Position);
        //foreach (Move move in moves)
        //{
        //    move.pieceMoved = piece;
        //}
        moves.RemoveAll(tile => FriendlyPieceAt(tile.secondPos.Position));
        return moves;
    }

    public List<Vector2Int> AttacksForPiece(GameObject pieceObject)
    {
        Piece piece = pieceObject.GetComponent<Piece>();
        if (piece is IAttackWithoutMoving)
        {
            Vector2Int gridPoint = TileForPiece(pieceObject).Position;

            List<Vector2Int> locations = ((IAttackWithoutMoving)piece).AttackLocations(gridPoint);

            locations.RemoveAll(tile => tile.x < 0 || tile.x > 7 || tile.y < 0 || tile.y > 7);
            locations.RemoveAll(tile => FriendlyPieceAt(tile));
            return locations;
        }
        return null;
    }
    public void NextPlayer()
    {
        Player tempPlayer = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = tempPlayer;
    }
}
