using UnityEngine;

public class IAMedio : MonoBehaviour
{
    public Transform alvo;
    public float distanciaAtaque = 190f;
    public float distanciaHaduken = 320f;
    public float distanciaMinima = 75f;
    public float intervaloDecisao = 0.65f;

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

        if (distanciaAbs <= distanciaAtaque)
        {
            if (Random.value < 0.55f)
                jogador.ChuteIA();
            else
                jogador.SocoIA();
        }
        else if (distanciaAbs <= distanciaHaduken && Random.value < 0.45f)
        {
            jogador.HadukenIA();
        }

        if (jogador.EstaNoChaoIA() && Random.value < 0.12f)
            jogador.PularIA();
    }
}
