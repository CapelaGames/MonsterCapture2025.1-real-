using UnityEngine;

public class Picture : MonoBehaviour, ITrappable
{
    private Vector3 originalScale;
    private float timer = 1;

    public bool isBeingCaptured { get; set; } = false;

    void Awake()
    {
        originalScale = transform.localScale;
    }
    public bool CaptureAnimation()
    {
        isBeingCaptured = true;
        timer -= Time.deltaTime * 1f;
        transform.localScale = Vector3.Lerp(Vector3.zero,originalScale , timer);

        if (timer <= 0)
        {
            return false;
        }
        
        return true;
    }

    public int PointValue()
    {
        return 1;
    }

    void Update()
    {
        if(isBeingCaptured) return;
        float wobble = Mathf.Sin(Time.time * 20f) * 0.1f;
        transform.localScale = new Vector3(wobble, wobble, wobble) + originalScale;
    }

}
