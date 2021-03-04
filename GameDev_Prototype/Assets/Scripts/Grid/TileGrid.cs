using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGrid : MonoBehaviour
{

    Tilemap tilemap;
    public List<Vector3> tileWorldLocations;

    public static TileGrid Instance;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        tilemap = GetComponent<Tilemap>();

        tileWorldLocations = new List<Vector3>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = tilemap.CellToWorld(localPlace);
            if (tilemap.HasTile(localPlace))
            {
                tileWorldLocations.Add(place);
            }
        }
    }


    public bool IsPositionTraversable(Vector3 position)
    {
        for (int i = 0; i < tileWorldLocations.Count; i++)
        {
            float distance = Vector3.Distance(tileWorldLocations[i], position);
            if (distance < .5f)
            {
                return true;
            }
        }
        return false;
    }
}
