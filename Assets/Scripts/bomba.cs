using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class bomba : MonoBehaviour {

    public Text[] numeros;
    public Text tiempo;

	public void aumentarNumero(int indice)
    {
        botonSrc.Play();

        int numero = int.Parse(numeros[indice].text);
        numero++;

        if(numero == 10)
        {
            numero = 1;
        }
        numeros[indice].text = numero.ToString();
    }
    public void disminuirNumero(int indice)
    {
        botonSrc.Play();

        int numero = int.Parse(numeros[indice].text);
        numero--;

        if (numero == 0)
        {
            numero = 9;
        }
        numeros[indice].text = numero.ToString();
    }
    public int tiempoPartida;
    void Update()
    {
        if (tiempoPartida - Time.timeSinceLevelLoad <= 0.4f && !explotado)
            Explosion();

        int segundos = (int)tiempoPartida - (int)Time.timeSinceLevelLoad;
        int minutos = 0;
        while (segundos >= 60)
        {
            segundos -= 60;
            minutos++;
        }
        //Contador del tiempo restante
        if (segundos < 10)
            tiempo.text = "0" + minutos.ToString() + ":0" + segundos.ToString();
        else
            tiempo.text = "0" + minutos.ToString() + ":" + segundos.ToString();

        if (segundos < 0)
        {
            tiempo.text = "0:00";
        }
    }

    void Awake()
    {
        Screen.SetResolution(250, 512, false);
    }

    int[] nums = new int[9];
    void Start()
    {
        explosionSrc.Stop();
        StartCoroutine(CheckearNumeros());
        StartCoroutine(pitido());
    }
    public AudioSource pitidoSrc, explosionSrc, botonSrc;
    IEnumerator pitido()
    {
        while (true)
        {
            if (partidaGanada || explotado)
                break;
            pitidoSrc.Play();
            yield return new WaitForSeconds(1);
        }
    }

    bool partidaGanada;

    IEnumerator CheckearNumeros()
    {
        bool repetido = false;
        while (true)
        {
            repetido = false;
            for(int i = 0; i < 9; i++)
            {
                for(int u = 0; u < 9; u++)
                {
                    if (numeros[u].text == numeros[i].text && i != u)
                    {
                        repetido = true;
                        break;
                    }
                }
                if (repetido)
                    break;
            }
            if (!repetido)
            {
                for (int i = 0; i < 9; i++)
                {
                    nums[i] = int.Parse(numeros[i].text);
                }

                if (nums[0] + nums[8] == 15 &&
                    nums[0] + nums[1] + nums[2] == 15 &&
                    nums[3] + nums[4] + nums[5] == 15 &&
                    nums[6] + nums[7] + nums[8] == 15 &&
                    nums[2] + nums[3] == 8 &&
                    nums[5] + nums[6] == 3)
                {
                    PartidaGanada();
                }
                
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void PartidaGanada()
    {
        partidaGanada = true;
        Debug.Log("LOL MUY BIEN GANASTE XDDD");
    }
    public GameObject panelNegro;

    bool explotado;
    void Explosion()
    {
        explotado = true;

        explosionSrc.Play();
        Invoke("activarPanelNegro", 0.4f);
    }

    void activarPanelNegro()
    {
        panelNegro.SetActive(true);
    }
}
