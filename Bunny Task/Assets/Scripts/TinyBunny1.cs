using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TinyBunny1 : BunnyClass
{
    private Animator animTiny;
    [SerializeField] private GameObject muscleBunny;
    [SerializeField] private GameObject tinyBunny;
    [SerializeField] private GameObject muscleImage;
    [SerializeField] private GameObject soil;
    [SerializeField] float soilTimer;
    [SerializeField] float yPos;
    [SerializeField] private bool finishBool;
    void Start()
    {
        
        finishBool = false;
        settings.muscleTimer = 7;
        yPos = 0.15f;
        settings.carrotNum = 0;
        onRoad = true;
        settings.isPlaying = false;
        settings.isMuscle = false;
        settings.isDeath = false;
        rb = GetComponent<Rigidbody>();
        animTiny = GetComponentInChildren<Animator>();
        animTiny.SetBool(StringClass.TAG_ISIDLE, true);
        animTiny.SetBool(StringClass.TAG_ISRUNNING, false);
        animTiny.SetBool(StringClass.TAG_ISMUSCLEPUNCH, false);
        animTiny.SetBool(StringClass.TAG_ISDEATH, false);
    }

    protected override void Update()
    {
        base.Update();
        soilTimer += Time.deltaTime;
        if (settings.isMuscle)
        {
            tinyBunny.SetActive(false);
            muscleBunny.SetActive(true);
        }
        if (settings.muscleTimer <= 0)
        {
            yPos = 0.1f;
            onRoad = true;
            animTiny.SetBool(StringClass.TAG_ISIDLE, false);
            animTiny.SetBool(StringClass.TAG_ISRUNNING, true);
            settings.muscleTimer = 7;
        }

        if (Input.GetMouseButtonDown(0))
        {
            settings.isPlaying = true;
            animTiny.SetBool(StringClass.TAG_ISIDLE, false);
            animTiny.SetBool(StringClass.TAG_ISRUNNING, true);
        }
        if (yPos == 0.1f && transform.position.z >= 1 && !settings.isDeath && !finishBool)
        {
            animTiny.SetBool(StringClass.TAG_ISIDLE, false);
            animTiny.SetBool(StringClass.TAG_ISRUNNING, true);
        }
        if (settings.isMuscle)
        {
            muscleImage.SetActive(true);
        }
        else
        {
            muscleImage.SetActive(false);
        }
        if (soilTimer > 0.08f && yPos == -0.15f && !settings.isMuscle)
        {
            Quaternion qRot = Quaternion.Euler(new Vector3(Random.Range(-30, 30), Random.Range(-45, 45), 0));
            Instantiate(soil, new Vector3(transform.localPosition.x, transform.localPosition.y + 0.15f, transform.localPosition.z), qRot);
            soilTimer = 0;
            Invoke("DestroySoil", 3);
        }
    }

    protected override void Move()
    {
        base.Move();
        if (mouseDif.y < -1)
        {
            animTiny.SetBool(StringClass.TAG_ISRUNNING, false);
            yPos = -0.15f;
            onRoad = false;
            animTiny.SetBool(StringClass.TAG_ISUNDERGROUND, true);
        }
        if (mouseDif.y > 1)
        {
            yPos = 0.1f;
            onRoad = true;
            animTiny.SetBool(StringClass.TAG_ISRUNNING, true);
            animTiny.SetBool(StringClass.TAG_ISUNDERGROUND, false);
        }
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        if (yPos == -0.15f)
        {
            float yClamp = Mathf.Clamp(transform.position.y, -0.75f, 0.125f);
            float xClamp = Mathf.Clamp(transform.position.x, -0.45f, 0.45f);
            float yRot = Mathf.Clamp(transform.rotation.y, -8f, 8f);
            tinyBunny.transform.position = new Vector3(transform.position.x, yClamp, transform.position.z);
            tinyBunny.transform.eulerAngles = new Vector3(transform.rotation.x, yRot, transform.rotation.z);
            tinyBunny.transform.position = new Vector3(xClamp, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag(StringClass.TAG_ROCK) || other.CompareTag(StringClass.TAG_FENSE) || other.CompareTag(StringClass.TAG_GARDENER) || other.CompareTag(StringClass.TAG_TRAP)) && onRoad && !settings.isMuscle)
        {
            settings.isDeath = true;
            animTiny.SetBool(StringClass.TAG_ISRUNNING, false);
            animTiny.SetBool(StringClass.TAG_ISDEATH, true);
            settings.isPlaying = false;
            rb.isKinematic = true;
            transform.DOMoveY(0.2f, 2);
            transform.DOMoveZ(transform.position.z - 0.30f, 2);
        }
        if (other.CompareTag(StringClass.TAG_CARROT) && !settings.isMuscle)
        {
            settings.carrotScore++;
            settings.carrotNum++;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(StringClass.TAG_FINISH))
        {
            rb.isKinematic = true;
            settings.isPlaying = false;
            finishBool = true;
            transform.DOMove(new Vector3(0, transform.position.y, transform.position.z + 0.1f ), 1);
            transform.DORotate(new Vector3(0, 180, 0), 1);
            animTiny.SetBool(StringClass.TAG_ISUNDERGROUND, false);
            animTiny.SetBool(StringClass.TAG_ISRUNNING, false);
            animTiny.SetBool(StringClass.TAG_ISDANCE, true);
        }
    }

    private void DestroySoil()
    {
        Destroy(GameObject.FindGameObjectWithTag("soil"));
    }
}
