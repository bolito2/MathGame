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

//Este es el array de los cuatro operadores
    string[] signos = new string[4] {"+" , "-" , "x", "÷"};
    string signo;
    public Text tiempo;
    public RectTransform barra;
    float posInicialBarra, posFinalBarra, diferencia;
    
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        CrearPregunta();

        posInicialBarra = barra.anchoredPosition.x;
        posFinalBarra = -1150;
        diferencia = posFinalBarra - posInicialBarra;

//Se recoge el tiempo que durará la partida elegido en el menú
        tiempoPartida = gameManager.tiempoParaPartida;
    }
	
	public int tiempoPartida;
	
    void Update()
    {
    	//El comportamiento del juego cambia dependiendo del modo
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
                //Contador del tiempo restante
                if(segundos < 10)
                    tiempo.text = minutos.ToString() + ":0" + segundos.ToString();
                else
                tiempo.text = minutos.ToString() + ":" + segundos.ToString();

                if(segundos < 0)
                {
                    tiempo.text = "0:00";
                }

//Se cambia la posición y el color de la barra
                barra.anchoredPosition = new Vector2(posFinalBarra - diferencia * (tiempoPartida - Time.timeSinceLevelLoad)/tiempoPartida , barra.anchoredPosition.y);
                barra.GetComponent<RawImage>().color = Color.Lerp(Color.red, Color.green, (tiempoPartida - Time.timeSinceLevelLoad) / tiempoPartida);

                if(Time.timeSinceLevelLoad > tiempoPartida)
                {
                	//Si se acaba el tiempo se invoca el metodo de juego terminado
                    JuegoTerminado();
                }
                break;

            default: break;
        }
    }

    void CrearPregunta()
    {
    	//Las preguntas cambian depende del tema
        switch (gameManager.tema)
        {
        case GameManager.Tema.aritmetica :
            //Se selecciona el input para que no haga falta clickearlo
            //cada vez que se haga una respuesta
            input.Select();
            input.ActivateInputField();

//Se eligen dos numeros aleatorios para las operaciones
//Aqui es donde dependiendo de la dificultad se modificarian los numeros
            num1 = Random.Range(1, 100);
            num2 = Random.Range(1, 100);

//Se elige el signo aleatoriamente
//Si el signo es de división, se cambia el primer número por una multiplicacion del segundo,
//para hacer que el resultado de la división sea exacto.
//Si es una multiplicación, al menos en dificultad uno el primer factor se cambia
//por un número del 1 al 10 para hacerlo mas fácil
            signo = signos[Random.Range(0, 4)];
            if (signo == "÷")
                num1 = num2 * Random.Range(1, 11);
            if (signo == "x")
                num1 = Random.Range(1, 11);
//Se cambia el texto de el indicador de la operación
            operacion.text = num1 + " " + signo + " " + num2;
            break;

            default: break;
        }
    }

    public void ResultadoEscrito()
    {
    	//Este método se ejecuta al pulsar Enter, si el input no esta vacío
        if (input.text != "")
        {
            switch (gameManager.tema)
            {
            	//Lo mismo, depende del modo se calcula el resultado de distinta manera
                case GameManager.Tema.aritmetica:
                    switch (signo)
                    {
                    	//Depende del signo se hace una operación u otra, y se invocan los métodos
                    	//Acierto() y Fallo() depende del input
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
    	//Se reproduce el sonido de erro y se vuelve a crear la pregunta.
    	//Además se aumenta en 1 el número de fallos y su contador.
    	
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
    	//Lo mismo que en el Fallo() pero con los aciertos.
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
    	//Al terminar el juego se activa el panel de partida finalizada con sus cosos.
    	
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
