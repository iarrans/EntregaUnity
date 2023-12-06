using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject cameraHolder;
    public float speed, senstivity, jumpForce, groundDistance;
    private Vector2 move, look;
    private float lookRotation;
    public float maxForce;
    public float limitX, limitY;
    //Indica si pj está en el suelo
    public bool grounded;

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
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move() {

        //Target Velocity
        Vector3 currentVelocity = rb.velocity;
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

    public bool IsGrounded()
    {
        //grounded = Physics.Raycast(transform.position, Vector3.down, groundDis collissions = Physics.OverlapBox(transform.position, Vector3.down, Quaternion.identity);tance);
        //grounded = Physics.Raycast(transform.position, -Vector3.up, 0.1f);
        Collider[] collissions = Physics.OverlapBox(transform.position, new Vector3(0,1,0), Quaternion.identity);
        Debug.Log("grounded? " + grounded);
        return grounded;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }


    void LateUpdate()
    {
        Look();
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
