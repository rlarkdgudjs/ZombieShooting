using UnityEngine;

public class Rifle : MonoBehaviour , IWeapon
{
    public GameObject muzzleFlashPrefab;
    public Transform muzzleLocation;
    public Transform aimLocation;
    private float destroyTimer = 2f;
    private bool isAiming = false;

    public float fireRate = 10f; // 초당 10발

    private float lastFireTime = Mathf.NegativeInfinity;

    [SerializeField] float baseSpread = 1f;        // 기본 퍼짐
    [SerializeField] float maxSpread = 5f;         // 최대 퍼짐
    [SerializeField] float spreadIncreasePerShot = 0.5f;
    [SerializeField] float spreadRecoveryRate = 2f; // 초당 회복량
    private float currentSpread = 0f;

    public LineRenderer lineRendererPrefab;
    public float lineDuration = 0.05f;
    public void PerformAttack()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aimLocation = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isAiming)
            {
                isAiming = false;
            }
            else
            {
                isAiming = true;
            }
        }
        RecoverSpread();
        HandleFireInput();
    }

    private void HandleFireInput()
    {
        if (Input.GetMouseButton(0))
        {
            TryFire();
        }
    }

    private void TryFire()
    {
        float interval = 1f / fireRate;

        if (Time.time >= lastFireTime + interval)
        {
            FireBullet();
            lastFireTime = Time.time;

            currentSpread += spreadIncreasePerShot;
            currentSpread = Mathf.Min(currentSpread, maxSpread);
        }
    }
    private void RecoverSpread()
    {
        if (!Input.GetMouseButton(0))
        {
            currentSpread -= spreadRecoveryRate * Time.deltaTime;
            currentSpread = Mathf.Max(currentSpread, baseSpread);
        }
    }
    private Vector3 GetSpreadDirection(Vector3 forward, float angle)
    {
        float spreadRadius = Mathf.Tan(angle * Mathf.Deg2Rad);
        Vector2 random = Random.insideUnitCircle * spreadRadius;

        Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;
        Vector3 up = Vector3.Cross(forward, right).normalized;

        return (forward + random.x * right + random.y * up).normalized;
    }

    void FireBullet()
    {
        
        Vector3 direction = GetSpreadDirection(aimLocation.forward, currentSpread);

        
        Vector3 effectdirection = GetSpreadDirection(muzzleLocation.forward, currentSpread);
        Vector3 endpoint = muzzleLocation.position + effectdirection * 100f;

        if (Physics.Raycast(aimLocation.position,direction, out RaycastHit hit, 100f))
        {

            if (hit.collider.gameObject.layer == 11)
            {

                hit.collider.gameObject.GetComponentInParent<Enemy>().currentState = EnemyState.Dead;
            }


        }
        DrawShotLine(muzzleLocation.position, endpoint);
        MuzzleFlash();
    }


    void MuzzleFlash()
    {
        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, muzzleLocation.position, muzzleLocation.rotation);
            tempFlash.transform.SetParent(muzzleLocation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }
    }
    private void DrawShotLine(Vector3 start, Vector3 end)
    {
        LineRenderer lr = Instantiate(lineRendererPrefab);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        Destroy(lr.gameObject, lineDuration);
    }
}
