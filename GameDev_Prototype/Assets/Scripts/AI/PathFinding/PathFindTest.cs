using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindTest : MonoBehaviour
{
    PathFinding pathFinding;

    [SerializeField]
    int width;
    [SerializeField]
    int height;
    [SerializeField]
    float cellSize;
    // Start is called before the first frame update
    void Start()
    {
        pathFinding = new PathFinding(width, height,cellSize);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = GetWorldPosition();
            pathFinding.GetGrid().GetXY(mouseWorldPosition,out int x,out int y);
            List<PathNode> path = pathFinding.FindPath(0, 0, x, y);
            if (path != null)
            {
                for (int i = 0; i < path.Count-1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * cellSize + Vector3.one * (cellSize / 2), new Vector3(path[i + 1].x, path[i + 1].y) * cellSize + Vector3.one * (cellSize/2), Color.green);
                }
            }
        }
    }

    private static Vector3 GetWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    private static Vector3 GetMouseWorldPositionWithZ(Vector3 mousePosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(mousePosition);
        return worldPosition;
    }
}
