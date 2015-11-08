using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    void Awake()
    {
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
}
