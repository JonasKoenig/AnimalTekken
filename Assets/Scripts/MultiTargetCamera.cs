using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Camera))]
public class MultiTargetCamera : MonoBehaviour
{

    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = .5f;
    public float bufferZone = 10f;
    private Camera cam;

    void Start(){
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (targets.Count == 0)
            return;


        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        foreach (Transform t in targets)
        {
            bounds.Encapsulate(t.position);
        }

        float fovWidth  = 2.0f * Mathf.Atan((bounds.size.x+bufferZone) * 0.5f / (bounds.center.z - transform.position.z)) * Mathf.Rad2Deg;
        float fovHeight = 2.0f * Mathf.Atan((bounds.size.y+bufferZone) * 0.5f / (bounds.center.z - transform.position.z)) * Mathf.Rad2Deg;

        transform.position = Vector3.Lerp(transform.position, bounds.center + offset, smoothTime);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, Mathf.Max(fovWidth,fovHeight), smoothTime);

    }

}
