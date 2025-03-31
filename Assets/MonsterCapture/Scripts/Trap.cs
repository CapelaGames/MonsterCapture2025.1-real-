using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ITrappable>(out ITrappable pals))
        {
            StartCoroutine(Capture(pals));
        }
    }
    
    IEnumerator Capture(ITrappable pals)
    {
        while (true)
        {
            rb.isKinematic = true;
            transform.rotation *= Quaternion.AngleAxis(Time.deltaTime * 100,
                                                        Vector3.right);
            pals.CaptureAnimation();
            yield return null;
        }
    }
}
