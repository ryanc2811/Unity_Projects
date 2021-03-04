using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundLineSpawner : MonoBehaviour
{
    [SerializeField] private Text lineCountTxt;
    [SerializeField] GameObject soundLinePrefab;
    [SerializeField] private float destroyObjectAfter;
    [SerializeField] private int numObjects = 10;
    [SerializeField] private float radius=1f;

    void Awake()
    {
        UpdateLineCount();
    }
    private Vector2 RandomCircle(int objectNumber, float radius,Vector2 center)
    {
        float angle = objectNumber * 2 * Mathf.PI / numObjects;
        float x = center.x+(Mathf.Sin(angle) * radius);
        float y = center.y+(Mathf.Cos(angle) * radius);
        return new Vector2(x, y);
    }

    void UpdateLineCount()
    {
        if(lineCountTxt!=null)lineCountTxt.text = "Line Count : " + numObjects;
    }

    public void IncreaseLineCount()
    {
        numObjects += 2;
        UpdateLineCount();
    }
    public void DecreaseLineCount()
    {
        numObjects -= 2;
        UpdateLineCount();
    }
    public void ResetLineCount()
    {
        numObjects = 10;
        UpdateLineCount();
    }
    public void SpawnLines()
    {
        Vector2 center = transform.position;

        for (int i = 0; i < numObjects; i++)
        {
            Vector2 pos = RandomCircle(i, radius,center);
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, center - pos);
            GameObject soundLineObj =Instantiate(soundLinePrefab, pos, rot);
            Destroy(soundLineObj, destroyObjectAfter);
        }
    }
}
