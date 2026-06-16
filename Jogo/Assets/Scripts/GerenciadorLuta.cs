using UnityEngine;

public class GerenciadorLuta : MonoBehaviour
{
    [Header("Lutadores do Jogador 1 (Esquerda)")]
    
    public GameObject bonecoTchunflayJ1;
    public GameObject bonecoTchovisJ1;

    [Header("Lutadores do Jogador 2 (Direita)")]
  
    public GameObject bonecoTchunflayJ2;
    public GameObject bonecoTchovisJ2;

    void Start()
    {
        // 1. CHECANDO O JOGADOR 1
        string escolhidoJ1 = DadosDoJogo.PersonagemP1;

        if (escolhidoJ1 == "Tchunflay")
        {
            bonecoTchunflayJ1.SetActive(true);
        }
        else if(escolhidoJ1 == "Tchovis")
        {
            bonecoTchovisJ1.SetActive(true);
        }

        // 2. CHECANDO O JOGADOR 2
        string escolhidoJ2 = DadosDoJogo.PersonagemP2;

        if (escolhidoJ2 == "Tchunflay")
        {
            bonecoTchunflayJ2.SetActive(true);
        }
        else if (escolhidoJ1 == "Tchovis")
        {
            bonecoTchovisJ2.SetActive(true);
        }
    }
}