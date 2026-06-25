using UnityEngine;

public class IAFacil : MonoBehaviour
{
    public Transform alvo;
    public float distanciaAtaque = 170f;
    public float distanciaMinima = 90f;
    public float intervaloDecisao = 1.1f;

    private ControleJogador jogador;
    private float proximaDecisao;

    void Awake()
    {
        jogador = GetComponent<ControleJogador>();
    }

    public void DefinirAlvo(Transform novoAlvo)
    {
        alvo = novoAlvo;
    }

    void Update()
    {
        if (jogador == null || alvo == null || !jogador.PodeAgirIA())
        {
            jogador?.PararMovimentoIA();
            return;
        }

        float distanciaX = alvo.position.x - transform.position.x;
        float distanciaAbs = Mathf.Abs(distanciaX);

        if (distanciaAbs > distanciaAtaque)
            jogador.DefinirMovimentoIA(Mathf.Sign(distanciaX));
        else if (distanciaAbs < distanciaMinima)
            jogador.DefinirMovimentoIA(-Mathf.Sign(distanciaX));
        else
            jogador.PararMovimentoIA();

        if (Time.time < proximaDecisao) return;
        proximaDecisao = Time.time + intervaloDecisao;

        if (distanciaAbs <= distanciaAtaque && Random.value < 0.55f)
            jogador.SocoIA();
    }
}
