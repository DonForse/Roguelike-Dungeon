using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public bool follow;
    public Transform target;
    private Vector3 mypos;

    void Start() {
        mypos = this.transform.position - target.position;
    }

	// Update is called once per frame
	void Update ()
    {
        if (follow) {
            this.transform.position = target.position + mypos;
        }
        TransparentWalls();
	}

    void TransparentWalls()
    {
        RaycastHit[] hits;
        // you can also use CapsuleCastAll()
        // TODO: setup your layermask it improve performance and filter your hits.
        hits = Physics.RaycastAll(transform.position, (this.transform.rotation * Vector3.forward) + (target.transform.position - this.transform.position));
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.tag != Tags.Wall)
                continue;
            var renderers = hit.collider.GetComponentsInChildren<Renderer>();
            if (renderers == null || renderers.Length == 0)
                continue; // no renderer attached? go to next hit
            // TODO: maybe implement here a check for GOs that should not be affected like the player

            var room = hit.transform.GetComponentInParent<Room>();
            if (room != null && room.IsLit == true)
            {
                foreach (var R in renderers)
                {
                    AutoTransparent AT = R.GetComponent<AutoTransparent>();
                    if (AT == null) // if no script is attached, attach one
                    {
                        AT = R.gameObject.AddComponent<AutoTransparent>();
                    }
                    AT.BeTransparent(); // get called every frame to reset the falloff
                }
            }
        }
    }
}
