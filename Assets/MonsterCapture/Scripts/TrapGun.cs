using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TrapGun : MonoBehaviour
{
    public float shootSpeed = 1000f;
    public GameObject trapPrefab;
    public List<GameObject> traps;
    public Vector3 trapOffset;
    public Vector3 trapRotation;

    public Camera cam;

    private void Awake()
    {
        if (cam == null) { cam = Camera.main; }
        if (cam == null) { cam = FindFirstObjectByType<Camera>(); }
    }

    void OnAttack()
    {
        Vector3 spawnPosition = transform.position + (cam.transform.forward * trapOffset.z);
        spawnPosition.y += trapOffset.y;
        spawnPosition += cam.transform.right * trapOffset.x;

        GameObject trap = Instantiate(trapPrefab, spawnPosition,
            Quaternion.LookRotation(cam.transform.forward, Vector3.up) * Quaternion.Euler(trapRotation));

        trap.GetComponent<Rigidbody>()?.AddForce(cam.transform.forward * shootSpeed);

        traps.Add(trap);
    }
}
