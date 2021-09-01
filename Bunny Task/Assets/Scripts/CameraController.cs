using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform followed;
    [SerializeField] private float dist;


    void Start()
    {
        dist = transform.position.z - followed.transform.position.z;
    }

    void Update()
    {
        transform.position = new Vector3(followed.position.x + .75f, transform.position.y, followed.position.z + dist);
    }
}
