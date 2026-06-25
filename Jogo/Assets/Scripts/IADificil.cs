using UnityEngine;

public class IADificil : MonoBehaviour
{
    public Transform alvo;
    public float distanciaAtaque = 210f;
    public float distanciaHaduken = 430f;
    public float distanciaMinima = 65f;
    public float intervaloDecisao = 0.38f;

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

        if (distanciaAbs > distanciaAtaque * 0.8f)
            jogador.DefinirMovimentoIA(Mathf.Sign(distanciaX));
        else if (distanciaAbs < distanciaMinima)
            jogador.DefinirMovimentoIA(-Mathf.Sign(distanciaX));
        else
            jogador.PararMovimentoIA();

        if (Time.time < proximaDecisao) return;
        proximaDecisao = Time.time + intervaloDecisao;

        if (distanciaAbs <= distanciaAtaque)
        {
            float escolha = Random.value;
            if (escolha < 0.45f)
                jogador.ChuteIA();
            else if (escolha < 0.85f)
                jogador.SocoIA();
            else
                jogador.HadukenIA();
        }
        else if (distanciaAbs <= distanciaHaduken)
        {
            if (Random.value < 0.75f)
                jogador.HadukenIA();
        }

        if (jogador.EstaNoChaoIA() && Random.value < 0.2f)
            jogador.PularIA();
    }
}
