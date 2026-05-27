using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FazAndar : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer personagemSpriteRenderer;
    void Awake()
    {
        personagemSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        
        if (x < 0)  // vira para a esquerda
           personagemSpriteRenderer.flipX = true;
        else
          if (x > 0)  // vira para a direita
             personagemSpriteRenderer.flipX = false;
             // se for 0, fica como está

        float y= Input.GetAxis("Vertical");
        Vector2 movimento = new Vector2(x, y) * Time.deltaTime;
        transform.Translate(movimento);
    }
}
