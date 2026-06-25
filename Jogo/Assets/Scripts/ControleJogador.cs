using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleJogador : MonoBehaviour
{
    [Header("Efeitos de Áudio")]
    public AudioClip somDarGolpe;
    public AudioClip somTomarDano;
    private AudioSource audioSource;

    [Header("Combate e Vida")]
    public float vidaMaxima = 100f;
    private float vidaAtual;
    public float danoSoco = 10f;
    public float danoChute = 15f;

    [Header("Hitbox (Onde o soco bate)")]
    public Transform pontoDeAtaque;
    public float raioDeAtaque = 0.5f;
    public float ajusteDoRaioNaEscala = 0.25f;

    [Header("Configurações de Movimento")]
    public float velocidade = 8f;
    public float forcaPulo = 12f;

    [Header("Identificação do Jogador")]
    public bool ehJogador1 = true;
    public bool controladoPorIA = false;

    [Header("Sprites de Combate")]
    public Sprite spriteNormal;
    public Sprite spriteHadukenPose;
    public Sprite spriteSoco;
    public Sprite spriteChute;
    public GameObject prefabProjetil;
    public float distanciaSpawnHaduken = 0.6f;

    [Header("Animação de Caminhada")]
    public Sprite spriteAndando;
    public float velocidadeAnimacao = 0.15f;

    // as privadas da classe
    private float tempoAnimacaoAtual;
    private bool mostrandoPasso = false;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float movimentoHorizontal;
    private bool estaNoChao;
    private bool estaAtacando = false;
    private bool olhandoParaDireita;
    private bool spritesBaseOlhamParaDireita;
    private bool estaMorto = false;
    private bool estaLancandoHaduken = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if (spriteNormal != null) 
            spriteRenderer.sprite = spriteNormal;

        spritesBaseOlhamParaDireita = ehJogador1;
        olhandoParaDireita = ehJogador1;
        AtualizarDirecaoVisual();

        vidaAtual = vidaMaxima;
    }

    void Update()
    {
        if (estaMorto) return;
        if (estaLancandoHaduken)
        {
            return;
        }

        // para eles andarem
        if (!estaAtacando)
        {
            if (movimentoHorizontal != 0 && estaNoChao) 
                AnimarCaminhada();
            else if (movimentoHorizontal == 0 && estaNoChao)
            {
                spriteRenderer.sprite = spriteNormal;
                mostrandoPasso = false;
                tempoAnimacaoAtual = 0f;
            }
        }

        if (!estaAtacando && !estaLancandoHaduken && !controladoPorIA)
        {
            if (ehJogador1)
            {
                if (Input.GetKey(KeyCode.A)) 
                    movimentoHorizontal = -1;
                else if (Input.GetKey(KeyCode.D)) 
                    movimentoHorizontal = 1;
                else 
                    movimentoHorizontal = 0;

                if (Input.GetKeyDown(KeyCode.W) && estaNoChao) 
                    Pular();
                if (Input.GetKeyDown(KeyCode.Q)) 
                    StartCoroutine(ExecutarAtaque(spriteSoco, danoSoco));
                if (Input.GetKeyDown(KeyCode.E)) 
                    StartCoroutine(ExecutarAtaque(spriteChute, danoChute));
                if (Input.GetKeyDown(KeyCode.R)) 
                    StartCoroutine(LancarHaduken());
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow)) 
                    movimentoHorizontal = -1;
                else if (Input.GetKey(KeyCode.RightArrow)) 
                    movimentoHorizontal = 1;
                else 
                    movimentoHorizontal = 0;

                if (Input.GetKeyDown(KeyCode.UpArrow) && estaNoChao) 
                    Pular();
                if (Input.GetKeyDown(KeyCode.J)) 
                    StartCoroutine(ExecutarAtaque(spriteSoco, danoSoco));
                if (Input.GetKeyDown(KeyCode.K)) 
                    StartCoroutine(ExecutarAtaque(spriteChute, danoChute));
                if (Input.GetKeyDown(KeyCode.L)) 
                    StartCoroutine(LancarHaduken());
            }
        }

        if (movimentoHorizontal > 0)
        {
            olhandoParaDireita = true;
            AtualizarDirecaoVisual();
        }
        else if (movimentoHorizontal < 0)
        {
            olhandoParaDireita = false;
            AtualizarDirecaoVisual();
        }

        if (estaAtacando || estaLancandoHaduken) movimentoHorizontal = 0;
    }

    void FixedUpdate()
    {
        if (estaMorto) return;

        if (!estaAtacando && !estaLancandoHaduken)
            rb.linearVelocity = new Vector2(movimentoHorizontal * velocidade, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    void AtualizarDirecaoVisual()
    {
        transform.eulerAngles = Vector3.zero;
        spriteRenderer.flipX = spritesBaseOlhamParaDireita != olhandoParaDireita;

        if (pontoDeAtaque != null)
        {
            float distanciaDoAtaque = Mathf.Abs(pontoDeAtaque.localPosition.x);
            float posicaoX = olhandoParaDireita ? distanciaDoAtaque : -distanciaDoAtaque;
            pontoDeAtaque.localPosition = new Vector3(posicaoX, pontoDeAtaque.localPosition.y, 0);
        }
    }

    void AnimarCaminhada()
    {
        
        if (estaLancandoHaduken) return;

        if (spriteAndando == null) return;
        tempoAnimacaoAtual += Time.deltaTime;
        if (tempoAnimacaoAtual >= velocidadeAnimacao)
        {
            tempoAnimacaoAtual = 0f;
            mostrandoPasso = !mostrandoPasso;
            spriteRenderer.sprite = mostrandoPasso ? spriteAndando : spriteNormal;
        }
    }

    void Pular()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
        estaNoChao = false;
    }

    IEnumerator ExecutarAtaque(Sprite spriteDoAtaque, float danoDoAtaque)
    {
        if (spriteDoAtaque == null) yield break;

        estaAtacando = true;
        spriteRenderer.sprite = spriteDoAtaque;
        AtualizarDirecaoVisual();
        if (audioSource != null && somDarGolpe != null)
        {
            audioSource.PlayOneShot(somDarGolpe); // Toca o som do vento do soco/chute
        }

        if (pontoDeAtaque != null)
        {
            Collider2D[] coisasAcertadas = Physics2D.OverlapCircleAll(pontoDeAtaque.position, CalcularRaioAtaque());
            HashSet<ControleJogador> jogadoresAcertados = new HashSet<ControleJogador>();

            foreach (Collider2D coisa in coisasAcertadas)
            {
                if (coisa.gameObject == this.gameObject) continue;

                // ignora objetos inativos
                if (!coisa.gameObject.activeInHierarchy) continue;

                ControleJogador outroJogador = coisa.GetComponent<ControleJogador>();
                if (outroJogador == null) outroJogador = coisa.GetComponentInParent<ControleJogador>();

                if (outroJogador != null &&
                    outroJogador.ehJogador1 != this.ehJogador1 &&
                    jogadoresAcertados.Add(outroJogador))
                {
                    outroJogador.TomarDano(danoDoAtaque);
                }
            }
        }

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.sprite = spriteNormal;
        AtualizarDirecaoVisual();
        estaAtacando = false;
    }

    float CalcularRaioAtaque()
    {
        float menorEscala = Mathf.Min(Mathf.Abs(transform.lossyScale.x), Mathf.Abs(transform.lossyScale.y));
        return raioDeAtaque * Mathf.Max(1f, menorEscala * ajusteDoRaioNaEscala);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.ToLower().Contains("chao") || collision.gameObject.CompareTag("Finish"))
        {
            estaNoChao = true;
        }
    }

    public void TocarSomPersonalizado(AudioClip som)
    {
        if (audioSource != null && som != null)
        {
            audioSource.PlayOneShot(som);
        }
    }
    public void TomarDano(float dano, bool tocarSom = true)
    {
        if (estaMorto) return;

        if (tocarSom && audioSource != null && somTomarDano != null)
        {
            audioSource.PlayOneShot(somTomarDano); // Toca o som de impacto/grito de dor
        }

        vidaAtual -= dano;

        if (GerenciadorLuta.Instancia != null)
        {
            GerenciadorLuta.Instancia.AtualizarVida(ehJogador1, vidaAtual, vidaMaxima);
        }

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        estaMorto = true;
        vidaAtual = 0;
        rb.linearVelocity = Vector2.zero;
        transform.eulerAngles = new Vector3(0, 0, -90);

        string textoVitoria = ehJogador1 ? "K.O!\nJOGADOR 2 VENCEU!" : "K.O!\nJOGADOR 1 VENCEU!";

        if (GerenciadorLuta.Instancia != null)
        {
            GerenciadorLuta.Instancia.FinalizarLuta(textoVitoria);
        }
    }

    IEnumerator LancarHaduken()
    {
        if (prefabProjetil == null) yield break;

        estaLancandoHaduken = true;

        // tem q trocar o sprite do personagem para a pose de "Lançamento" (K.O. style)
        // spriteRenderer.sprite = spriteHadukenPose;
        Sprite spriteOriginal = spriteRenderer.sprite;
        if (spriteHadukenPose != null)
        {
            spriteRenderer.sprite = spriteHadukenPose;
        }

        float direcao = olhandoParaDireita ? 1f : -1f;
        float distanciaSpawn = Mathf.Max(1.5f, Mathf.Abs(transform.lossyScale.x) * distanciaSpawnHaduken) - 90;

        //instancia o projétil em uma posição levemente na frente do jogador
        Vector3 posicaoSpawn = transform.position + new Vector3(direcao * distanciaSpawn, 0f, 0f);
        GameObject novoHaduken = Instantiate(prefabProjetil, posicaoSpawn, Quaternion.identity);
        HadukenScript haduken = novoHaduken.GetComponent<HadukenScript>();
        if (haduken != null)
        {
            haduken.DefinirDono(this);
            if (audioSource != null && haduken.somLancamento != null)
                audioSource.PlayOneShot(haduken.somLancamento);
            else
                haduken.TocarSomLancamento(posicaoSpawn);
        }

        SpriteRenderer renderizadorHaduken = novoHaduken.GetComponent<SpriteRenderer>();
        if (renderizadorHaduken != null)
            renderizadorHaduken.sortingOrder = spriteRenderer.sortingOrder + 1;

        //passa a direção para o projétil saber para onde ir, mantendo o tamanho do prefab
        Vector3 escalaProjetil = novoHaduken.transform.localScale;
        escalaProjetil.x = Mathf.Abs(escalaProjetil.x) * direcao;
        novoHaduken.transform.localScale = escalaProjetil;

        yield return new WaitForSeconds(0.4f); //pequeno atraso na pose de lançamento
        spriteRenderer.sprite = spriteOriginal;

        estaLancandoHaduken = false;
    }

    public bool PodeAgirIA()
    {
        //iA só pode agir se não estiver morta, nem atacando, nem lançando poder
        return !estaMorto && !estaAtacando && !estaLancandoHaduken;
    }

    public void PararMovimentoIA()
    {
        movimentoHorizontal = 0f;
    }

    public void DefinirMovimentoIA(float direcao)
    {
        movimentoHorizontal = direcao;
    }

    public void SocoIA()
    {
        if (PodeAgirIA())
        {
            StartCoroutine(ExecutarAtaque(spriteSoco, danoSoco));
        }
    }

    public void ChuteIA()
    {
        if (PodeAgirIA())
        {
            StartCoroutine(ExecutarAtaque(spriteChute, danoChute));
        }
    }

    public void HadukenIA()
    {
        if (PodeAgirIA())
        {
            StartCoroutine(LancarHaduken());
        }
    }

    public bool EstaNoChaoIA()
    {
        return estaNoChao;
    }

    public void PularIA()
    {
        if (estaNoChao && PodeAgirIA())
        {
            Pular();
        }
    }

}
