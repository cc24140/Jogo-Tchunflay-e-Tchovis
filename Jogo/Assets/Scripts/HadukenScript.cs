using UnityEngine;

public class HadukenScript : MonoBehaviour
{
    public float velocidadeProjetil = 600f; // Quão rápido ele voa
    public int dano = 10; // Quanto dano ele causa (se você já tiver sistema de vida)

    private Rigidbody2D rb;
    private ControleJogador dono;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Define a velocidade inicial baseado na escala X do jogador
        // (Se o jogador estiver olhando pra esquerda (-1), ele voa pra esquerda)
        float direcao = Mathf.Sign(transform.localScale.x);
        rb.linearVelocity = new Vector2(velocidadeProjetil * direcao, 0f);

        // Destrói o projétil automaticamente depois de 3 segundos para não pesar o jogo
        Destroy(gameObject, 3f);
    }

    public void DefinirDono(ControleJogador jogador)
    {
        dono = jogador;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ControleJogador jogadorAtingido = other.GetComponent<ControleJogador>();
        if (jogadorAtingido == null)
            jogadorAtingido = other.GetComponentInParent<ControleJogador>();

        if (jogadorAtingido != null && jogadorAtingido != dono)
        {
            jogadorAtingido.TomarDano(dano);
            Destroy(gameObject);
        }
    }
}