using UnityEngine;
using System.Collections;
using Assets.Scripts.Helper;

public class ThrowKnifeSkill : ISkill
{
    public Transform charaTransform;
    public override void Activate()
    {
        var knife = Resources.Load<GameObject>("Skills/" + Spells.ThrowKnife);
        Debug.Log(this);
        knife.GetComponent<ThrowKnife>().ThrowKnifeSkill = this;
        knife.GetComponent<ThrowKnife>().Speed = 100f;
        knife.transform.position = this.transform.position;
        knife.transform.localRotation = this.transform.rotation;
        Instantiate<GameObject>(knife);
        
    }

    public void ObjectHit(Collider col) 
    {
        if (col.gameObject.transform == this.charaTransform)
            return;
        Debug.Log("hit:" + col.name);
    }
}
