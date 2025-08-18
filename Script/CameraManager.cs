using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using static Unity.Cinemachine.CinemachineOrbitalFollow;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    //public Transform target; // �÷��̾�
    //public float distance = 5f;
    //public float sensitivity = 3f;
    //public float yMin = -40f;
    //public float yMax = 80f;

    //public float yaw = 0f;
    //public float pitch = 20f;

    //[SerializeField] private float positionSmoothTime = 0.1f;  // ��ġ ���� �ӵ�
    //[SerializeField] private float rotationSmoothTime = 0.1f;  // ȸ�� ���� �ӵ�
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
        Cursor.lockState = CursorLockMode.Locked; // ���콺�� ȭ�� �߾ӿ� ������Ű�� ����
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
        
        //Cursor.lockState = CursorLockMode.Locked; // ���콺�� ȭ�� �߾ӿ� ������Ű�� ����
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

    //    //// ��ġ ���� (�ε巴�� �̵�)
    //    //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, positionSmoothTime);

    //    //// ȸ�� ���� (�ε巴�� ȸ��)
    //    //Quaternion desiredRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
    //    //currentRotation = Quaternion.Slerp(currentRotation, desiredRotation, Time.deltaTime / rotationSmoothTime);
    //    //transform.rotation = currentRotation;
    //}

    //private void LateUpdate()
    //{
    //    if (player == null) return;

    //    // �÷��̾� ���� ��ġ ��� (�÷��̾� forward �ݴ� ���� * �Ÿ� + ����)
    //    Vector3 desiredPosition = player.position - player.forward * distance + Vector3.up * height;

    //    // ī�޶� ��ġ �ε巴�� �̵�
    //    //transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    //    transform.position = desiredPosition;

    //    // �÷��̾ �ٶ󺸵��� ȸ�� (�� �ڸ� ������)
    //    //Quaternion desiredRotation = Quaternion.LookRotation(player.position - transform.position);
    //    //transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    //    Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
    //    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    //}
}
