using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //La instancia del GameManager, refiriendose al objeto que hay en el inspector
    //y que contiene todos los datos de la partida.
    public static GameManager instance = null;

//El tiempo que dura la partida. Ahora el valor lo recoge el script de jueg directamente,
//en vez de cambiar el valor desde aquí.
	public int tiempoParaPartida;
	public int preguntasDePartida;
	
	public bool dificultadElegir1, dificultadElegir2, dificultadElegir3, modoKrypto, modoExamen;
	
    void Awake()
    {	
    	//Aquí es para hacer que el objeto no se destruya al cambiar de escena.
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }

//Los modos a elegir
    public enum Modo
    {
        contrareloj,
        puntos
    }
    public Modo modo;

//Los temas a elegir
    public enum Tema
    {
        aritmetica,
        ano
    }
    public Tema tema;

    public void elegirModo(string stringModo)
    {
        switch (stringModo)
        {
            case "contrareloj":
                //Recuerda utilizar el instance para todo lo que quieras que perpetúe
                instance.modo = Modo.contrareloj;
				instance.modoKrypto = false;
				instance.modoExamen = true;
                break;

            default:
                Debug.LogError("Modo mal escrito");
                return;
        }

        Application.LoadLevel("menuTemas");
    }
	
	//estas son las cosas del menú principal
    public void irAMenu()
    {
        Application.LoadLevel("menuPrincipal");
    }
	
	public void irAJugar()
    {
        Application.LoadLevel("menuModos");
    }
	
	public void irACreditos()
    {
        Application.LoadLevel("menuCreditos");
    }
	
	public void Salir()
    {
        Application.Quit();
    }
	//Acá se acaban las cosas del menú principal
	
    public void RepetirJuego()
    {
        Debug.Log("Reiniciando");
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void elegirTema(string stringTema)
    {
        switch (stringTema)
        {
            case "aritmetica":
                instance.tema = Tema.aritmetica;
                break;

            default:
                Debug.LogError("Tema mal escrito");
                return;
        }

        Application.LoadLevel("juego");
    }
	
	//esto es lo del menú de contrareloj
	public void elegirTiempo(int tiempeishon)
	{
		instance.tiempoParaPartida = tiempeishon;
        instance.modo = Modo.contrareloj;
		instance.modoKrypto = false;
		instance.modoExamen = true;
        Application.LoadLevel("menuTemas");
	}
	
	public void elegirPreguntas(int cantidadPreguntas)
	{
		instance.preguntasDePartida = cantidadPreguntas;
        instance.modo = Modo.puntos;
		instance.modoKrypto = true;
		instance.modoExamen = false;
        Application.LoadLevel("menuTemas");
	}
	
	public void dificultad1()
	{
		instance.tema = Tema.aritmetica;
		instance.dificultadElegir1 = true;
        Application.LoadLevel("juego");
	}
	
	public void dificultad2()
	{
		instance.tema = Tema.aritmetica;
		instance.dificultadElegir2 = true;
        Application.LoadLevel("juego");
	}
	
	public void dificultad3()
	{
		instance.tema = Tema.aritmetica;
		instance.dificultadElegir3 = true;
        Application.LoadLevel("juego");
	}
	
	public void modoCrono()
	{
        Application.LoadLevel("contrareloj");
	}
	
	public void modoPuntos()
	{
        Application.LoadLevel("puntos");
	}
}