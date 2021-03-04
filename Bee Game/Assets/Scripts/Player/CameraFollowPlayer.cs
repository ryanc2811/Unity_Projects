using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        //FOLLOW THE PLAYER
        transform.position = player.position + offset;
        transform.eulerAngles = player.transform.eulerAngles;
    }

    //void ApplyPositionDamping(Vector3 targetCenter)
    //{
    //    Vector3 position = transform.position;
    //    Vector3 offset = position - targetCenter;
    //    offset.y = 0;
    //    Vector3 newTargetPos = offset.normalized * distance + targetCenter;

    //    Vector3 newPosition;
    //    newPosition.x = Mathf.SmoothDamp(position.x, newTargetPos.x, ref velocity.x, smoothLag, maxSpeed);
    //    newPosition.y = Mathf.SmoothDamp(position.y, newTargetPos.y, ref velocity.y, smoothLag, maxSpeed);
    //    newPosition.z = Mathf.SmoothDamp(position.z, newTargetPos.z, ref velocity.z, smoothLag, maxSpeed);

    //    newPosition = AdjustLineOfSight(newPosition, targetCenter);
    //}
}