using UnityEngine;
using System.Collections;

public class BarrilExplosivo : MonoBehaviour {

    public int HP;
    public float RadioExplosion;
    public float FuerzaExplosion;
    public float FuerzaLevantamiento;

    public float DañoExplosion;

    public void Explotar()
    {
        //var raycastHits = Physics.SphereCastAll(this.transform.position,RadioExplosion,this.transform.position);
        var raycastHits = Physics.SphereCastAll(this.transform.position, RadioExplosion, Vector3.forward);
        foreach (var raycast in raycastHits) 
        {
            Debug.Log(raycast.transform.name);
            if (raycast.transform == this.transform)
                continue;

            if (raycast.transform.gameObject != null) 
            {
                var controladorHp = raycast.transform.gameObject.GetComponent<LifeController>();

                if (controladorHp != null)
                    controladorHp.ReceiveDamage((int)DañoExplosion);
            }
            if (raycast.rigidbody != null)
                raycast.rigidbody.AddExplosionForce(FuerzaExplosion, this.transform.position, RadioExplosion, FuerzaLevantamiento, ForceMode.Impulse);
        }
        //Destroy(this.gameObject);
    }
}
