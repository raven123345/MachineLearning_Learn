using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    public float r;
    public float g;
    public float b;
    public float timeToDie = 0f;

    bool dead = false;
    SpriteRenderer sRenderer;
    Collider2D sCollider;

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        sCollider = GetComponent<Collider2D>();
        sRenderer.color = new Color(r, g, b);
    }


    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        dead = true;
        sRenderer.enabled = false;
        sCollider.enabled = false;

        timeToDie = PopulationManager.elapsed;
    }
}
