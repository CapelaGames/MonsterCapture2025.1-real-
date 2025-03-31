using System.Collections;
using UnityEngine;

public class TrapLifeSpan : MonoBehaviour
{
    MeshRenderer[] renderers;
    Light[] lights;

    void Start()
    {
        //renderer = GetComponent<MeshRenderer>();
        renderers = GetComponentsInChildren<MeshRenderer>();
        lights = GetComponentsInChildren<Light>();
        StartCoroutine(LifeSpan());
        
    }

    IEnumerator LifeSpan()
    {
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();

        Color currentColor = propertyBlock.GetColor("_BaseColor");
        currentColor.a = 0;
        propertyBlock.SetColor("_BaseColor", currentColor);

        yield return new WaitForSeconds(1f);

        foreach(Light light in lights)
        {
            light.intensity = 0;
        }

        foreach(MeshRenderer renderer in renderers)
        {
            renderer.SetPropertyBlock(propertyBlock);
        }

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

    }
}
