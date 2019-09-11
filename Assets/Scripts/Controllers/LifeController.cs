using UnityEngine;
using System.Collections;

public class LifeController : MonoBehaviour
{
    public delegate void HealthDepletedHandler(GameObject go);
    public event HealthDepletedHandler HealthDepleted;

    public int TotalHealth;
    private int CurrentHealth;

    void Start() {
        CurrentHealth = TotalHealth;
    }

    public void ReceiveDamage(int health) 
    {
        this.CurrentHealth -= health;
        if (this.CurrentHealth <= 0)
        {
            this.CurrentHealth = 0;
            HealthDepleted(this.gameObject);
        }
    }
}

