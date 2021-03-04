using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    LineRenderer lineRenderer;
    Camera cam;

    FieldOfView fov;

    Vector3 mousePos;
    float aimHeight;
    bool waiting = false;
    bool alreadyShotGun = false;
    public delegate void OnFireTrigger(Vector3 direction);
    public OnFireTrigger OnFireGunCallback;
    // Start is called before the first frame update
    void Start()
    {
        aimHeight = transform.position.y+1f;
        cam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        fov = GetComponent<FieldOfView>();
        EventManager.instance.OnBulletDestroyedTrigger += BulletDestroyed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.instance.isPaused)
            return;
        if (!alreadyShotGun)
        {
            Aim();
            fov.PlacePointInsideFov(Vector3.zero);
        }

    }
    void BulletDestroyed()
    {
        alreadyShotGun = false;
        StartCoroutine(WaitToAim());
    }
    void Aim()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                lineRenderer.enabled = true;
                lineRenderer.positionCount = 2;
                startPos = new Vector3(transform.position.x, aimHeight, transform.position.z);
                lineRenderer.SetPosition(0, startPos);
                lineRenderer.useWorldSpace = true;
                lineRenderer.numCapVertices = 10;
            }
            if (Input.GetMouseButton(0))
            {
                RaycastHit lineHit;
                Vector3 tempPos = new Vector3(hit.point.x, aimHeight, hit.point.z);

                if (fov.CheckPointInsideFov(tempPos))
                {
                    endPos = fov.PlacePointInsideFov(tempPos);
                    if (Physics.Linecast(startPos, endPos, out lineHit))
                    {
                        lineRenderer.SetPosition(1, lineHit.point);
                    }
                    else
                    {
                        lineRenderer.SetPosition(1, endPos);
                    }
                }

            }
            if (Input.GetMouseButtonUp(0) && !waiting)
            {
                lineRenderer.enabled = false;
                Vector3 heading = startPos - endPos;
                float distance = heading.magnitude;
                Vector3 direction = heading / distance;
                OnFireGunCallback.Invoke(direction);
                alreadyShotGun = true;
            }
        }
    }
    IEnumerator WaitToAim()
    {
        waiting = true;
        yield return new WaitForSeconds(1f);
        waiting = false;
        Debug.Log("waited");
    }

    void OnDestroy()
    {
        EventManager.instance.OnBulletDestroyedTrigger -= BulletDestroyed;
    }
}
