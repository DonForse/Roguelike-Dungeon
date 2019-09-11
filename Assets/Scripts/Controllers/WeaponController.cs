using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    public IWeapon EquippedWeapon;
    void Start()
    {
        EquippedWeapon = GetComponentInChildren<IWeapon>();
    }

    public void Attack() { 
        if (EquippedWeapon == null)
            throw new System.NotImplementedException();

        EquippedWeapon.Attack();
        //var mGo = GameObject.Find("Monstruo");
        //if (mGo != null)
        //{
        //    var m = mGo.GetComponent<Monster>();
        //    m.Matar();
        //}
    }

    public void SwitchWeapon() { 
    
    }
}
