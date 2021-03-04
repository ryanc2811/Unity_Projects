using UnityEngine;

public class Board : MonoBehaviour
{
    public Material zombieMaterial;
    public Material humanMaterial;
    public Material selectedMaterial;
    public static Board Instance;
    void Awake()
    {
        Instance = this;
    }
    public GameObject AddPiece(GameObject piece, int col, int row)
    {
        Vector2Int gridPoint = Geometry.GridPoint(col, row);
        GameObject newPiece = Instantiate(piece, Geometry.PointFromGrid(gridPoint),
            Quaternion.identity, gameObject.transform);
        _board[gridPoint.x, gridPoint.y].CurrentPiece = newPiece.GetComponent<Piece>();
        //Debug.Log(_board[gridPoint.x, gridPoint.y].CurrentPiece);
        return newPiece;
    }

    public void RemovePiece(GameObject piece)
    {
        Destroy(piece);
    }

    public void MovePiece(GameObject piece, Vector2Int gridPoint)
    {
        piece.transform.position = Geometry.PointFromGrid(gridPoint);
        piece.transform.position = new Vector3(piece.transform.position.x, piece.transform.position.y+.4f, piece.transform.position.z);
        _board[gridPoint.x, gridPoint.y].CurrentPiece=piece.GetComponent<Piece>();
    }

    public void SelectPiece(GameObject piece)
    {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        renderers.material = selectedMaterial;
    }

    public void DeselectPiece(GameObject piece)
    {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        if(GameManager.instance.currentPlayer.name=="zombie")
            renderers.material = zombieMaterial;
        else
            renderers.material = humanMaterial;
    }
    private Tile[,] _board = new Tile[8, 8];

    public void SetupBoard()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                _board[x, y] = new Tile(x, y);
            }
        }
    }
    public Tile GetTileFromPiece(GameObject piece)
    {
        foreach(Tile tile in _board)
        {
            //Debug.Log(tile.CurrentPiece+ " "+tile.Position);
            if (tile.CurrentPiece == piece.GetComponent<Piece>())
            {
                return tile;
            }
        }
        return null;
    }
    public Tile GetTileFromBoard(Vector2Int tile)
    {
        return _board[tile.x, tile.y];
    }
}
