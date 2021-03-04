using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoSlot : MonoBehaviour
{
    Image ammoActiveImage;
    Image ammoUsedImage;
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void OnEnable()
    {
        ammoActiveImage = transform.Find("AmmoImage").GetComponent<Image>();
        ammoUsedImage = transform.Find("BlackedOutImage").GetComponent<Image>();
    }
    public void SetActive()
    {
        ammoActiveImage.enabled = true;
        ammoUsedImage.enabled = false;
    }
    public void AmmoUsed()
    {
        ammoActiveImage.enabled = false;
        ammoUsedImage.enabled = true;
    }
    public void Remove()
    {
        Destroy(gameObject);
    }
}
