using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject cameraHolder;
    public float speed, senstivity, jumpForce, groundDistance;
    private Vector2 move, look;
    private float lookRotation;
    public float maxForce;
    public float limitX, limitY;
    //Indica si pj está en el suelo
    public bool grounded;

    public bool IsSprinting;


    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (GameController.instance.isPlaying) Jump();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (GameController.instance.isPlaying) Sprint();
    }

    private void Sprint()
    {
        if (!IsSprinting)
        {
            StartCoroutine(SprintCoroutine());
        }
    }

    private IEnumerator SprintCoroutine()
    {
        IsSprinting = true;
        speed = speed * 2.5f;
        //Tiempo máximo de duración del sprint
        float sprintTime = 2;
        while (sprintTime > 0)
        {
            UIManager.instance.SprintSlider.value = sprintTime;
            sprintTime -= 0.05f;
            yield return new WaitForSeconds(0.05f);       
        }
        speed /= 2.5f;
        //Tiempo de delay para poder hacer el siguiente sprint
        sprintTime = 0;
        while (sprintTime < 2)
        {
            UIManager.instance.SprintSlider.value = sprintTime;
            sprintTime += 0.02f;
            yield return new WaitForSeconds(0.1f);
        }
        IsSprinting = false;
    }

    private void FixedUpdate()
    {
        if(GameController.instance.isPlaying) Move();
    }

    void Move() {

        //Target Velocity
        Vector3 currentVelocity = rb.velocity;

        if (currentVelocity.x > 0 || currentVelocity.z > 0 || currentVelocity.y > 0)
        {
            transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed",1);
        }
        else
        {
            transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed", 0);
        }

        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);
        targetVelocity *= speed;

        //Dieccion
        targetVelocity = transform.TransformDirection(targetVelocity);

        //Calculate Forces
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x,0, velocityChange.z);

        //Limit force
        Vector3.ClampMagnitude(velocityChange, maxForce);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    void Look()
    {
        //Turn (LooK x = Mouse X)
        transform.Rotate(Vector3.up * look.x * senstivity);

        //Look
        lookRotation += (-look.y * senstivity);
        lookRotation = Mathf.Clamp(lookRotation, -limitX, limitY);
        cameraHolder.transform.eulerAngles = new Vector3(lookRotation, cameraHolder.transform.eulerAngles.y, cameraHolder.transform.eulerAngles.z);
    }

    void Jump()
    {
        Vector3 jumpForces = Vector3.zero;
        if (grounded)
        {
            jumpForces = Vector3.up * jumpForce;
        }

        rb.AddForce(jumpForces, ForceMode.VelocityChange);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        IsSprinting = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }


    void LateUpdate()
    {
        if (GameController.instance.isPlaying) Look();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            grounded = true;
        }
    }

    void OnCollisionExit(Collision collider)
    {
        if (collider.gameObject.CompareTag("Floor"))
        {
            grounded = false;
        }
    }


}
