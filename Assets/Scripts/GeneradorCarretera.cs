using UnityEngine;
using System.Collections;

public class GeneradorCarretera : MonoBehaviour {

    public GameObject carretera;
    public Transform instanciador;

    public modoCoches modoCoches;

    int contador;

	void OnTriggerEnter (Collider collision)
    {
	if(collision.tag == "carretera")
        {
            Instantiate(carretera, new Vector3(instanciador.transform.position.x - 3 * modoCoches.velosidad, instanciador.transform.position.y - 0.0001f * contador, instanciador.transform.position.z), instanciador.transform.rotation);
            contador++;
        }
	}
}
