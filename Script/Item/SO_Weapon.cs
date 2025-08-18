using UnityEngine;

[CreateAssetMenu(fileName = "SO_Weapon", menuName = "Scriptable Objects/SO_Weapon")]
public class SO_Weapon : SO_ItemData
{
    
    public RuntimeAnimatorController weaponAnimator;
    public GameObject weaponPrefab;
    public Vector3 weaponOffset;
    public Vector3 weaponRotation;
    public Vector3 weaponSize;
}
