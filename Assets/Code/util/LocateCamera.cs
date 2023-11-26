using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateCamera : MonoBehaviour
{
    private Transform mainCamera;

    private void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
    }
    private void Update()
    {
        transform.LookAt(mainCamera);
    }
}
