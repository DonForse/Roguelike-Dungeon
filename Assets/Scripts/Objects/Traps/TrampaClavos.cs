using UnityEngine;
using System.Collections;

public class TrampaClavos : ITrampa {

    public override void Activar(GameObject activadorTrampa)
    {
        Debug.Log(string.Format("{0} Activo la trampa {1}", activadorTrampa.name, this.GetType()));
    }
}
