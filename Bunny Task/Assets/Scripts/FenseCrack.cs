using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenseCrack : MonoBehaviour
{
    [SerializeField] private GameObject fense;
    [SerializeField] private GameObject crackedFense;
    [SerializeField] private PlayerSettings settings;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringClass.TAG_MUSCLEBUNNY) && settings.isMuscle && settings.muscleTimer > 0)
        {
            fense.SetActive(false);
            crackedFense.SetActive(true);
            rb.AddForce(Random.Range(-1, 1), Random.Range(1.5f, 3), Random.Range(1, 3), ForceMode.Impulse);
            Invoke("DestroyFense", 1.5f);
        }
    }

    private void DestroyFense()
    {
        Destroy(this.gameObject);
        Destroy(crackedFense.gameObject);
    }
}
