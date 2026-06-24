using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GerenciadorLuta : MonoBehaviour
{
    public static GerenciadorLuta Instancia;

    [Header("Lutadores do Jogador 1 (Esquerda)")]
    public GameObject bonecoTchunflayJ1;
    public GameObject bonecoTchovisJ1;

    [Header("Lutadores do Jogador 2 (Direita)")]
    public GameObject bonecoTchunflayJ2;
    public GameObject bonecoTchovisJ2;

    [Header("Sons do Jogo")]
    public AudioClip musicaFundo;
    public AudioClip musicaFimJogo;
    private AudioSource audioSource;

    [Header("Interface (UI)")]
    public Slider barraVidaJ1;
    public Slider barraVidaJ2;
    public TextMeshProUGUI textoTempo;
    public GameObject fundoFim;
    public TextMeshProUGUI textoVitoria;

    [Header("Configurações da Luta")]
    public float tempoMaximo = 90f;

    // as privadas da classe
    private float tempoAtual;
    private bool lutaAcabou = false;


    void Awake()
    {
        Instancia = this;
    }

    void Start()
    {
        // desativa os bonecos
        bonecoTchunflayJ1.SetActive(false);
        bonecoTchovisJ1.SetActive(false);
        bonecoTchunflayJ2.SetActive(false);
        bonecoTchovisJ2.SetActive(false);

        // ativa apenas o personagem escolhido de cada jogador
        string escolhidoJ1 = DadosDoJogo.PersonagemJ1;
        if (escolhidoJ1 == "Tchunflay")
            bonecoTchunflayJ1.SetActive(true);
        else if (escolhidoJ1 == "Tchovis")
            bonecoTchovisJ1.SetActive(true);

        string escolhidoJ2 = DadosDoJogo.PersonagemJ2;
        if (escolhidoJ2 == "Tchunflay")
            bonecoTchunflayJ2.SetActive(true);
        else if (escolhidoJ2 == "Tchovis")
            bonecoTchovisJ2.SetActive(true);

        // força as barras de vida a começarem cheias
        PrepararBarraVida(barraVidaJ1);
        PrepararBarraVida(barraVidaJ2);

        // configura o relógio
        Time.timeScale = 1f;
        tempoAtual = tempoMaximo;

        // Esconde o painel inteiro de fim de jogo para a luta começar limpa
        if (fundoFim != null) fundoFim.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && musicaFundo != null)
        {
            audioSource.clip = musicaFundo;
            audioSource.loop = true; // Deixa a música de fundo tocando em loop
            audioSource.Play();
        }
    }

    void Update()
    {
        if (lutaAcabou) return;

        tempoAtual -= Time.deltaTime;
        textoTempo.text = Mathf.Ceil(tempoAtual).ToString();

        if (tempoAtual <= 0)
        {
            tempoAtual = 0;
            AcabouOTempo();
        }
    }

    void PrepararBarraVida(Slider barra)
    {
        if (barra == null) return;

        barra.minValue = 0f;
        barra.maxValue = 1f;
        barra.value = 1f;
        barra.normalizedValue = 1f;
    }

    public void AtualizarVida(bool ehJogador1, float vidaAtual, float vidaMaxima)
    {
        if (ehJogador1)
            barraVidaJ1.value = vidaAtual / vidaMaxima;
        else
            barraVidaJ2.value = vidaAtual / vidaMaxima;
    }

    public void FinalizarLuta(string mensagemVitoria)
    {
        lutaAcabou = true;

        // Altera o texto com o vencedor da vez
        textoVitoria.text = mensagemVitoria;

        // LIGA O PAINEL MÃE! (Isso faz o fundo, o texto e os botões aparecerem juntos)
        if (fundoFim != null) fundoFim.SetActive(true);

        Time.timeScale = 0f;
        if (audioSource != null)
        {
            audioSource.Stop();
            if (musicaFimJogo != null)
            {
                audioSource.clip = musicaFimJogo;
                audioSource.loop = false; // Música de fim de jogo toca só uma vez
                audioSource.Play();
            }
        }
    }

    void AcabouOTempo()
    {
        if (barraVidaJ1.value > barraVidaJ2.value)
            FinalizarLuta("TEMPO ESGOTADO!\nJOGADOR 1 VENCEU!");
        else if (barraVidaJ2.value > barraVidaJ1.value)
            FinalizarLuta("TEMPO ESGOTADO!\nJOGADOR 2 VENCEU!");
        else
            FinalizarLuta("TEMPO ESGOTADO!\nEMPATE!");
    }

    public void ReiniciarLuta()
    {
        Time.timeScale = 1f; //descongela o tempo, senão o jogo reinicia travado!
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //recarrega a cena atual
    }

    public void IrMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}