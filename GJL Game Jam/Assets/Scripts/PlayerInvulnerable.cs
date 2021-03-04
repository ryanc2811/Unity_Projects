using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvulnerable : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerStats.Instance.Invulnerable)
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, .5f));
        else
            spriteRenderer.color = Color.white;
    }
}
