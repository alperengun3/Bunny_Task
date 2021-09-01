using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedTrap : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject trap;
    [SerializeField] private GameObject crackedTrap;
    [SerializeField] private PlayerSettings settings;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringClass.TAG_MUSCLEBUNNY) && settings.isMuscle && settings.muscleTimer > 0)
        {
            trap.SetActive(false);
            crackedTrap.SetActive(true);
            rb.AddForce(Random.Range(-0.25f, 0.25f), Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f), ForceMode.Impulse);
            Invoke("DestroyTrap", 1.5f);
        }
    }

    private void DestroyTrap()
    {
        Destroy(this.gameObject);
        Destroy(crackedTrap.gameObject);
    }
}
