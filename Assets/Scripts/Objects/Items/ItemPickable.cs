using UnityEngine;
using System.Collections;

public class ItemPickable : MonoBehaviour
{
    public IItem Item;

    void OnTriggerEnter(Collider col) 
    {
        ////TODO: ver que pasa con la propiedad trigger si no lo triggerea un personaje.
        //if (col.gameObject.name == "Personaje")
        //{
        //    var inventario = col.gameObject.GetComponent<Inventario>();
        //    var item = Item.CrearItem();
        //    inventario.Items.Add(item);
        //    Debug.Log(string.Format("Se agrego un item al inventario - {0}", item.GetType().Name));
        //    Destroy(this.gameObject);
        //}
    }
}
