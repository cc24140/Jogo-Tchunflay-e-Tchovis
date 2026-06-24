using UnityEngine;

public class GerenciadorPause : MonoBehaviour
{
    [Header("Tela de Pause")]
    public GameObject painelPause; 

    private bool jogoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))    //qnd clica no espaço
        {
            if (jogoPausado)
            {
                DespausarJogo();
            }
            else
            {
                PausarJogo();
            }
        }
    }

    void PausarJogo()
    {
        painelPause.SetActive(true);

        //congela o tempo do jogo
        Time.timeScale = 0f;

        jogoPausado = true;
    }

    void DespausarJogo()
    {
        painelPause.SetActive(false);

        //faz o tempo voltar a correr normalmente
        Time.timeScale = 1f;

        jogoPausado = false;
    }
}