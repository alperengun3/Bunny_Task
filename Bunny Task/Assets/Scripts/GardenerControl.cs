using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenerControl : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    [SerializeField] private PlayerSettings settings;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringClass.TAG_MUSCLEBUNNY) && settings.isMuscle)
        {
            anim.SetBool(StringClass.TAG_ISGARDENERFALL, true);
            rb.useGravity = true;
            anim.enabled = false;
        }
    }
}
