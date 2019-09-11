using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour, IWeapon {

    Collider col;
    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider>();
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    void OnTriggerEnter(Collider col) 
    {
        Debug.Log(col.gameObject.name);
    }

    public void SwitchCollider() 
    {
        col.enabled = !col.enabled;
    }
}
