using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 5f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * 4);
	}
}
