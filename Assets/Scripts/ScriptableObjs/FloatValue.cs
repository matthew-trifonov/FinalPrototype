using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float value;

    [HideInInspector]
    public float RuntimeValue;
    public void OnAfterDeserialize()
    {
        RuntimeValue = value;
    }

    public void OnBeforeSerialize()
    {
    }
}
