using UnityEngine;
using System.Collections;

/// <summary>
/// Interface de Item
/// </summary>
/// <remarks>HACK: Unity no maneja correctamente las interfaces, asi que se cambia a una abstract class</remarks>
public abstract class IItem : MonoBehaviour
{
    public abstract void Use();
    public abstract IItem CreateItem();
}
