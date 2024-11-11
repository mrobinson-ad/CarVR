using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform targetCamera;

    void Awake()
    {
       targetCamera = GameObject.FindWithTag("MainCamera").transform;
    }
    void Update()
    {
        ApplyBillboard();
    }

    private void ApplyBillboard()
    {
        transform.rotation = targetCamera.rotation;
    }
}

