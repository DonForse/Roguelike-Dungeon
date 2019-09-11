using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LifeController))]
public class Monster : MonoBehaviour
{
    LifeController lc;

    // Use this for initialization
    void Start()
    {
        lc = GetComponent<LifeController>();
        lc.HealthDepleted += OnHealthDepleted;
    }

    private void OnHealthDepleted(GameObject go)
    {
        Debug.Log(go.name);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Matar()
    {
        lc.ReceiveDamage(100000);
    }
}
