using UnityEngine;

public class HadukenScript : MonoBehaviour
{
    public float velocidadeProjetil = 600f;
    public int dano = 10;

    private Rigidbody2D rb;
    private ControleJogador dono;

    [Header("Sons do Hadouken")]
    public AudioClip somLancamento;
    public AudioClip somImpacto;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();


        float direcao = Mathf.Sign(transform.localScale.x);
        rb.linearVelocity = new Vector2(velocidadeProjetil * direcao, 0f);

        Destroy(gameObject, 3f);
    }

    public void DefinirDono(ControleJogador jogador)
    {
        dono = jogador;
    }

    public void TocarSomLancamento(Vector3 posicao)
    {
        if (somLancamento != null)
        {
            AudioSource.PlayClipAtPoint(somLancamento, posicao);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ControleJogador jogadorAtingido = other.GetComponent<ControleJogador>();
        if (jogadorAtingido == null)
            jogadorAtingido = other.GetComponentInParent<ControleJogador>();

        if (jogadorAtingido != null && jogadorAtingido == dono)
        {
            return;
        }

        if (jogadorAtingido != null && jogadorAtingido != dono)
        {
            jogadorAtingido.TomarDano(dano, false);
            jogadorAtingido.TocarSomPersonalizado(somImpacto);

            Destroy(gameObject);
            return;
        }

        if (other.gameObject.name.ToLower().Contains("parede") || other.gameObject.name.ToLower().Contains("chao") || other.gameObject.name.ToLower().Contains("barreira"))
        {
            if (somImpacto != null)
            {
                AudioSource.PlayClipAtPoint(somImpacto, transform.position);
            }
            Destroy(gameObject);
        }
    }
}
