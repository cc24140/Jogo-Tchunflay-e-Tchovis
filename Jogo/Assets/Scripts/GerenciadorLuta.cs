using UnityEngine;

public class GerenciadorLuta : MonoBehaviour
{
    [Header("Lutadores do Jogador 1 (Esquerda)")]
    public GameObject bonecoCiborgueJ1;
    public GameObject bonecoNinjaJ1;
    public GameObject bonecoMariJ1;

    [Header("Lutadores do Jogador 2 (Direita)")]
    public GameObject bonecoCavaleiraJ2;
    public GameObject bonecoMagoJ2;
    public GameObject bonecoMariJ2;

    void Start()
    {
        // 1. CHECANDO O JOGADOR 1
        string escolhidoJ1 = DadosDoJogo.PersonagemP1;

        if (escolhidoJ1 == "Tchovis") // Troque pelo texto EXATO que digitou no botão!
        {
            bonecoCiborgueJ1.SetActive(true); // Liga o Ciborgue!
        }
        else if (escolhidoJ1 == "Tchunflay")
        {
            bonecoNinjaJ1.SetActive(true); // Liga o Ninja!
        }
        else if(escolhidoJ1 == "Mari"){
            bonecoMariJ1 .SetActive(true);
        }

        // 2. CHECANDO O JOGADOR 2
        string escolhidoJ2 = DadosDoJogo.PersonagemP2;

        if (escolhidoJ2 == "Tchovis") // Troque pelo texto EXATO do botão do J2!
        {
            bonecoCavaleiraJ2.SetActive(true); // Liga a Cavaleira!
        }
        else if (escolhidoJ2 == "Tchunflay")
        {
            bonecoMagoJ2.SetActive(true); // Liga o Mago!
        }
        else if (escolhidoJ1 == "Mari")
        {
            bonecoMariJ2.SetActive(true);
        }
    }
}