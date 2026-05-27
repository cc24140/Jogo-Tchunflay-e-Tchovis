using UnityEngine;
using UnityEngine.SceneManagement;

public class TesteTeclas : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Debug.Log("Botão esquerdo do mouse foi pressionado sobre o Player!");

        if (Input.GetMouseButton(0))
            Debug.Log("Botão esquerdo do mouse mantido pressionado sobre o Player!");

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Botão esquerdo do mouse foi liberado sobre o Player!");
            //SceneManager.LoadScene("cena02");
        }
        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log("Player pula com espaço!");
        
        if (Input.GetButtonDown("Jump"))
            Debug.Log("Player salta com botão Jump!");

        float horizontal = Input.GetAxis("Horizontal"); // -1.0 a 1.0
        float vertical = Input.GetAxis("Vertical");     // -1.0 a 1.0

        if (horizontal != 0f)
            Debug.Log($"Eixo horizontal = {horizontal}");
        if (vertical != 0f)
            Debug.Log($"Eixo vertical = {vertical}");
    }
}
