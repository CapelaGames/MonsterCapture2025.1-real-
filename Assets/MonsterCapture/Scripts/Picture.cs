using UnityEngine;

public class Picture : MonoBehaviour, ITrappable
{
    public void CaptureAnimation()
    {
        //transform.localScale -= Time.deltaTime * 1f;
    }

    public int PointValue()
    {
        return 1;
    }

    void Update()
    {
        float wobble = Mathf.Sin(Time.time * 20f) * 0.1f + 1f;
        transform.localScale = new Vector3(wobble, wobble, wobble);
    }

}
