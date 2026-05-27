using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidade = 6f;
    public Vector2 direcao;

    public float tempoDeVida = 3f;
    public Color corInicial = Color.white;
    public Color corFinal;
    private SpriteRenderer _srBala;
    private float _tempoInicial;

    void Awake()
    {
        _srBala = GetComponent<SpriteRenderer>();   
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _tempoInicial = Time.time; // horário inicial do script
        Destroy(this.gameObject, tempoDeVida);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movimento = direcao.normalized * velocidade * Time.deltaTime;
        transform.Translate(movimento);

        float _tempoDecorrido = Time.time - _tempoInicial;
        float _porcentagemJaCompleta = _tempoDecorrido / tempoDeVida;
        _srBala.color = Color.Lerp(corInicial, corFinal, _porcentagemJaCompleta);
    }
}
