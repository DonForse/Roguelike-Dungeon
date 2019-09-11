using UnityEngine;
using System.Collections;

/// <summary>
/// Interface de Habilidad
/// </summary>
/// <remarks>HACK: Unity no maneja correctamente las interfaces, asi que se cambia a una abstract class</remarks>
public abstract class ISkill : MonoBehaviour
{
    public float Cooldown;
    internal bool isOnCooldown = false;
    internal float timeSinceLastUse;
    public abstract void Activate();

    public virtual float UpdateCoolDown(float time, float cd)
    {
        if (isOnCooldown)
        {
            time += Time.deltaTime;
            isOnCooldown = cd > time;
            return time;
        }
        time = 0;
        return time;
    }
}
