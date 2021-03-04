using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour
{
    public GameObject moveLocationPrefab;
    public GameObject tileHighlightPrefab;
    public GameObject attackLocationPrefab;

    private GameObject tileHighlight;
    private GameObject movingPiece;

    private List<Move> moveLocations;
    private List<Vector2Int> attackLocations;
    private List<GameObject> locationHighlights;
    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
        tileHighlight = Instantiate(tileHighlightPrefab, Geometry.PointFromGrid(new Vector2Int(0, 0)), Quaternion.identity, gameObject.transform);
        tileHighlight.SetActive(false);
    }
    public void EnterState(GameObject piece)
    {
        movingPiece = piece;
        this.enabled = true;
        moveLocations = GameManager.instance.MovesForPiece(movingPiece);
        attackLocations = GameManager.instance.AttacksForPiece(movingPiece);
        locationHighlights = new List<GameObject>();

        foreach(Move move in moveLocations)
        {
            GameObject highlight;
            if (GameManager.instance.PieceAtGrid(move.secondPos.Position))
            {
                highlight = Instantiate(attackLocationPrefab, Geometry.PointFromGrid(move.secondPos.Position), Quaternion.identity, gameObject.transform);
            }
            else
                highlight = Instantiate(moveLocationPrefab, Geometry.PointFromGrid(move.secondPos.Position), Quaternion.identity, gameObject.transform);
            locationHighlights.Add(highlight);
        }
        if (attackLocations != null)
        {
            foreach (Vector2Int loc in attackLocations)
            {
                GameObject highlight;
                if (GameManager.instance.PieceAtGrid(loc))
                {
                    highlight = Instantiate(attackLocationPrefab, Geometry.PointFromGrid(loc), Quaternion.identity, gameObject.transform);
                    locationHighlights.Add(highlight);
                }
            }
        }
    }
    void ExitState()
    {
        this.enabled = false;
        tileHighlight.SetActive(false);
        GameManager.instance.DeselectPiece(movingPiece);
        movingPiece = null;
        TileSelector selector = GetComponent<TileSelector>();
        foreach (GameObject highlight in locationHighlights)
            Destroy(highlight);
        GameManager.instance.NextPlayer();
        selector.EnterState();
    }
    void SwapPieces()
    {
        this.enabled = false;
        tileHighlight.SetActive(false);
        GameManager.instance.DeselectPiece(movingPiece);
        movingPiece = null;
        TileSelector selector = GetComponent<TileSelector>();
        foreach (GameObject highlight in locationHighlights)
            Destroy(highlight);
        selector.EnterState();
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 point = hit.point;
            Vector2Int gridPoint = Geometry.GridFromPoint(point);
            tileHighlight.SetActive(true);
            tileHighlight.transform.position = Geometry.PointFromGrid(gridPoint);
            if (Input.GetMouseButtonDown(0))
            {
                Piece piece = movingPiece.GetComponent<Piece>();
                bool containsGridPoint = false;
                foreach(Move move in moveLocations)
                {
                    if (move.secondPos.Position == gridPoint)
                    {
                        containsGridPoint = true;
                        break;
                    }
                }
                if(piece is IAttackWithoutMoving)
                {
                    if (GameManager.instance.PieceAtGrid(gridPoint) == null&& containsGridPoint)
                    {
                        GameManager.instance.Move(movingPiece, Board.Instance.GetTileFromBoard(gridPoint));
                        ExitState();
                    }
                    else if (GameManager.instance.PieceAtGrid(gridPoint) !=null&& attackLocations.Contains(gridPoint)&& !containsGridPoint)
                    {
                        if (piece is Shotgunner)
                        {
                            foreach(Vector2Int loc in attackLocations)
                                GameManager.instance.CapturePieceAt(loc);
                        }
                        else
                            GameManager.instance.CapturePieceAt(gridPoint);
                        ExitState();
                    }
                    else if(!attackLocations.Contains(gridPoint)&& GameManager.instance.PieceAtGrid(gridPoint) != null&& containsGridPoint)
                    {
                        GameManager.instance.CapturePieceAt(gridPoint);
                        GameManager.instance.Move(movingPiece, Board.Instance.GetTileFromBoard(gridPoint));
                        ExitState();
                    }
                }
                else
                {
                    if (!containsGridPoint)
                        return;
                    if (GameManager.instance.PieceAtGrid(gridPoint) == null)
                    {
                        GameManager.instance.Move(movingPiece, Board.Instance.GetTileFromBoard(gridPoint));
                    }
                    else
                    {
                        GameManager.instance.CapturePieceAt(gridPoint);
                        GameManager.instance.Move(movingPiece, Board.Instance.GetTileFromBoard(gridPoint));
                    }
                    ExitState();
                }
            }
        }
        else
            tileHighlight.SetActive(false);
        if (Input.GetMouseButtonDown(1))
        {
            SwapPieces();
        }
    }


}
