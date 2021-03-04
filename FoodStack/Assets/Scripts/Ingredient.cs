using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Ingredient",menuName ="Ingredient")]
public class Ingredient : ScriptableObject
{
    public new string name;
    private GameObject prefab;
    public IngredientType type;
    public void Initialize(Vector3 position)
    {
        GameObject ingredient = Instantiate(prefab, position, Quaternion.identity, null);
        ingredient.GetComponent<IngredientBehaviour>().SetIngredientType(type);
    }
    //public void SpawnIngredient
}
