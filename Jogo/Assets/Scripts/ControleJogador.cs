using System.Collections;
using UnityEngine;

public class ControleJogador : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidade = 8f;
    public float forcaPulo = 12f;

    [Header("Identificação do Jogador")]
    public bool ehJogador1 = true;

    [Header("Sprites de Combate (Arrastar os da folha de sprite)")]
    public Sprite spriteNormal;
    public Sprite spriteSoco;
    public Sprite spriteChute;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float movimentoHorizontal;
    private bool estaNoChao;
    private bool estaAtacando = false;
    private bool olhandoParaDireita;
    private bool spritesBaseOlhamParaDireita;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteNormal != null)
        {
            spriteRenderer.sprite = spriteNormal;
        }

        spritesBaseOlhamParaDireita = ehJogador1;
        olhandoParaDireita = ehJogador1;

        // Configuração Inicial de fábrica para elas já nascerem se olhando:
        AtualizarDirecaoVisual();
    }

    void Update()
    {
        // 1. CAPTURAR OS CONTROLES SE NÃO ESTIVER ATACANDO
        if (!estaAtacando)
        {
            if (ehJogador1)
            {
                if (Input.GetKey(KeyCode.A)) movimentoHorizontal = -1;
                else if (Input.GetKey(KeyCode.D)) movimentoHorizontal = 1;
                else movimentoHorizontal = 0;

                if (Input.GetKeyDown(KeyCode.W) && estaNoChao) Pular();

                if (Input.GetKeyDown(KeyCode.Q)) StartCoroutine(ExecutarAtaque(spriteSoco));
                if (Input.GetKeyDown(KeyCode.E)) StartCoroutine(ExecutarAtaque(spriteChute));
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow)) movimentoHorizontal = -1;
                else if (Input.GetKey(KeyCode.RightArrow)) movimentoHorizontal = 1;
                else movimentoHorizontal = 0;

                if (Input.GetKeyDown(KeyCode.UpArrow) && estaNoChao) Pular();

                if (Input.GetKeyDown(KeyCode.J)) StartCoroutine(ExecutarAtaque(spriteSoco));
                if (Input.GetKeyDown(KeyCode.K)) StartCoroutine(ExecutarAtaque(spriteChute));
            }
        }

        // 2. VIRAR O PERSONAGEM CONFORME A DIRECAO ATUAL
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

        if (estaAtacando)
        {
            movimentoHorizontal = 0;
        }
    }

    void FixedUpdate()
    {
        if (!estaAtacando)
        {
            rb.linearVelocity = new Vector2(movimentoHorizontal * velocidade, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    void Pular()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
        estaNoChao = false;
    }

    IEnumerator ExecutarAtaque(Sprite spriteDoAtaque)
    {
        if (spriteDoAtaque == null) yield break;

        estaAtacando = true;

        spriteRenderer.sprite = spriteDoAtaque;
        AtualizarDirecaoVisual();

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.sprite = spriteNormal;
        AtualizarDirecaoVisual();
        estaAtacando = false;
    }

    void AtualizarDirecaoVisual()
    {
        transform.eulerAngles = Vector3.zero;

        // Os sprites do J1 ja olham para a direita; os da J2 ja olham para a esquerda.
        // So espelha quando a direcao atual for diferente da direcao original do sprite.
        spriteRenderer.flipX = spritesBaseOlhamParaDireita != olhandoParaDireita;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.ToLower().Contains("chao") || collision.gameObject.CompareTag("Finish"))
        {
            estaNoChao = true;
        }
    }
}