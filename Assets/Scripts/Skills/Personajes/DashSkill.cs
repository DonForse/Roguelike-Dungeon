using UnityEngine;
using System.Collections;

public class DashSkill : ISkill
{
    public float distance;   

    private CharacterController charaMov;
    private MovementController charaController;
    private TrailRenderer tr;
    private float currentDistance = 0f;
    private bool isDashing = false;
    private Vector3 direction;

    void Start() {
        charaMov = GetComponentInParent<CharacterController>();
        charaController = GetComponentInParent<MovementController>();
        tr = GetComponentInParent<TrailRenderer>();
        tr.enabled = false;
    }

    void Update() {
        timeSinceLastUse = UpdateCoolDown(timeSinceLastUse, Cooldown);

        if (!isDashing) 
            return;

        if (currentDistance > distance)
        {
            isDashing = false;
            currentDistance = 0f;
            Physics.IgnoreLayerCollision(19, 23, false);
            tr.enabled = false;
            charaController.enabled = true;
            return;
        }

        charaMov.Move(direction);
        currentDistance += 1f;

    }

    public override void Activate()
    {
        if (isDashing)
            return;
        if (isOnCooldown)
            return;
        isOnCooldown = true;
        direction = transform.rotation * Vector3.forward;
        isDashing = true;
        tr.enabled = true;
        charaController.enabled = false;
        Physics.IgnoreLayerCollision(19,23,true);
        
    }
}
