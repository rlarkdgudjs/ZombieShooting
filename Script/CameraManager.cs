using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using static Unity.Cinemachine.CinemachineOrbitalFollow;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    //public Transform target; // 플레이어
    //public float distance = 5f;
    //public float sensitivity = 3f;
    //public float yMin = -40f;
    //public float yMax = 80f;

    //public float yaw = 0f;
    //public float pitch = 20f;

    //[SerializeField] private float positionSmoothTime = 0.1f;  // 위치 보간 속도
    //[SerializeField] private float rotationSmoothTime = 0.1f;  // 회전 보간 속도
    public static CameraManager instance;

    private Vector3 currentVelocity = Vector3.zero;
    private Quaternion currentRotation;
    public CinemachineCamera aimcamera;
    public CinemachineCamera normalcamera;
    public CinemachineBrain braincamera;
    private bool isAiming = false;
    public Transform player;

    [SerializeField] CinemachineInputAxisController inputController;

    private WaitForSeconds waitTime = new WaitForSeconds(0.25f);
    void Awake()
    { 
       instance = this;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스를 화면 중앙에 고정시키고 숨김
        Cursor.visible = false;
    }
    private void Update()
    {

        if (isAiming)
        {
            player.rotation = Quaternion.LookRotation(normalcamera.transform.forward);
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (isAiming)
            {
                Vector3 vector3 = normalcamera.transform.forward;
                vector3.y = 0;
                player.rotation = Quaternion.LookRotation(vector3);
                
                normalcamera.GetComponent<CinemachineOrbitalFollow>().Radius = 4f;
                normalcamera.Priority = 1;
                aimcamera.Priority = 0;
                isAiming = false;
            }
            else
            {
                normalcamera.Priority = 0;
                aimcamera.Priority = 1;
                StartCoroutine(WaitforSecond());
                isAiming = true;
            }
        }


    }
    IEnumerator WaitforSecond()
    {
        yield return waitTime;
        normalcamera.GetComponent<CinemachineOrbitalFollow>().Radius = 10f;
        
        //Cursor.lockState = CursorLockMode.Locked; // 마우스를 화면 중앙에 고정시키고 숨김
        //Cursor.visible = false;
    }

    public void SetUIOpen(bool open)
    {
        inputController.enabled = !open;
    }
    //void LateUpdate()
    //{
    //    Vector2 mouseDelta = Mouse.current.delta.ReadValue();
    //    yaw += mouseDelta.x * sensitivity * Time.deltaTime;
    //    yaw = yaw % 360f;
    //    if (yaw < 0) yaw += 360f;
    //    pitch -= mouseDelta.y * sensitivity * Time.deltaTime;
    //    pitch = Mathf.Clamp(pitch, yMin, yMax);

    //    Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
    //    Vector3 offset = rot * new Vector3(0, 0, -distance);

    //    //transform.position = target.position + offset + Vector3.up * 2f;
    //    transform.LookAt(target.position + Vector3.up * 1.5f);

    //    //Vector3 targetPosition = target.position + offset + Vector3.up * 2f;
    //    Vector3 lookAtPosition = target.position + Vector3.up * 1.5f;

    //    //// 위치 보간 (부드럽게 이동)
    //    //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, positionSmoothTime);

    //    //// 회전 보간 (부드럽게 회전)
    //    //Quaternion desiredRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
    //    //currentRotation = Quaternion.Slerp(currentRotation, desiredRotation, Time.deltaTime / rotationSmoothTime);
    //    //transform.rotation = currentRotation;
    //}

    //private void LateUpdate()
    //{
    //    if (player == null) return;

    //    // 플레이어 뒤쪽 위치 계산 (플레이어 forward 반대 방향 * 거리 + 높이)
    //    Vector3 desiredPosition = player.position - player.forward * distance + Vector3.up * height;

    //    // 카메라 위치 부드럽게 이동
    //    //transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    //    transform.position = desiredPosition;

    //    // 플레이어를 바라보도록 회전 (등 뒤를 보도록)
    //    //Quaternion desiredRotation = Quaternion.LookRotation(player.position - transform.position);
    //    //transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    //    Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
    //    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    //}
}
