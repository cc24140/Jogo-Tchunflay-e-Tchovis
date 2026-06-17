using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleJogador : MonoBehaviour
{
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

    [Header("Sprites de Combate")]
    public Sprite spriteNormal;
    public Sprite spriteSoco;
    public Sprite spriteChute;

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


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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

        if (!estaAtacando)
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

        if (estaAtacando) movimentoHorizontal = 0;
    }

    void FixedUpdate()
    {
        if (estaMorto) return;

        if (!estaAtacando)
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

    public void TomarDano(float dano)
    {
        if (estaMorto) return;

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
        GerenciadorLuta.Instancia.FinalizarLuta(textoVitoria);
    }

}
