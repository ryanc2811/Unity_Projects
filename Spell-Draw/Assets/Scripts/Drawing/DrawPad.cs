using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using PDollarGestureRecognizer;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DrawPad : MonoBehaviour
{
    public static DrawPad instance;
    public Transform gestureOnScreenPrefab;
    public GestureRecogniser gestureRecogniser;
    private List<Point> points = new List<Point>();
    private int strokeId = -1;
    private Vector3 virtualKeyPosition = Vector2.zero;

    private RuntimePlatform platform;
    private int positionCount = 0;
    private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
    private LineRenderer currentGestureLineRenderer;

    //GUI
    private string message;
    private bool recognised;
    public Text gestureNameTxt;
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    public bool hitDrawArea { get; private set; }

    public List<Point> Points { get { return points;} }
    void Awake()
    {
        instance = this;
    }
    void Start()
    {

        platform = Application.platform;
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    private void CheckTouch(Vector2 position)
    {
        // Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("DrawArea"))
            {
                virtualKeyPosition = new Vector2(position.x,position.y);
                hitDrawArea = true;
            }
            else
            {
                hitDrawArea = false;
            }
        }
    }
    void Update()
    {

        //if (points.Count > 1)
        //{
        //    Result result = gestureRecogniser.CheckIfSymbolExist(points);
        //    if (result.GestureClass != "null")
        //    {
        //        gestureNameTxt.text = result.GestureClass;
        //        recognised = true;
        //    }
        //}
           
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                CheckTouch(new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                CheckTouch(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            }
        }
        if (hitDrawArea)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (recognised)
                {
                    recognised = false;
                    strokeId = -1;

                    points.Clear();

                    foreach (LineRenderer lineRenderer in gestureLinesRenderer)
                    {
                        lineRenderer.positionCount=0;
                        Destroy(lineRenderer.gameObject);
                    }

                    gestureLinesRenderer.Clear();
                }

                ++strokeId;

                Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
                currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();

                gestureLinesRenderer.Add(currentGestureLineRenderer);

                positionCount = 0;
            }

            if (Input.GetMouseButton(0))
            {
                points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));

                currentGestureLineRenderer.positionCount=++positionCount;
                currentGestureLineRenderer.SetPosition(positionCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
            }
            
        }
    }
}
