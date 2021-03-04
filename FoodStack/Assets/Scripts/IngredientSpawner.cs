using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    GameObject[] ingredientSpawns;
    //List of ingredients to be spawned;
    //[SerializeField]
    //List<Ingredient> ingredients = new List<Ingredient>();

    // Start is called before the first frame update
    void Start()
    {
        ingredientSpawns = GameObject.FindGameObjectsWithTag("IngredientSpawn");
    }

    public void SpawnIngredient(Ingredient ingredient)
    {
        int spawnIndex=Random.Range(0, ingredientSpawns.Length);
        ingredient.Initialize(ingredientSpawns[spawnIndex].transform.position);
    }
}
