using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof (GuideDog))]
public class GuideDogEditor : Editor
{
    void OnSceneGUI()
    {
        GuideDog guideDogView = (GuideDog)target;
        Handles.color = Color.white;
        //Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Handles.DrawWireDisc((Vector2)guideDogView.transform.position, guideDogView.transform.forward, guideDogView.viewRadius);
        Vector2 viewAngleA = guideDogView.DirFromAngles(-guideDogView.viewAngle / 2, false);
        Vector2 viewAngleB = guideDogView.DirFromAngles(guideDogView.viewAngle / 2, false);

        Handles.DrawLine((Vector2)guideDogView.transform.position, (Vector2)guideDogView.transform.position + viewAngleA * guideDogView.viewRadius);
        Handles.DrawLine((Vector2)guideDogView.transform.position, (Vector2)guideDogView.transform.position + viewAngleB * guideDogView.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in (guideDogView).visibleTargets)
        {
            Handles.DrawLine(guideDogView.transform.position, visibleTarget.transform.position);
        }
    }
}
