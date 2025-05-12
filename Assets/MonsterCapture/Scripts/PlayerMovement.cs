using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    
    public Camera cam;
    Rigidbody rb;

    Vector2 input;
    bool isGrounded = false;

    float jumpSpeed = 300;
    float moveSpeed = 20;

    private float maxSpeed = 100f;

    public GameObject dooDoo;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (cam == null) { cam = Camera.main; }
        if (cam == null) { cam = FindFirstObjectByType<Camera>(); }
    }

    void OnDooDoo()
    {
        Instantiate(dooDoo, transform.position, Quaternion.identity);
    }

    void OnJump()
    {
        if (!isGrounded) return; 
        rb.AddForce(Vector3.up * jumpSpeed);
    }

    void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
        if(input.magnitude > 1)
        {
            input.Normalize();
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement3D = new Vector3(input.x, 0f, input.y);
        movement3D = cam.transform.TransformDirection(movement3D);
        movement3D.y = 0f;
        movement3D = movement3D.normalized * movement3D.magnitude;
        
        rb.AddForce(movement3D * moveSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        if (isGrounded)
        {
            Vector3 goalMovement = movement3D * moveSpeed;
            Vector3 newVelocity = Vector3.Lerp(rb.linearVelocity, goalMovement, Time.deltaTime * 5f);
            rb.linearVelocity = newVelocity;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Vector3 floorNormal = collision.contacts[0].normal.normalized;
        if(Vector3.Dot(floorNormal, Vector3.up) > 0.5)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
