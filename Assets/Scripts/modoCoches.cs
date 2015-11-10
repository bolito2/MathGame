using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/*
public struct Ecuacion
{
    public int valor;
    public int estado;
    //Estados: 0 - suma arriba, 1 - resta arriba ,2 - multiplicacion arriba, 3- suma abajo, 4- resta abajo, 5- multiplicacion abajo.

    public Ecuacion(int valor, int estado)
    {
        this.valor = valor;
        this.estado = estado;
    }

}
*/
public class modoCoches : MonoBehaviour {

    public Transform coche;

    int preguntasRespondidas;
    [Range(0.05f, 0.5f)]
    public float velosidad;
    public float velocidadGiro;
    bool girando;

    public Text ecuacion;

    public GameManager gameManager;

    int posicion = 1;

    int solucion;

    void Start ()
    {
        ecuacion.text = "";
        CrearPregunta();
	}

    void Update()
    {
        if(!girando)
        coche.transform.position = new Vector3(coche.transform.position.x + velosidad ,coche.transform.position.y, coche.transform.position.z);
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + velosidad, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }
    /*
    void CrearPregunta()
    {
        arriba.text = "x ";
        Ecuacion[] ecuacion = new Ecuacion[gameManager.numElementosEcuacion];
        //Estados: 0 - suma arriba, 1 - resta arriba ,2 - multiplicacion arriba, 3- suma abajo, 4- resta abajo, 5- multiplicacion abajo.
        int random = 0;
        int contadorAbajo = 0;
        for (int i = 0; i < gameManager.numElementosEcuacion; i++)
        {
            random = Random.Range(0, 10);
            
            if (contadorAbajo == 2)
            {
                random = Random.Range(5, 10);

                if (random == 5 || random == 6)
                {
                    ecuacion[i] = new Ecuacion(Random.Range(1, 21), 0);
                    arriba.text += "+" + ecuacion[i].valor.ToString() + " ";
                }
                if (random == 7 || random == 8)
                {
                    ecuacion[i] = new Ecuacion(Random.Range(1, 21), 1);
                    arriba.text += "-" + ecuacion[i].valor.ToString() + " ";
                }
                if (random == 9 && i != 0)
                {
                    ecuacion[i] = new Ecuacion(Random.Range(2, 7), 2);
                    arriba.text += "x" + ecuacion[i].valor.ToString() + " ";
                }
            }
            else
            {

                if (random < 2)
                {
                    ecuacion[i] = new Ecuacion(Random.Range(1, 21), 3);
                    abajo.text += "+" + ecuacion[i].valor.ToString() + " ";
                    contadorAbajo++;
                }
                if (random == 2 || random == 3)
                {
                    ecuacion[i] = new Ecuacion(Random.Range(1, 21), 4);
                    abajo.text += "-" + ecuacion[i].valor.ToString() + " ";
                    contadorAbajo++;
                }
                if (random == 4 && i != 0)
                {
                    ecuacion[i] = new Ecuacion(Random.Range(2, 7), 5);
                    abajo.text += "x" + ecuacion[i].valor.ToString() + " ";
                    contadorAbajo++;
                }

                if (random == 5 || random == 6)
                {
                    ecuacion[i] = new Ecuacion(Random.Range(1, 21), 0);
                    arriba.text += "+" + ecuacion[i].valor.ToString() + " ";
                }
                if (random == 7 || random == 8)
                {
                    ecuacion[i] = new Ecuacion(Random.Range(1, 21), 1);
                    arriba.text += "-" + ecuacion[i].valor.ToString() + " ";
                }
                if (random == 9 && i != 0)
                {
                    ecuacion[i] = new Ecuacion(Random.Range(2, 7), 2);
                    arriba.text += "x" + ecuacion[i].valor.ToString() + " ";
                }
            }
            if(ecuacion[i].estado == 2 && ecuacion[i - 1].estado >= 3)
            {
                ecuacion[i].estado = Random.Range(0, 2);
            }
            if (ecuacion[i].estado == 5 && ecuacion[i - 1].estado < 3)
            {
                ecuacion[i].estado = Random.Range(3, 5);
            }
            Debug.Log("i = " + i);
            Debug.Log("Estado : " + ecuacion[i].estado + ". Valor : " + ecuacion[i].valor);
        }

        int valorArriba = 0;
        for (int i = 0; i < gameManager.numElementosEcuacion; i++)
        { 

            if (ecuacion[i].estado == 0)
            {
                valorArriba += ecuacion[i].valor;
            }
            if (ecuacion[i].estado == 1)
            {
                valorArriba -= ecuacion[i].valor;
            }
            if (ecuacion[i].estado == 2)
            {
                valorArriba -= ecuacion[i - 1].valor;
                valorArriba += ecuacion[i].valor * ecuacion[i - 1].valor;
            }
        }

        int valorAbajo = 0;
        for (int i = 0; i < gameManager.numElementosEcuacion; i++)
        {
            

            if (ecuacion[i].estado == 3)
            {
                valorAbajo += ecuacion[i].valor;
            }
            if (ecuacion[i].estado == 4)
            {
                valorAbajo -= ecuacion[i].valor;
            }
            if (ecuacion[i].estado == 5)
            {
                valorAbajo -= ecuacion[i - 1].valor;
                valorAbajo += ecuacion[i].valor * ecuacion[i - 1].valor;
            }
        }

        if(valorAbajo == 0)
        {
            valorAbajo++;
            abajo.text += "1";
        }

        //if(valorArriba % valorAbajo != 0)
        {
            arriba.text += "-" + valorArriba % valorAbajo;
            valorArriba -= (valorArriba % valorAbajo);
        }

        solucion = valorArriba / valorAbajo;
        Debug.Log("Modulo : " + valorArriba % valorAbajo);
        Debug.Log("Numerador :" + valorArriba);
        Debug.Log("Denominador :" + valorAbajo);

        Debug.Log("Solucion fracción :" +solucion);

        int multiplo = Random.Range(0, 10);
        Debug.Log("Multiplo : " + multiplo);
        int igual = (solucion * multiplo);
        miembro2.text = "= " + igual.ToString();

        solucion = igual / solucion;
        Debug.Log("Solucion final :" + solucion);
    }
    */
    int[] numeros; 
    void CrearPregunta()
    {
        ecuacion.text = "x";
        numeros = new int[gameManager.numElementosEcuacion];

        int suma = 0;
        for(int i = 0; i < gameManager.numElementosEcuacion; i++)
        {           
            numeros[i] = Random.Range(-99, 100);
            suma += numeros[i];
                if(numeros[i] > 0)
                {
                    ecuacion.text += " +" + numeros[i];
                }
                else if(numeros[i] < 0)
                {
                    ecuacion.text += numeros[i];
                }
            
        }

        int miembro = Random.Range(0, 10) + suma;
        ecuacion.text += " = " + miembro;

        solucion = miembro - suma;
    }
    
    Vector3 posicionCambio;
	public void Respuesta(int respuesta)
    {
        ecuacion.text = "";
        if (respuesta == solucion)
        {
            posicionCambio = new Vector3(coche.transform.position.x, coche.transform.position.y, coche.transform.position.z + 0.85f * posicion);
            StartCoroutine(moverCoche());
            Invoke("CrearPregunta", 2f);
        }
    }

    IEnumerator moverCoche()
    {
        posicion *= -1;
        girando = true;
        float t = 0;
        Vector3 initPos = coche.transform.position;
        while (Mathf.Abs(coche.transform.position.z - posicionCambio.z) > 0.01f)
        {
            coche.transform.position = new Vector3(coche.transform.position.x + velosidad, coche.transform.position.y, Vector3.Lerp(initPos, posicionCambio, t).z);
            t+=Time.deltaTime * velocidadGiro;
            yield return null;
        }
        girando = false;
        coche.transform.position = new Vector3(coche.transform.position.x + velosidad, coche.transform.position.y, coche.transform.position.z);
    }
}
