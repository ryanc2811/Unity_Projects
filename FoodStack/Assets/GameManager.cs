using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public IngredientSpawner ingredientSpawner;
    public Ingredient Ingredient;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.OnIngredientHitPlayerTrigger += OnIngredientCollision;
    }
    void OnIngredientCollision(IngredientType type)
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ingredientSpawner.SpawnIngredient(Ingredient);
        }
    }
}
