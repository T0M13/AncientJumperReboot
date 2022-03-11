using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "/ScriptableHolder", menuName = "ScriptableHolders/ScriptableHolder")]
public class ScriptableHolderScriptableObject : ScriptableObject
{
    public List<PlatformScriptableObject> platformScriptableObject;
}
