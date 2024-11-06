using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform targetCamera;
    void Update()
    {
        ApplyBillboard();
    }

    private void ApplyBillboard()
    {
        transform.rotation = targetCamera.rotation;
    }
}

