using UnityEngine;
using System.Collections;
using Assets.Scripts.Helper;

public class HabilidadMegaShot : ISkill
{
    public Transform TransformPersonaje;
    
    public HabilidadMegaShot(Transform t)
    {
        TransformPersonaje = t;
    }

    public override void Activate()
    {
        Debug.Log("Activo la habilidad MegaShot.");
        var fireball = Resources.Load<GameObject>(Spells.Megashot);
        fireball.transform.position = TransformPersonaje.position;
        Instantiate<GameObject>(fireball);
        fireball.transform.localRotation = TransformPersonaje.localRotation;
        TransformPersonaje.gameObject.GetComponent<MovementController>().RecibirImpacto(fireball.transform.rotation * Vector3.forward, 50f);
    }
}
