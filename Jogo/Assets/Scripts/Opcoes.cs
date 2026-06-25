using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Opcoes : MonoBehaviour
{
    public Slider volumeSlider;
    public TMP_InputField timer;

    public static float tempoConfigurado = 91f;

    void Start()
    {
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
        timer.text = tempoConfigurado.ToString();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void Confirmar()
    {
        if (float.TryParse(timer.text, out float tempo))
        {
            tempo = Mathf.Clamp(tempo, 1f, 91f);
            PlayerPrefs.SetFloat("tempoConfigurado", tempo);
            PlayerPrefs.Save();
            Debug.Log("Tempo salvo: " + tempo);
        }
        else
            Debug.LogWarning("Digite um número válido!");
    }
}