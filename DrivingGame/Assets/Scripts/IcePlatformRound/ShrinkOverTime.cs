using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkOverTime : MonoBehaviour
{
    [SerializeField]private float shrinkRate;
    [SerializeField]private float shrinkFactor;
    [SerializeField] private float lowestPossibleSize;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(Shrink());
    }

    IEnumerator Shrink()
    {
        float timeSinceLastShrink = 0f;
        while (true)
        {
            if (transform.localScale.x > lowestPossibleSize)
            {
                if (Time.time > timeSinceLastShrink)
                {
                    timeSinceLastShrink = Time.time + shrinkRate;
                    transform.localScale -= new Vector3(1, 0, 1) * Time.deltaTime * shrinkFactor;
                }
            }
            yield return null;
        }
    }
}
