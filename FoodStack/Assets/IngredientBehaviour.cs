using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBehaviour : MonoBehaviour
{
    IngredientType ingredientType;
    float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetIngredientType(IngredientType type)
    {
        ingredientType = type;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.up*Time.deltaTime*speed);
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            EventManager.instance.IngredientHitPlayerTrigger(ingredientType);
            Destroy(gameObject);
        }
    }
}
public enum IngredientType
{
    Tomato,
    Patty
}
