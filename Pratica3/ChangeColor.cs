using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    void Awake() 
    {
        Debug.Log("Estou associado a um objeto da tela atual!"); 
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("O jogador acabou de pressionar o botão PLAY.");
    }

    // Update is called once per frame
    void Update()
    {
        // Verificaremos digitação de teclas pelo jogador e mudaremos cores
        if (Input.GetKeyDown(KeyCode.R))
            this.GetComponent<SpriteRenderer>().color = Color.red;
        if (Input.GetKeyDown(KeyCode.G))
            this.GetComponent<SpriteRenderer>().color = Color.green;
        if (Input.GetKeyDown(KeyCode.Y))
            this.GetComponent<SpriteRenderer>().color = Color.yellow;
        if (Input.GetKeyDown(KeyCode.C))
            this.GetComponent<SpriteRenderer>().color = Color.cyan;  
    }
}
