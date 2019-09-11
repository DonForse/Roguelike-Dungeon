using UnityEngine;
using System.Collections;

public class ThrowKnife : MonoBehaviour
{
    public ThrowKnifeSkill ThrowKnifeSkill;
    public float Speed;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 1f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * Speed);
	}

    void OnTriggerEnter(Collider col) {
        ThrowKnifeSkill.ObjectHit(col);
    }
}
