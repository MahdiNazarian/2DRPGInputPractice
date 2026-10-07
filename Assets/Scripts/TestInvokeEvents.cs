using UnityEngine;
using UnityEngine.InputSystem;

public class TestInvokeEvents : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExampleAction(InputAction.CallbackContext context)
    {
        if (context.started) Debug.Log("Example Started");
        if (context.performed) Debug.Log("Example Performed");
        if (context.canceled) Debug.Log("Example Cancelled");
    }
}
