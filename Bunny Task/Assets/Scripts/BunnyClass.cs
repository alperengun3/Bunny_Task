using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BunnyClass : MonoBehaviour
{
    
    protected Rigidbody rb;
    [SerializeField] protected PlayerSettings settings;
    protected Vector3 mousePos;
    protected Vector3 firstPos;
    protected Vector3 mouseDif;
    [SerializeField] protected Camera ortho;
    [SerializeField] protected bool onRoad;
    [SerializeField] protected int carrotScore;

    void Start()
    {
        
    }

    protected virtual void Update()
    {
        if (settings.carrotNum == 5)
        {
            settings.isMuscle = true;
            settings.muscleTimer = 7;
            Invoke("CarrotScore", 0.1f);
        }
        if (settings.isMuscle)
        {
            settings.muscleTimer -= Time.deltaTime;
        }
        if (settings.isPlaying)
        {
            Move();
        }
        if (settings.isPlaying)
        {
            firstPos = Vector3.Lerp(firstPos, mousePos, 0.1f);

            if (Input.GetMouseButtonDown(0))
                MouseDown(Input.mousePosition);

            else if (Input.GetMouseButtonUp(0))
                MouseUp();

            else if (Input.GetMouseButton(0))
                MouseHold(Input.mousePosition);
        }
    }

    protected virtual void MouseDown(Vector3 inputPos)
    {
        mousePos = ortho.ScreenToWorldPoint(inputPos);
        firstPos = mousePos;
    }

    protected virtual void MouseHold(Vector3 inputPos)
    {
        mousePos = ortho.ScreenToWorldPoint(inputPos);
        mouseDif = mousePos - firstPos;
        mouseDif *= settings.sensitivity;
    }

    protected virtual void MouseUp()
    {
        mouseDif = Vector3.zero;
    }

    protected virtual void Move()
    {
        if (settings.isPlaying)
        {
            float xPos = Mathf.Clamp(transform.position.x, -0.45f, 0.45f);
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
            rb.velocity = new Vector3(mouseDif.x, rb.velocity.y, 1 * settings.ForwardSpeed);
        }
    }
    protected void CarrotScore()
    {
        settings.carrotNum = 0;
    }
}
