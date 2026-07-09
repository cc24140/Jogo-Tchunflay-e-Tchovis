using UnityEngine;
using UnityEngine.UI; 

public class ControleModo : MonoBehaviour
{
    [Header("Painéis de Navegação")]
    public GameObject panelInicial;
    public GameObject panelModo;    
    public GameObject panelSelecao;
    public GameObject panelDoisJogadores;

    [Header("Botões dificuldade")]
    public Button btnDificil;
    public Button btnFacil;
    public Button btnMedio;

    [Header("Botões modo de jogo")]
    public Button btnIA;
    public Button btnDoisJogadores;

    void Start()
    {
        DadosDoJogo.ModoJogo = "DoisJogadores";
        DadosDoJogo.Dificuldade = "";
        MostrarBotoesDificuldade(false);
    }

    public void IrParaTelaDeModo()
    {
        if (panelInicial != null) panelInicial.SetActive(false);
        if (panelModo != null) panelModo.SetActive(true);
    }

    public void SelecionarModo(string modo)
    {
        DadosDoJogo.ModoJogo = modo;

        if (modo == "IA")
        {
            MostrarBotoesDificuldade(true);
        }
        else if (modo == "DoisJogadores")
        {
            MostrarBotoesDificuldade(false);
        }
    }

    public void SelecionarDificuldade(string dificul)
    {
        DadosDoJogo.Dificuldade = dificul;
        
    }

    public void AvancarParaSelecao()
    {
        if (panelModo != null) panelModo.SetActive(false);
        if (panelSelecao != null) panelSelecao.SetActive(true);
    }

    private void MostrarBotoesDificuldade(bool mostrar)
    {
        if (panelDoisJogadores != null) panelDoisJogadores.gameObject.SetActive(mostrar);
    }
}   
