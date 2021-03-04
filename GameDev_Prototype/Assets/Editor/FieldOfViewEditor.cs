using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof (TargetFinder))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI()
    {
        FieldOfView fow = (TargetFinder)target;
        Handles.color = Color.white;
        //Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Handles.DrawWireDisc((Vector2)fow.transform.position, fow.transform.forward, fow.viewRadius);
        Vector2 viewAngleA = fow.DirFromAngles(-fow.viewAngle / 2, false);
        Vector2 viewAngleB = fow.DirFromAngles(fow.viewAngle / 2, false);

        Handles.DrawLine((Vector2)fow.transform.position, (Vector2)fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine((Vector2)fow.transform.position, (Vector2)fow.transform.position + viewAngleB * fow.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in ((TargetFinder)fow).visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.transform.position);
        }
    }
}
