using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class juego: MonoBehaviour 
{

    int num1, num2, cantidadPreguntas;
    public Text operacion;
    public InputField input;
    public AudioClip error, success;
	public bool dif1, dif2, dif3;
    AudioSource audioSource;

    private GameManager gameManager;

    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

//Este es el array de los cuatro operadores
    string[] signos = new string[4] {"+" , "-" , "x", "/"};
    string signo;
    public Text tiempo;
    public RectTransform barra;
    float posInicialBarra, posFinalBarra, diferencia;
	public bool Examen, Krypto;
    
	void Start ()
    {
		if(gameManager.modoKrypto == true)
		{
			Krypto = true;
			Examen = false;
		}
		
		if(gameManager.modoExamen == true)
		{
			Krypto = false;
			Examen = true;
		}
		
		if(gameManager.dificultadElegir1 == true)
		{
			dif1 = true;
			dif2 = false;
			dif3 = false;
		}	
		
		if(gameManager.dificultadElegir2 == true)
		{
			dif1 = false;
			dif2 = true;
			dif3 = false;
		}	
		
		if(gameManager.dificultadElegir3 == true)
		{
			dif1 = false;
			dif2 = false;
			dif3 = true;
		}
		
        audioSource = GetComponent<AudioSource>();
		if(Examen)
		{
			CrearPregunta();
		}
		
		if(Krypto)
		{
			CrearKrypto();
		}

        posInicialBarra = barra.anchoredPosition.x;
        posFinalBarra = -1150;
        diferencia = posFinalBarra - posInicialBarra;

//Se recoge el tiempo que durará la partida elegido en el menú
        tiempoPartida = gameManager.tiempoParaPartida;
		cantidadPreguntas = gameManager.preguntasDePartida;
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
                    tiempo.text = minutos.ToString() +":0" + segundos.ToString();
                else
                tiempo.text = minutos.ToString() +":" + segundos.ToString();

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

           case GameManager.Modo.puntos :
				int preguntas = (int)cantidadPreguntas;
				if(preguntas >= 1)
				{
					tiempo.text = preguntas.ToString() +" restantes." ;
				}
	
				if(preguntas == 1)
				{
					tiempo.text = "ultima pregunta";
				}
				
				if(preguntas == 0)
				{
					tiempo.text = "Ya no hay preguntas";
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
		if(dif1 == true)
		{
            num1 = Random.Range(3, 100);
            num2 = Random.Range(3, 100);
		}
		
		if(dif2 == true)
		{
            num1 = Random.Range(3, 300);
            num2 = Random.Range(3, 300);
		}		
		
		if(dif3 == true)
		{
            num1 = Random.Range(3, 600);
            num2 = Random.Range(3, 600);
		}
//Se elige el signo aleatoriamente
//Si el signo es de división, se cambia el primer número por una multiplicacion del segundo,
//para hacer que el resultado de la división sea exacto.
//Si es una multiplicación, al menos en dificultad uno el primer factor se cambia
//por un número del 1 al 10 para hacerlo mas fácil
            signo = signos[Random.Range(0, 4)];
            if (signo == "/"&& dif1 == true)
                num1 = num2 * Random.Range(3, 10);
            if (signo == "/"&& dif2 == true)
                num1 = num2 * Random.Range(3, 20);
            if (signo == "/"&& dif3 == true)
                num1 = num2 * Random.Range(3, 30);
            if (signo == "x" && dif1 == true)
                num1 = Random.Range(3, 11);
            if (signo == "x" && dif2 == true)
                num1 = Random.Range(3, 30);
			if (num1 == 0 && num2 == 0)
				CrearPregunta();
//Se cambia el texto de el indicador de la operación
            operacion.text = num1 +" " + signo +" " + num2;
            break;

            default: break;
        }
    }
	
	public int number1, number2, number3, number4;
	public string[] signoKrypto3 = new string[17] {"+ + +", "+ + -", "+ + *", "+ + /","+ - +", "+ * -", "+ - *", "+ * /","* + +", "/ + -", "- - -", "* * *","/ + /","/ * +", "/ + -", "/ * -", "+ * /"};
	public string[] signoKrypto2 = new string[12] {"+ +", "+ -", "+ *", "* +","* -", "+ *", "+ /", "/ /","/ +", "/ *", "- -", "* *"};
	public string[] signoKrypto1 = new string[4] {"+", "-", "*", "/"};
	public string signo1, signo2, signo3, signoKrypto;
	public float resultado;
	
	void CrearKrypto()
    {		
        switch (gameManager.tema)
        {
        case GameManager.Tema.aritmetica :
            input.Select();
            input.ActivateInputField();

		if(dif1 == true)
		{
            number1 = Random.Range(3, 20);
            number2 = Random.Range(3, 20);
		}	

		if(dif2 == true)
		{
            number1 = Random.Range(3, 34);
            number2 = Random.Range(3, 35);
            number3 = Random.Range(3, 40);
		}		
		
		if(dif3 == true)
		{
            number1 = Random.Range(3, 40);
            number2 = Random.Range(3, 30);
            number3 = Random.Range(3, 45);
            number4 = Random.Range(3, 35);
		}
		
			if(number1 <= 1)
			{
				number1 = Random.Range(4, 40);
			}
		
			if(number2 <= 1)
			{
				number2 = Random.Range(4, 40);
			}
		
			if(number3 <= 1)
			{
				number3 = Random.Range(4, 40);
			}
		
			if(number4 <= 1)
			{
				number4 = Random.Range(4, 40);
			}
			
			if(dif3 == true)
            signoKrypto = signoKrypto1[Random.Range(0, 17)];
			if(dif2 == true)
            signoKrypto = signoKrypto2[Random.Range(0, 12)];
			if(dif1 == true)
            signoKrypto = signoKrypto3[Random.Range(0, 4)];
			
	if(dif1 == true)
		{
			if(signoKrypto == "+")
			{
				signo1 = "+";
				resultado = number1 + number2;
			}
			
			if(signoKrypto == "-")
			{
				signo1 = "-";
				resultado = number1 - number2;
			}
			
			if(signoKrypto == "/")
			{
				signo1 = "/";
				resultado = number1 / number2;
			}
			
			if(signoKrypto == "*")
			{
				signo1 = "*";
				resultado = number1 * number2;
			}
		}		
	
		if(dif2 == true)
		{
			if(signoKrypto == "+ +")
			{
				signo1 = "+";
				signo2 = "+";
				resultado = number1 + number2 + number3;
			}
			
			if(signoKrypto == "+ -")
			{
				signo1 = "+";
				signo2 = "-";
				resultado = number1 + number2 - number3;
			}
			
			if(signoKrypto == "+ *")
			{
				signo1 = "+";
				signo2 = "*";
				resultado = number1 + number2 * number3;
			}
			
			if(signoKrypto == "* +")
			{
				signo1 = "*";
				signo2 = "+";
				resultado = number1 * number2 + number3;
			}
			
			if(signoKrypto == "+ - +")
			{
				signo1 = "+";
				signo2 = "-";
				signo3 = "+";
				resultado = number1 + number2 - number3 + number4;
			}
			
			if(signoKrypto == "* -")
			{
				signo1 = "+*";
				signo2 = "-";
				resultado = number1 * number2 - number3;
			}
			
			if(signoKrypto == "+ /")
			{
				signo1 = "+";
				signo2 = "*";
				resultado = number1 + number2 / number3;
			}
			
			if(signoKrypto == "/ /")
			{
				signo1 = "/";
				signo2 = "/";
				resultado = number1 / number2 / number3;
			}
			
			if(signoKrypto == "/ +")
			{
				signo1 = "/";
				signo2 = "+";
				resultado = number1 / number2 + number3;
			}
			
			if(signoKrypto == "/ *")
			{
				signo1 = "/";
				signo2 = "*";
				resultado = number1 / number2 * number3;
			}
			
			if(signoKrypto == "- -")
			{
				signo1 = "-";
				signo2 = "-";
				resultado = number1 - number2 - number3;
			}
			
			if(signoKrypto == "* *")
			{
				signo1 = "*";
				signo2 = "*";
				resultado = number1 * number2 * number3;
			}
		}
		
		if(dif3 == true)
		{
			if(signoKrypto == "+ + +")
			{
				signo1 = "+";
				signo2 = "+";
				signo3 = "+";
				resultado = number1 + number2 + number3 + number4;
			}
			
			if(signoKrypto == "+ + -")
			{
				signo1 = "+";
				signo2 = "+";
				signo3 = "-";
				resultado = number1 + number2 + number3 - number4;
			}
			
			if(signoKrypto == "+ + *")
			{
				signo1 = "+";
				signo2 = "+";
				signo3 = "*";
				resultado = number1 + number2 + number3 * number4;
			}
			
			if(signoKrypto == "+ + /")
			{
				signo1 = "+";
				signo2 = "+";
				signo3 = "/";
				resultado = number1 + number2 + number3 / number4;
			}
			
			if(signoKrypto == "+ - +")
			{
				signo1 = "+";
				signo2 = "-";
				signo3 = "+";
				resultado = number1 + number2 - number3 + number4;
			}
			
			if(signoKrypto == "+ * -")
			{
				signo1 = "+";
				signo2 = "*";
				signo3 = "-";
				resultado = number1 + number2 + number3 + number4;
			}
			
			if(signoKrypto == "+ - *")
			{
				signo1 = "+";
				signo2 = "-";
				signo3 = "*";
				resultado = number1 + number2 - number3 * number4;
			}
			
			if(signoKrypto == "+ * /")
			{
				signo1 = "+";
				signo2 = "*";
				signo3 = "/";
				resultado = number1 + number2 * number3 / number4;
			}
			
			if(signoKrypto == "* + +")
			{
				signo1 = "*";
				signo2 = "+";
				signo3 = "+";
				resultado = number1 * number2 + number3 + number4;
			}
			
			if(signoKrypto == "/ + -")
			{
				signo1 = "/";
				signo2 = "+";
				signo3 = "-";
				resultado = number1 / number2 + number3 - number4;
			}
			
			if(signoKrypto == "- - -")
			{
				signo1 = "-";
				signo2 = "-";
				signo3 = "-";
				resultado = number1 - number2 - number3 - number4;
			}
			
			if(signoKrypto == "* * *")
			{
				signo1 = "*";
				signo2 = "*";
				signo3 = "*";
				resultado = number1 * number2 * number3 * number4;
			}
			
			if(signoKrypto == "/ + /")
			{
				signo1 = "/";
				signo2 = "+";
				signo3 = "/";
				resultado = number1 / number2 + number3 / number4;
			}
			
			if(signoKrypto == "/ * +")
			{
				signo1 = "/";
				signo2 = "*";
				signo3 = "+";
				resultado = number1 / number2 * number3 + number4;
			}
			
			if(signoKrypto == "/ * -")
			{
				signo1 = "/";
				signo2 = "*";
				signo3 = "-";
				resultado = number1 / number2 * number3 - number4;
			}
		}
			
//Se cambia el texto de el indicador de la operación
			if(dif1 == true)
			{
            operacion.text = number1 +" ? " + number2 +" = " + resultado.ToString();
			}
			if(dif2 == true)
			{
            operacion.text = number1 +" ? " + number2 + " " + signo2 + " " + number3 +" = " + resultado.ToString();
			}
			if(dif3 == true)
			{
            operacion.text = number1 +" ? " + number2 + " " + signo2 + " " + number3 +" ? " + number4 +" = " + resultado.ToString();
			}
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
					
                    switch (signoKrypto)
                    {
                    	//Depende del signo se hace una operación u otra, y se invocan los métodos
                    	//Acierto() y Fallo() depende del inputcase "-":
						case "-":
                            if (input.text == ("-"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "*":
                            if (input.text == ("*"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "/":
                            if (input.text == ("/"))
                                Acierto();
                            else
                                Fallo();
                            break;	
						case "+ +":
                            if (input.text == ("+ +"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "+ -":
                            if (input.text == ("+ -"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "- -":
                            if (input.text == ("- -"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "+ *":
                            if (input.text == ("+ *"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "* +":
                            if (input.text == ("* +"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "* *":
                            if (input.text == ("* *"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "/ /":
                            if (input.text == ("/ /"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "/ +":
                            if (input.text == ("/ +"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "/ *":
                            if (input.text == ("/ *"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "* -":
                            if (input.text == ("* -"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "+ /":
                            if (input.text == ("+ /"))
                                Acierto();
                            else
                                Fallo();
                            break;				
						case "+":
                            if (input.text == ("+"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "+ + +":
                            if (input.text == ("+ + +"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "+ + -":
                            if (input.text == ("+ + -"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "+ + *":
                            if (input.text == ("+ + *"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "+ + /":
                            if (input.text == ("+ + /"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "+ - +":
                            if (input.text == ("+ - +"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "+ * -":
                            if (input.text == ("+ * -"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "+ - *":
                            if (input.text == ("+ - *"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "+ * /":
                            if (input.text == ("+ * /"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "* + +":
                            if (input.text == ("* + +"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "/ + -":
                            if (input.text == ("/ + -"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "- - -":
                            if (input.text == ("- - -"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "* * *":
                            if (input.text == ("* * *"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "/ + /":
                            if (input.text == ("/ + /"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "/ * +":
                            if (input.text == ("/ * +"))
                                Acierto();
                            else
                                Fallo();
                            break;
						case "/ * -":
                            if (input.text == ("/ * -"))
                                Acierto();
                            else
                                Fallo();
                            break;
                        default: break;
                    }
				
				switch(signo)
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
					
					case "*":
                        if (input.text == (num1 * num2).ToString())
							Acierto();
						else
							Fallo();
						break;
					
					case "/":
                        if (input.text == (num1 / num2).ToString())
							Acierto();
						else
							Fallo();
						break;
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
		
		if(Krypto)
		{
			CrearKrypto();
		}
		if(Examen)
		{
			CrearPregunta();
		}
		
        cantidadPreguntas--;
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

		if(Krypto)
		{
			CrearKrypto();
		}
		if(Examen)
		{
			CrearPregunta();
		}
		
		cantidadPreguntas--;
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
			textoCosa.text = "!Lo haz hecho bien! sigue así.";
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
			textoCosa.text = "¿Realmente estás intentando?";
		}
		
		if(numFallos == 0 && numAciertos >= numFallos)
		{
			textoCosa.text = "!Perfecto!";
		}
		
		if(numFallos == 0 && numAciertos == 0)
		{
			textoCosa.text = "¿Estás ahí?";
		}
		
        input.interactable = false;
    }
}