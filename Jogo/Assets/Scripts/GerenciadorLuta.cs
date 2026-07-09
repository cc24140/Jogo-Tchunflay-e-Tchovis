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

    [Header("Configura��es da Luta")]
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

        
        string escolhidoJ1 = DadosDoJogo.PersonagemJ1;
        GameObject bonecoAtivoJ1 = null;

        if (escolhidoJ1 == "Tchunflay")
        {
            bonecoTchunflayJ1.SetActive(true);
            bonecoAtivoJ1 = bonecoTchunflayJ1;
        }
        else if (escolhidoJ1 == "Tchovis")
        {
            bonecoTchovisJ1.SetActive(true);
            bonecoAtivoJ1 = bonecoTchovisJ1;
        }

        
        string escolhidoJ2 = DadosDoJogo.PersonagemJ2;
        GameObject bonecoAtivoJ2 = null;

        if (escolhidoJ2 == "Tchunflay")
        {
            bonecoTchunflayJ2.SetActive(true);
            bonecoAtivoJ2 = bonecoTchunflayJ2;
        }
        else if (escolhidoJ2 == "Tchovis")
        {
            bonecoTchovisJ2.SetActive(true);
            bonecoAtivoJ2 = bonecoTchovisJ2;
        }

       
        if (bonecoAtivoJ2 != null && bonecoAtivoJ1 != null)
        {
            ConfigurarControleJ2(bonecoAtivoJ2, bonecoAtivoJ1.transform);
        }

        // for�a as barras de vida a come�arem cheias
        PrepararBarraVida(barraVidaJ1);
        PrepararBarraVida(barraVidaJ2);

        Time.timeScale = 1f;
        tempoMaximo = PlayerPrefs.GetFloat("tempoConfigurado", 91f);
        tempoAtual = tempoMaximo;

        if (fundoFim != null) fundoFim.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && musicaFundo != null)
        {
            audioSource.clip = musicaFundo;
            audioSource.loop = true;
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

       
        textoVitoria.text = mensagemVitoria;

  
        if (fundoFim != null) fundoFim.SetActive(true);

        Time.timeScale = 0f;
        if (audioSource != null)
        {
            audioSource.Stop();
            if (musicaFimJogo != null)
            {
                audioSource.clip = musicaFimJogo;
                audioSource.loop = false; //m�sica de fim de jogo toca s� uma vez
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
        Time.timeScale = 1f; //descongela o tempo, sen�o o jogo reinicia travado!
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //recarrega a cena atual
    }

    public void IrMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ConfigurarControleJ2(GameObject bonecoJ2, Transform alvo)
    {
        ControleJogador controleHumano = bonecoJ2.GetComponent<ControleJogador>();
        IAFacil iaFacil = bonecoJ2.GetComponent<IAFacil>();
        IAMedio iaMedio = bonecoJ2.GetComponent<IAMedio>();
        IADificil iaDificil = bonecoJ2.GetComponent<IADificil>();

        if (DadosDoJogo.ModoJogo == "IA")
        {
            if (controleHumano != null) controleHumano.controladoPorIA = true;

            if (iaFacil != null) iaFacil.enabled = false;
            if (iaMedio != null) iaMedio.enabled = false;
            if (iaDificil != null) iaDificil.enabled = false;

            string dif = DadosDoJogo.Dificuldade.ToLower().Replace("�", "a").Replace("�", "i");

 
            if (dif == "facil" && iaFacil != null)
            {
                iaFacil.enabled = true;
                iaFacil.DefinirAlvo(alvo);
            }
            else if (dif == "medio" && iaMedio != null)
            {
                iaMedio.enabled = true;
                iaMedio.DefinirAlvo(alvo);
            }
            else if (dif == "dificil" && iaDificil != null)
            {
                iaDificil.enabled = true;
                iaDificil.DefinirAlvo(alvo);
            }
        }
        else 
        {
            if (controleHumano != null) controleHumano.controladoPorIA = false;

            if (iaFacil != null) iaFacil.enabled = false;
            if (iaMedio != null) iaMedio.enabled = false;
            if (iaDificil != null) iaDificil.enabled = false;
        }
    }

}