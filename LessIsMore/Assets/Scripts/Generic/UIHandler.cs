using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIHandler : MonoBehaviour
{
    //DECLARE A LIST OF GAMEOBJECTS FOR STORING EACH IGNORED UI ELEMENT
    public List<GameObject> ignoredList = new List<GameObject>();
    public Texture2D cursor;
    #region SINGLETON
    public static UIHandler Instance;
    
    void Awake()
    {
        Vector3 positionOffset = Vector3.zero;
        Cursor.SetCursor(cursor, positionOffset, CursorMode.Auto);
        if (Instance != null)
            return;
        Instance = this;
    }
    #endregion
    /// <summary>
    /// CHECKS TO SEE IF THE MOUSE IS HOVERING OVER A UI ELEMENT
    /// </summary>
    /// <returns></returns>
    public bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    /// <summary>
    /// CHECKS TO SEE IF THE MOUSE IS HOVERING OVER AN IGNORED UI ELEMENT
    /// </summary>
    /// <returns></returns>
    public bool IsMouseOverIgnoredUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        
        for (int j = 0; j < ignoredList.Count; j++)
        {
            for (int i = 0; i < raycastResults.Count; i++)
            {
                if (raycastResults[i].gameObject != ignoredList[j])
                {
                    raycastResults.RemoveAt(i);
                    i--;
                }
                else
                    j++;
            }
        }
        return raycastResults.Count > 0;
    }
   void Update()
    {
        
    }
}
