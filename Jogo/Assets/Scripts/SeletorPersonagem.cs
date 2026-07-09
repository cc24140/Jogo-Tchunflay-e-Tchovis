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
    public RectTransform botaoTchovisJ1;
    public RectTransform botaoTchunflayJ1;

    [Header("Botoes do Jogador 2 (Direita)")]
    public RectTransform botaoTchovisJ2;
    public RectTransform botaoTchunflayJ2;

    [Header("Música do Menu")]
    public AudioClip musicaMenuInicial;
    private AudioSource audioSource;

    // as privadas da classe
    private bool J1Escolhido = false;
    private bool J2Escolhido = false;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (musicaMenuInicial != null)
        {
            audioSource.clip = musicaMenuInicial;
            audioSource.loop = true;
            audioSource.Play();
        }
        //força o jogo a começar sabendo que o P1 está com o boneco de cima selecionado
        DadosDoJogo.PersonagemJ1 = "Tchovis";
        J1Escolhido = true;

        //força o jogo a começar sabendo que o P2 está com o boneco de cima selecionado
        DadosDoJogo.PersonagemJ2 = "Tchovis";
        J2Escolhido = true;

        botaoConfirmar.gameObject.SetActive(true);
    }

    public void SelecionarJ1_Cima(string nomePersonagem)
    {
        DadosDoJogo.PersonagemJ1 = nomePersonagem;
        J1Escolhido = true;

        bordaJ1.gameObject.SetActive(true);
        bordaJ1.transform.position = botaoTchovisJ1.position;

        VerificarSelecao();
    }

    public void SelecionarJ1_Baixo(string nomePersonagem)
    {
        DadosDoJogo.PersonagemJ1 = nomePersonagem;
        J1Escolhido = true;

        bordaJ1.gameObject.SetActive(true);
        bordaJ1.transform.position = botaoTchunflayJ1.position;

        VerificarSelecao();
    }

    public void SelecionarJ2_Cima(string nomePersonagem)
    {
        DadosDoJogo.PersonagemJ2 = nomePersonagem;
        J2Escolhido = true;

        bordaJ2.gameObject.SetActive(true);
        bordaJ2.transform.position = botaoTchovisJ2.position;

        VerificarSelecao();
    }

    public void SelecionarJ2_Baixo(string nomePersonagem)
    {
        DadosDoJogo.PersonagemJ2 = nomePersonagem;
        J2Escolhido = true;

        bordaJ2.gameObject.SetActive(true);
        bordaJ2.transform.position = botaoTchunflayJ2.position;

        VerificarSelecao();
    }

    void VerificarSelecao()
    {
        if (J1Escolhido && J2Escolhido)
        {
            botaoConfirmar.gameObject.SetActive(true);
        }
    }

    public void ConfirmarEscolha()
    {
        SceneManager.LoadScene("CenaLuta");
    }
}
