using UnityEngine;
using System.Collections;

public class borraCarretera : MonoBehaviour {

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "carretera")
        {
            Destroy(collision.gameObject);
        }
    }
}
