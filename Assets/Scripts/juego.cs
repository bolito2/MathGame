using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class juego: MonoBehaviour {

    int num1, num2;
    public Text operacion;
    public InputField input;
    public AudioClip error, success;
    AudioSource audioSource;

    private GameManager gameManager;

    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    string[] signos = new string[4] {"+" , "-" , "x", "÷"};
    string signo;
    public Text tiempo;
    public RectTransform barra;
    float posInicialBarra, posFinalBarra, diferencia;
    
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        CrearPregunta();

        posInicialBarra = posInicialBarra;
        posFinalBarra = -1150;
        diferencia = posFinalBarra - posInicialBarra;
    }
	
	public int tiempoPartida; //en vez de usar "120", cambié eso por una variable (Esta) que se toma desde el GameManager para el tiempo. así solo tener que -
	//cambiar esta variable en vez del script entero
	
    void Update()
    {
        switch (gameManager.modo)
        {
            case GameManager.Modo.contrareloj :
                int segundos = (int)tiempoPartida - (int)Time.timeSinceLevelLoad;
                int minutos = 0;
                while(segundos >= 60)
                {
                    segundos -= 60;
                    minutos++;
                }
                if(segundos < 10)
                    tiempo.text = minutos.ToString() + ":0" + segundos.ToString();
                else
                tiempo.text = minutos.ToString() + ":" + segundos.ToString();

                if(segundos < 0)
                {
                    tiempo.text = "0:00";
                }

                barra.anchoredPosition = new Vector2(posFinalBarra - diferencia * (tiempoPartida - Time.time)/tiempoPartida , barra.anchoredPosition.y);

                if(Time.timeSinceLevelLoad > tiempoPartida)
                {
                    JuegoTerminado();
                }
                break;

            default: break;
        }
    }

    void CrearPregunta()
    {
        switch (gameManager.tema)
        {
        case GameManager.Tema.aritmetica :
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
            break;

            default: break;
        }
    }

    public void ResultadoEscrito()
    {
        if (input.text != "")
        {
            switch (gameManager.tema)
            {
                case GameManager.Tema.aritmetica:
                    switch (signo)
                    {
                        case "+":
                            if (input.text == (num1 + num2).ToString())
                                Acierto();
                            else
                                Fallo();
                            break;
                        case "-":
                            if (input.text == (num1 - num2).ToString())
                                Acierto();
                            else
                                Fallo();
                            break;
                        case "x":
                            if (input.text == (num1 * num2).ToString())
                                Acierto();
                            else
                                Fallo();
                            break;
                        case "÷":
                            if (input.text == (num1 / num2).ToString())
                                Acierto();
                            else
                                Fallo();
                            break;
                        default: break;
                    }
                    break;
                default: break;
            }

            input.text = "";
        }
    }

    int numFallos = 0;
    public Text fallos;
    public Text fallosFinal;
    public Text textoCosa;
    void Fallo()
    {
        audioSource.clip = error;
        audioSource.volume = 0.3f;
        audioSource.Play();

        input.Select();
        input.ActivateInputField();

        CrearPregunta();

        numFallos++;
        fallos.text = numFallos.ToString();
    }

    int numAciertos = 0;
    public Text aciertos;
    public Text aciertosFinal;
    void Acierto()
    {
        audioSource.clip = success;
        audioSource.volume = 1;
        audioSource.Play();

        CrearPregunta();

        numAciertos++;
        aciertos.text = numAciertos.ToString();
    }

    public GameObject panelFinal;
    void JuegoTerminado()
    {
        panelFinal.SetActive(true);

        aciertosFinal.text = "Preguntas acertadas : " + numAciertos;
        fallosFinal.text = "Preguntas falladas : " + numFallos;
		
		//Borralo si quieres. esto es un mensaje dependiendo de tu puntuación
		if(numAciertos >= numFallos)
		{
			textoCosa.text = "Lo haz hecho bien! sigue así.";
		}

		if(numAciertos <= numFallos)
		{
			textoCosa.text = "Sigue intentando.";
		}
		
		if(numAciertos == numFallos)
		{
			textoCosa.text = "Puedes hacerlo mejor.";
		}
		
		if(numAciertos == 0 && numFallos >= numAciertos)
		{
			textoCosa.text = "Realmente estás intentando?";
		}
		
		if(numFallos == 0 && numAciertos >= numFallos)
		{
			textoCosa.text = "Perfecto!";
		}
		
		if(numFallos == 0 && numAciertos == 0)
		{
			textoCosa.text = "Estás ahí?";
		}
		
        input.interactable = false;
    }
}
