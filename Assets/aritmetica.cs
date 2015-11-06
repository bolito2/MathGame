using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class aritmetica : MonoBehaviour {

    int num1, num2;
    public Text operacion;
    public InputField input;
    public AudioClip error, success;
    AudioSource audioSource;


    public string[] signos = new string[4] {"+" , "-" , "x", "÷"};

    string signo;

	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        CrearPregunta();
	}

	void CrearPregunta ()
    {
        input.Select();
        input.ActivateInputField();

        num1 = Random.Range(1, 100);
        num2 = Random.Range(1, 100);

        signo = signos[Random.Range(0, 4)];
        if (signo == "÷")
            num1 = num2 * Random.Range(1, 11);
        if (signo == "x")
            num1 = Random.Range(1, 11);
        
        operacion.text = num1 + " " + signo + " " + num2;
    }

    public void ResultadoEscrito()
    {
        switch (signo)
        {
            case "+":
                if (input.text == (num1 + num2).ToString())
                {
                    audioSource.clip = success;
                    audioSource.volume = 1;
                    audioSource.Play();

                    CrearPregunta();
                }
                else
                {
                    audioSource.clip = error;
                    audioSource.volume = 0.3f;
                    audioSource.Play();

                    input.Select();
                    input.ActivateInputField();
                }
                break;
            case "-":
                if (input.text == (num1 - num2).ToString())
                {
                    audioSource.clip = success;
                    audioSource.volume = 1;
                    audioSource.Play();

                    CrearPregunta();
                }
                else
                {
                    audioSource.clip = error;
                    audioSource.volume = 0.3f;
                    audioSource.Play();

                    input.Select();
                    input.ActivateInputField();
                }
                break;
            case "x":
                if (input.text == (num1 * num2).ToString())
                {
                    audioSource.clip = success;
                    audioSource.volume = 1;
                    audioSource.Play();

                    CrearPregunta();
                }
                else
                {
                    audioSource.clip = error;
                    audioSource.volume = 0.3f;
                    audioSource.Play();

                    input.Select();
                    input.ActivateInputField();
                }
                break;
            case "÷":
                if (input.text == (num1 / num2).ToString())
                {
                    audioSource.clip = success;
                    audioSource.volume = 1;
                    audioSource.Play();

                    CrearPregunta();
                }
                else
                {
                    audioSource.clip = error;
                    audioSource.volume = 0.3f;
                    audioSource.Play();

                    input.Select();
                    input.ActivateInputField();
                }
                break;
            default:
                break;
        }
        input.text = "";
    }
}
