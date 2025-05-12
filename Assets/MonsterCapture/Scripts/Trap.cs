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
            if (pals.isBeingCaptured) return;
            HighScoreManager.instance?.IncreaseScore(pals.PointValue());
            StartCoroutine(Capture(pals, other.gameObject));
        }
    }
    
    IEnumerator Capture(ITrappable pals, GameObject go)
    {
        bool isAnimationPlaying = true;
        while (isAnimationPlaying)
        {
            rb.isKinematic = true;
            transform.rotation *= Quaternion.AngleAxis(Time.deltaTime * 100,
                Vector3.right);
            isAnimationPlaying = pals.CaptureAnimation();
            yield return null;
        }
        Destroy(go);
    }
}
