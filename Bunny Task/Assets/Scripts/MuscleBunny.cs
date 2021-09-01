using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MuscleBunny : BunnyClass
{
    [SerializeField] private GameObject littleBunny;
    [SerializeField] private GameObject muscleBunny;
    [SerializeField] GameObject capsul1;
    [SerializeField] GameObject capsul2;
    [SerializeField] private float dist;
    void Start()
    {
        settings.carrotScore = 0;
        rb = GetComponent<Rigidbody>();
        onRoad = true;
        settings.isMuscle = false;
        dist = Vector3.Distance(capsul1.transform.position, capsul2.transform.position);
    }

    protected override void Update()
    {
        base.Update();
        if (settings.isDeath)
        {
            rb.isKinematic = true;
        }
        if (settings.muscleTimer <= 0)
        {
            littleBunny.SetActive(true);
            muscleBunny.SetActive(false);
            settings.isMuscle = false;
        }
       
    }

    protected override void Move()
    {
        base.Move();
        float xClamp = Mathf.Clamp(transform.position.x, -0.45f, 0.45f);
        muscleBunny.transform.position = new Vector3(xClamp, 0.05f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringClass.TAG_FINISH))
        {
            settings.muscleTimer = 0;
        }
        if (other.CompareTag(StringClass.TAG_CARROT) && settings.isMuscle)
        {
            settings.carrotScore++;
            Destroy(other.gameObject);
        }
    }
}
