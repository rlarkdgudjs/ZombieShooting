using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction tabAction;
    private InputAction AttackAction;
    private InputAction AimAction;
    private Vector3 moveAmount;
    private Vector3 moveDir = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    public float rotationSpeed;
    public float moveSpeed;
    public float jumpForce = 5f;
    public float smoothTime = 0.1f;

    public RuntimeAnimatorController animatorController;
    private bool isSprinting = false;

    public GameObject Inventory;
    public Canvas canvas;
    [SerializeField] private float sprintMultiplier = 1.4f;

    [SerializeField] private Rigidbody rigidbody;

    [SerializeField] private Animator animator;

    public GameObject Crosshair;
    private bool isAiming = false;
    public bool isUIOpen { get; private set; } = false;

    public GameObject enemyprefab;

    public IWeapon weapon;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        if (moveAction != null)
        {
            moveAction.Enable();
            moveAction.performed += ctx => animator.SetBool("Walk", true);
            moveAction.canceled += ctx => animator.SetBool("Walk", false);
        }

        jumpAction = InputSystem.actions.FindAction("Jump");
        if (jumpAction != null)
        {
            jumpAction.Enable();
            jumpAction.performed += OnJumpPerform;
        }
        sprintAction = InputSystem.actions.FindAction("Sprint");

        if (sprintAction != null)
        {
            sprintAction.Enable();
            sprintAction.performed += ctx =>
            {
                isSprinting = true;
                animator.SetBool("Run", true);
            };
            sprintAction.canceled += ctx =>
            {
                isSprinting = false;
                animator.SetBool("Run", false);
            };
        }

        tabAction = InputSystem.actions.FindAction("Tab");
        if (tabAction != null)
        {
            tabAction.Enable();
            tabAction.performed += OnTabPerform;
        }
        AttackAction = InputSystem.actions.FindAction("Attack");
        if (AttackAction != null)
        {
            AttackAction.Enable();
            AttackAction.performed += OnAttackPerform;
        }

        AimAction = InputSystem.actions.FindAction("Aim");
        if (AimAction != null)
        {
            AimAction.Enable();
            AimAction.performed += OnAimPerform;

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < 100; i++)
            {
                GetRandomSpawnPosition();
            }

        }
        if (moveAction != null)
        {
            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            moveDir = GetCameraRelativeMovement(moveInput, Camera.main.transform);

            float currentSpeed = moveSpeed * (isSprinting ? sprintMultiplier : 1f);
            moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * currentSpeed, ref velocity, smoothTime);
            transform.position += moveAmount * Time.deltaTime;
            if (moveAmount != Vector3.zero)
            {


                Quaternion targetRotation = Quaternion.LookRotation(moveAmount);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    // 테스트용 좀비 생성
    void GetRandomSpawnPosition()
    {
        int mapSize = 30;
        int halfSize = mapSize / 2;

        float x = Random.Range(-halfSize, halfSize + 1);
        float z = Random.Range(-halfSize, halfSize + 1);
        float y = 3f;


        Vector3 spawnPosition = new Vector3(x, y, z) + this.transform.position;
        Instantiate(enemyprefab, spawnPosition, Quaternion.identity);
    }
    Vector3 GetCameraRelativeMovement(Vector2 input, Transform cameraTransform)
    {
        // 카메라의 forward, right 벡터 얻기 (y축 방향은 0으로 평면화)
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        forward.Normalize(); // 정규화

        Vector3 right = cameraTransform.right;
        right.y = 0;
        right.Normalize(); // 정규화

        // 입력을 카메라 방향 기준으로 변환
        Vector3 moveDirection = forward * input.y + right * input.x;
        return moveDirection.normalized;
    }

    void OnJumpPerform(InputAction.CallbackContext context)
    {
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void OnTabPerform(InputAction.CallbackContext context)
    {
        if (canvas.enabled)
        {
            canvas.enabled = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isUIOpen = false;
        }
        else
        {
            canvas.enabled = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            isUIOpen = true;
        }

        CameraManager.instance.SetUIOpen(isUIOpen);
    }

    void OnAttackPerform(InputAction.CallbackContext context)
    {
        if (isUIOpen) return;

        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();
        transform.rotation = Quaternion.LookRotation(forward);
        animator.SetTrigger("Attack"); // 현재 animator에 "Attack" 트리거는 근접 모션만 존재

    }

    void OnAimPerform(InputAction.CallbackContext context)
    {
        if (isAiming)
        {
            animator.SetBool("Aim", false);
            Crosshair.SetActive(false);
            isAiming = false;
            animator.ResetTrigger("Attack");
        }
        else
        {
            animator.SetBool("Aim", true);
            Crosshair.SetActive(true);
            isAiming = true;
        }
    }
    public void SetWeapon(GameObject obj)
    {
        weapon = obj.GetComponent<IWeapon>();
    }
    void PerfromAttack()
    {
        Debug.Log("PerformAttack");
        weapon?.PerformAttack();
    }
}
    
