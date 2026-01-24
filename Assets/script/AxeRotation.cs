using UnityEngine;

public class AxeRotation : MonoBehaviour
{
    public float rotationSpeed = 500f; // Tốc độ xoay

    void Update()
    {
        // Xoay rìu quanh trục X hoặc Z tùy theo hướng Prefab của bạn
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}