using UnityEngine;
using UnityEngine.InputSystem;

public class InputInvokeUnityEvents : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    private Vector2 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = speed * direction.x;
        rb.linearVelocityY = speed * direction.y;
    }

    public void MoveExample(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }
    public void AttackExample(InputAction.CallbackContext context)
    {
        if (context.started) Debug.Log("Attack Started");
        if (context.performed) Debug.Log("Attack performed");
        if (context.canceled) Debug.Log("Attack cancelled");
    }
}
