using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeletorPersonagem : MonoBehaviour
{
    [Header("UI - Bordas de Selecao")]
    public Image bordaJ1;
    public Image bordaJ2;

    [Header("UI - Botao Confirmar")]
    public Button botaoConfirmar;

    [Header("Botoes do Jogador 1 (Esquerda)")]
    public RectTransform botaoPerso1;
    public RectTransform botaoPerso3;

    [Header("Botoes do Jogador 2 (Direita)")]
    public RectTransform botaoPerso2;
    public RectTransform botaoPerso4;

    private bool p1Escolhido = false;
    private bool p2Escolhido = false;
    void Start()
    {
        // Força o jogo a começar sabendo que o P1 está com o boneco de cima selecionado
        // TROQUE PELO NOME EXATO DO SEU PRIMEIRO PERSONAGEM (em letra minúscula se usou o código anterior)
        DadosDoJogo.PersonagemP1 = "Mari";
        p1Escolhido = true;

        // Força o jogo a começar sabendo que o P2 está com o boneco de cima selecionado
        // TROQUE PELO NOME EXATO DO SEU SEGUNDO PERSONAGEM
        DadosDoJogo.PersonagemP2 = "Mari";
        p2Escolhido = true;

        // Como ambos já começam escolhidos por padrão, libera o botão Confirmar direto!
        botaoConfirmar.gameObject.SetActive(true);
    }

    // --- FUNÇÕES DO JOGADOR 1 (ESQUERDA) ---

    public void SelecionarP1_Cima(string nomeDoBoneco)
    {
        DadosDoJogo.PersonagemP1 = nomeDoBoneco;
        p1Escolhido = true; // J1 escolheu alguém!

        bordaJ1.gameObject.SetActive(true);
        bordaJ1.transform.position = botaoPerso1.position;

        Debug.Log("P1 mudou para Cima: " + nomeDoBoneco);
        VerificarSelecao();
    }

    public void SelecionarP1_Baixo(string nomeDoBoneco)
    {
        DadosDoJogo.PersonagemP1 = nomeDoBoneco;
        p1Escolhido = true; // J1 escolheu alguém!

        bordaJ1.gameObject.SetActive(true);
        bordaJ1.transform.position = botaoPerso3.position;

        Debug.Log("P1 mudou para Baixo: " + nomeDoBoneco);
        VerificarSelecao();
    }

    // --- FUNÇÕES DO JOGADOR 2 (DIREITA) ---

    public void SelecionarP2_Cima(string nomeDoBoneco)
    {
        DadosDoJogo.PersonagemP2 = nomeDoBoneco;
        p2Escolhido = true; // J2 escolheu alguém!

        bordaJ2.gameObject.SetActive(true);
        bordaJ2.transform.position = botaoPerso2.position;

        Debug.Log("P2 mudou para Cima: " + nomeDoBoneco);
        VerificarSelecao();
    }

    public void SelecionarP2_Baixo(string nomeDoBoneco)
    {
        DadosDoJogo.PersonagemP2 = nomeDoBoneco;
        p2Escolhido = true; // J2 escolheu alguém!

        bordaJ2.gameObject.SetActive(true);
        bordaJ2.transform.position = botaoPerso4.position;

        Debug.Log("P2 mudou para Baixo: " + nomeDoBoneco);
        VerificarSelecao();
    }

    void VerificarSelecao()
    {
        // O botão de confirmar só aparece se AMBOS tiverem pelo menos um boneco selecionado
        if (p1Escolhido && p2Escolhido)
        {
            botaoConfirmar.gameObject.SetActive(true);
        }
    }

    public void ConfirmarEscolha()
    {
        // Lembra de trocar pelo nome EXATO da sua cena de luta!
        SceneManager.LoadScene("CenaLuta");
    }
}