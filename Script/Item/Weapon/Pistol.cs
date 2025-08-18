using System.Net;
using UnityEngine;

public class Pistol : MonoBehaviour,IWeapon
{ 
    [Header("Prefab Refrences")]
    
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform muzzelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")][SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")][SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")][SerializeField] private float ejectPower = 150f;

    public LineRenderer lineRendererPrefab;
    public float lineDuration = 0.05f;

    public LayerMask enemyLayer;
    private bool isAiming = false;
    void Start()
    {
        muzzelLocation = Camera.main.transform;
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
    }

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

        //If you want a different input, change it here
        if (Input.GetButtonDown("Fire1") & isAiming)
        {
            //Calls animation on the gun that has the relevant animation events that will fire
            gunAnimator.SetTrigger("Fire");
        }
    }

    public void PerformAttack()
    {

    }

    //This function creates the bullet behavior
    void Shoot()
    {
        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);
            tempFlash.transform.SetParent(barrelLocation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        Vector3 endpoint = muzzelLocation.position + muzzelLocation.forward * 100f;
        DrawShotLine(barrelLocation.position, endpoint);
        // Create a bullet and add force on it in direction of the barrel
        if (Physics.Raycast(muzzelLocation.position, muzzelLocation.forward, out RaycastHit hit, 50f))
        {
            
            if (hit.collider.gameObject.layer==11)
            {
                
                hit.collider.gameObject.GetComponentInParent<Enemy>().currentState = EnemyState.Dead;
            }
            
        }
    }
    private void DrawShotLine(Vector3 start, Vector3 end)
    {
        LineRenderer lr = Instantiate(lineRendererPrefab);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        Destroy(lr.gameObject, lineDuration);
    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }
}
