using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

    public Area Area { get; set; }
    public bool IsLit;
    public bool Explored;

    void ExploreRoom() {
        Explored = true;
        foreach (Transform child in transform)
        {
            var r = child.Find("Roof");
            r.gameObject.SetActive(false);
        }
        
    }
    void HideRoom() {
        Explored = false;
        foreach (Transform child in transform)
        {
            var r = child.Find("Roof");
            r.gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider col) 
    {
        if (col.GetComponent<Character>() != null)
        {
            IsLit = true;
            ExploreRoom();
        }
    }
    void OnTriggerExit(Collider col) 
    {
        if (col.GetComponent<Character>() != null)
            IsLit = false;
    }
}
