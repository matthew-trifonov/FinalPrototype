using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
    public bool value;

    [HideInInspector]
    public bool RuntimeValue;
    public void OnAfterDeserialize()
    {
        RuntimeValue = value;
    }

    public void OnBeforeSerialize()
    {
    }
}