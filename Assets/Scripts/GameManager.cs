using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
	public int tiempoParaPartida;
	public GameObject juego; //cosa para el contrareloj
	
    void Awake()
    {	
		juego = GameObject.Find("juego"); //esto es para poner la variable del tiempo para el contrareloj
		
		if(juego != null)
		{
			juego.GetComponent<juego>().tiempoPartida = tiempoParaPartida;
		}
		
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

    public enum Modo
    {
        contrareloj,
        puntos
    }
    public Modo modo;

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
                instance.modo = Modo.contrareloj;
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
	void elegirTiempo(int tiempeishon)
	{
		tiempoParaPartida = tiempeishon;
        instance.modo = Modo.contrareloj;
        Application.LoadLevel("menuTemas");
	}
}
