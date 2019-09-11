using UnityEngine;
using System.Collections;
using Assets.Scripts.Helper;

public class HabilidadShockwave : ISkill {

    //public float Cooldown = 5f;
    public Transform Posicion;
    public HabilidadShockwave(Transform t)
    {
        Posicion = t;
    }
    public override void Activate()
    {
        Debug.Log("Activo la habilidad Shockwave.");
        var shockwave = Resources.Load<GameObject>(Spells.Shockwave);
        shockwave.transform.position = Posicion.position;
        Instantiate<GameObject>(shockwave);
    }
}
