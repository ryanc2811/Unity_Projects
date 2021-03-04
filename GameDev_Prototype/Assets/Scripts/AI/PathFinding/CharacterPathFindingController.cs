using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPathFindingController : MonoBehaviour
{
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    [SerializeField]
    private float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    public void StopMoving()
    {
        pathVectorList = null;
    }
    private void HandleMovement()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
    }
   
    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = PathFinding.Instance.FindPath(transform.position, targetPosition);
        if (pathVectorList!=null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }
}
