using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToAim : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    GameObject player;
    LineRenderer lineRenderer;
    Vector3 camOffset = new Vector3(0, 0, 10);
    Camera camera;
    RaycastHit hit;
    Ray ray;
    public delegate void OnSpellCast(Vector3 direction);
    public OnSpellCast OnSpellCastCallback;
    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.gameObject;
        camera = Camera.main;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
    }
    void Aim()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("DrawArea"))
                return;
            if (Input.GetMouseButtonDown(0))
            {
                
                lineRenderer.enabled = true;
                lineRenderer.positionCount = 2;
                startPos = new Vector3(player.transform.position.x, 5f, player.transform.position.z);
                lineRenderer.SetPosition(0, startPos);
                lineRenderer.useWorldSpace = true;
                lineRenderer.numCapVertices = 10;

            }
            if (Input.GetMouseButton(0))
            {
                endPos = new Vector3(hit.point.x, 5f, hit.point.z);
                lineRenderer.SetPosition(1, endPos);
            }
            if (Input.GetMouseButtonUp(0))
            {
                lineRenderer.enabled = false;
                Vector3 heading = startPos - endPos;
                float distance = heading.magnitude;
                Vector3 direction = heading / distance;
                OnSpellCastCallback.Invoke(direction);
            }
        }
    }
}
