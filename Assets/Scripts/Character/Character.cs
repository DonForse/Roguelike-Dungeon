using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Helper;

[RequireComponent(typeof(LifeController))]
[RequireComponent(typeof(SkillsController))]
[RequireComponent(typeof(WeaponController))]
public class Character : MonoBehaviour
{
    LifeController lifeController;
    SkillsController skillsController;
    WeaponController weaponController;

    // Use this for initialization
    void Start()
    {
        skillsController = GetComponent<SkillsController>();
        lifeController = GetComponent<LifeController>();
        lifeController.HealthDepleted += OnHealthDepleted;
        weaponController = GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(MousePositionHelper.GetWorldPositionFromMousePosition(this.transform.position.y));
        if (Input.GetButtonUp("Fire1"))
        {
            weaponController.Attack();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            skillsController.UseSkill();
        }
        //if (Input.GetButton("Fire1"))
        //{
        //    if (this.Equipo.Arma == null)
        //        return;
        //    var mousePos = MousePositionHelper.GetWorldPositionFromMousePosition(this.transform.position.y);

        //    Equipo.Arma.Atacar(mousePos);
        //}
    }

    /// <summary>
    /// Funcion a llamar cuando el personaje fue golpeado
    /// </summary>
    /// <param name="daño">Daño del impacto. [Update Vida]</param>
    /// <param name="posicionDesde">Posicion desde la que se lo ataco. [Para realizar la fisica]</param>
    /// <param name="fuerzaDeImpacto">Fuerza del impacto recibido. [Para realizar la fisica]</param>
    public void RecibirImpacto(Vector3 posicionDesde, int fuerzaDeImpacto)
    {

    }

    void OnHealthDepleted(GameObject go)
    {
        Debug.Log(go.tag);
        //gameover
        Destroy(this.gameObject);
    }
}
