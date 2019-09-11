using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    private CharacterController charController;

    public float Mass;
    public float MovementSpeed;

    public float JumpSpeed;
    public float Gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    Vector3 impact = Vector3.zero;

	// Use this for initialization
	void Start () {
        charController = this.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (impact.magnitude > 0.2F) 
            charController.Move(impact * Time.deltaTime);
           // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5*Time.deltaTime);

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        if (charController.isGrounded)
        {
            moveDirection = Vector3.right * h + Vector3.forward * v ;

            //moveDirection = transform.TransformDirection(moveDirection);

            moveDirection *= MovementSpeed;

            if (Input.GetButton("Jump"))
                moveDirection.y = JumpSpeed;
        }

        moveDirection.y -= Gravity * Time.deltaTime;
        charController.Move(moveDirection * Time.deltaTime);
	}

    /// <summary>
    /// Manually moves the character on impact. 
    /// Because CharacterControlled is not affected by rigidbody properties.
    /// </summary>
    public void RecibirImpacto(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground

        impact += dir.normalized*-1 * force / Mass;
    }
}
