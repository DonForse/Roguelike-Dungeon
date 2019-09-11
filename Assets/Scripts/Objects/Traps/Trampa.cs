using UnityEngine;
using System.Collections;

public class Trampa : MonoBehaviour {

    public ITrampa TrampaEspecifica;
    
    void OnTriggerEnter(Collider col)
    {
        TrampaEspecifica.Activar(col.gameObject);    
    }
}
