using UnityEngine;

public interface ITrappable
{
    public bool isBeingCaptured { get; set; }
    public bool CaptureAnimation();
    public int PointValue();
}
