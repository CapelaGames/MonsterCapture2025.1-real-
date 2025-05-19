using UnityEngine;
public class InputDisableChild : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Transform[] children = GetComponentsInChildren<Transform>(true);
            if(children.Length <= 1) return;
            
            bool active = !children[1].gameObject.activeSelf;

            foreach (var child in children)
            {
                if(child == transform) continue;
                
                child.gameObject.SetActive(active);
            }
        }
    }
}
