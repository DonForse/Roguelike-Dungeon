using UnityEngine;
using System.Collections;

/// <summary>
/// Interface de Trampa
/// </summary>
/// <remarks>HACK: Unity no maneja correctamente las interfaces, asi que se cambia a una abstract class</remarks>
public abstract class ITrampa : MonoBehaviour
{
    public abstract void Activar(GameObject activadorTrampa);
}
