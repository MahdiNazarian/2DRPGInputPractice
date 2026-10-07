using UnityEngine;
using UnityEngine.InputSystem;

public class InvokeCSharpEvents : MonoBehaviour
{
    private PlayerInput playerInputs;
    private InputActionMap uIMap;
    private InputActionMap swithMenuMap;
    private InputActionMap playerBasicMap;
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction exampleAction;
    private InputAction switchMapAction;

    InputActionMap currentActionMapPlaceHolder;

    private Rigidbody2D rb;
    public float speed;
    private Vector2 direction;

    private Animator animator;

    [SerializeField] private GameObject uICanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        playerInputs = GetComponent<PlayerInput>();
        playerBasicMap = playerInputs.actions.FindActionMap("PlayerBasic");
        uIMap = playerInputs.actions.FindActionMap("UI");
        swithMenuMap = playerInputs.actions.FindActionMap("MapSwitcher");
        moveAction = playerBasicMap.FindAction("Move");
        attackAction = playerBasicMap.FindAction("Attack");
        exampleAction = playerBasicMap.FindAction("Example");
        switchMapAction = swithMenuMap.FindAction("Switch");
    }

    private void OnEnable()
    {
        attackAction.performed += AttackExample;
        attackAction.canceled += AttackCancelExample;
        exampleAction.performed += ExampleActionPerformed;
        exampleAction.canceled += ExampleActionCancelled;
        switchMapAction.performed += SwitchMenuAciton;
        swithMenuMap.Enable();
        playerBasicMap.Enable();
    }

    private void OnDisable()
    {
        playerBasicMap.Disable();
        swithMenuMap.Disable();
        switchMapAction.performed -= SwitchMenuAciton;
        attackAction.performed -= AttackExample;
        attackAction.canceled -= AttackCancelExample;
        exampleAction.performed -= ExampleActionPerformed;
        exampleAction.canceled -= ExampleActionCancelled;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("speedY", -1f);
    }

    // Update is called once per frame
    void Update()
    {
        direction = moveAction.ReadValue<Vector2>();
        if (direction != Vector2.zero)
        {
            animator.SetFloat("speedX", direction.x);
            animator.SetFloat("speedY", direction.y);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = speed * direction.x;
        rb.linearVelocityY = speed * direction.y;
    }

    private void AttackExample(InputAction.CallbackContext context)
    {
        Debug.Log("Attack Performed");
    }

    private void AttackCancelExample(InputAction.CallbackContext context)
    {
        Debug.Log("Attack Canecelled");
    }
    private void ExampleActionPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Example Performed");
    }

    private void ExampleActionCancelled(InputAction.CallbackContext context)
    {
        Debug.Log("Example cancelled");
    }

    private void SwitchMenuAciton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (currentActionMapPlaceHolder == null)
            {
                currentActionMapPlaceHolder = playerInputs.currentActionMap;
            }
            if (uICanvas.activeSelf)
            {
                uICanvas.SetActive(false);
                playerInputs.SwitchCurrentActionMap(currentActionMapPlaceHolder.name);
            }
            else
            {
                uICanvas.SetActive(true);
                playerInputs.SwitchCurrentActionMap(uIMap.name);
            }
        }

    }
}
