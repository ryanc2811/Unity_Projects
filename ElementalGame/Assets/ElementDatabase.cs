using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementDatabase : MonoBehaviour
{
    public static ElementDatabase instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public float CalculateDamage (ElementType source,ElementType target)
    {
        switch (source)
        {
            default:
            case ElementType.None:
                return 1f;
            case ElementType.Water:
                switch (target)
                {
                    case ElementType.Fire:
                        return 1.5f;
                    case ElementType.Earth:
                        return 0.5f;
                    case ElementType.Water:
                        return 1f;
                }
                break;
            case ElementType.Fire:
                switch (target)
                {
                    case ElementType.Fire:
                        return 1f;
                    case ElementType.Earth:
                        return 1.5f;
                    case ElementType.Water:
                        return 0.5f;
                }
                break;
            case ElementType.Earth:
                switch (target)
                {
                    case ElementType.Fire:
                        return 0.5f;
                    case ElementType.Earth:
                        return 1f;
                    case ElementType.Water:
                        return 1.5f;
                }
                break;
        }
        return 0f;
    }
}
public enum ElementType
{
    Water,
    Fire,
    Earth,
    None
}
