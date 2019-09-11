using UnityEngine;
using System.Collections;

public class InvisibilitySkill : ISkill
{
    public float duration;   

    private CharacterController charaMov;
    private float currentDuration = 0f;
    private bool isUsingSkill = false;
    private Vector3 direction;
    public MeshRenderer mr;

    void Start() {
        charaMov = GetComponentInParent<CharacterController>();
    }

    void Update() {
        timeSinceLastUse = UpdateCoolDown(timeSinceLastUse, Cooldown);

        if (!isUsingSkill) 
            return;

        if (currentDuration > duration)
        {
            mr.material.shader = Shader.Find("Standard");
            isUsingSkill = false;
            currentDuration = 0f;
            Physics.IgnoreLayerCollision(19, 23, false);
            
            return;
        }

        charaMov.Move(direction);
        currentDuration += Time.deltaTime;

    }

    public override void Activate()
    {
        if (isUsingSkill)
            return;
        if (isOnCooldown)
            return;
        //mr.material.SetColor();
        isOnCooldown = true;
        isUsingSkill = true;
        Physics.IgnoreLayerCollision(19,23,true);
        mr.material.shader = Shader.Find("Transparent/Diffuse");
    }
}
