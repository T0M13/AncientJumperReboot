using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "/Platform", menuName = "ScriptableObjects/Platform")]
public class PlatformScriptableObject : ScriptableObject
{
    public new string name;
    public float jumpForce;
    public float despawnRange;
    public GameObject platformPrefab;
    public PlatformTypes platformType;

    public enum PlatformTypes
    {
        normal,
        breakable,
        boosted,
        baseground,

    }
}
