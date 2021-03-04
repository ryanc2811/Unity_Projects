using PDollarGestureRecognizer;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GestureRecogniser : MonoBehaviour
{
    private List<Gesture> trainingSet = new List<Gesture>();
    // Start is called before the first frame update
    void Start()
    {
        //Load pre-made gestures
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("Gestures");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        ////Load user custom gestures
        //string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        //foreach (string filePath in filePaths)
        //    trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
    }

    public Result CheckIfSymbolExist(List<Point>points)
    {
        Gesture candidate = new Gesture(points.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

        return gestureResult;
    }
}
