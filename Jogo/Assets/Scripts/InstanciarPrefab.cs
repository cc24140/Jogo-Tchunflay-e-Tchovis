using UnityEngine;

public class InstanciarPrefab : MonoBehaviour
{
    public GameObject prefab;
    public Transform ponto;
    public float tempoDeVida;
    

    public void Instanciar()
    {
        GameObject objetoCriado = Instantiate(prefab, ponto.position, Quaternion.identity) as GameObject;
    
        if (tempoDeVida > 0f)
            Destroy(objetoCriado, tempoDeVida);
    }
}
