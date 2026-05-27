using UnityEditor;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public GameObject prefabBala;

    public GameObject atirador;

    private Transform _pontoDeTiro;

    void Awake()
    {
        _pontoDeTiro = transform.Find("PontoDeTiro")   ;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Atirar()
    {
        if (prefabBala != null && _pontoDeTiro != null && atirador != null)
        {
            GameObject minhaBala = Instantiate(prefabBala, _pontoDeTiro.position,
                                            Quaternion.identity) as GameObject;
            Bala scriptDaBala = minhaBala.GetComponent<Bala>();
            if (atirador.transform.localScale.x < 0f)
                scriptDaBala.direcao = Vector2.left;
            else
                scriptDaBala.direcao = Vector2.right;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Atirar();

        if (Input.GetButtonDown("Fire2"))
        {
            Invoke(nameof(Atirar), 0.2f);
            Invoke(nameof(Atirar), 0.4f);
            Invoke(nameof(Atirar), 0.6f);
        }
    }
}
