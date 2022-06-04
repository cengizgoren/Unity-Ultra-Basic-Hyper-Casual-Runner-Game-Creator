using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton;

    public float runSpeed = 2f;
    public float movementSpeed = .5f;
    public float movementHeight = 1.5f;
    
    private Rigidbody rb;
    
    private void Awake()
    {
        singleton = this;
        
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.singleton.isGameStart) return;
        Movement();
    }

    private void Movement()
    {
        var pos = rb.position;
        var touchInput = InputManager.singleton.TouchInput;

        pos.x += touchInput.x * Time.deltaTime * movementSpeed;
        pos.x = Mathf.Clamp(pos.x, -movementHeight, movementHeight);
        
        pos.z += Time.deltaTime * runSpeed;

        rb.position = pos;
    }
}
