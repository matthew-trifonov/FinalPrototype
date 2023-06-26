using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraPosition : MonoBehaviour
{
    public Transform player;
    public float attraction;
    public Vector2 maxPos;
    public Vector2 minPos;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position != player.position)
        {
            Vector3 cameraPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

            cameraPosition.x = Mathf.Clamp(cameraPosition.x, minPos.x, maxPos.x);
            cameraPosition.y = Mathf.Clamp(cameraPosition.y, minPos.y, maxPos.y);

            transform.position = Vector3.Lerp(transform.position, cameraPosition, attraction);
        }
    }
}
