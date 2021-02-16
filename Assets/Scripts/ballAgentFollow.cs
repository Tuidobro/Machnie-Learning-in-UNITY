using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballAgentFollow : MonoBehaviour
{
    public Transform BallAgentTransform;

    private Vector3 _cameraOffset;

    private void Start()
    {
        _cameraOffset = transform.position - BallAgentTransform.position;
    }
    void LateUpdate()
    {
        transform.position = BallAgentTransform.position + _cameraOffset; 
    }
}
