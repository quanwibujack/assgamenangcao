using UnityEngine;

public class AxeReturn : MonoBehaviour
{
    [Header("Cấu hình thu hồi")]
    public float autoDisableTime = 3f; // Rìu tự biến mất sau 3 giây để quay về pool

    void OnEnable()
    {
        // Khi rìu được lấy ra khỏi pool (SetActive true), bắt đầu đếm ngược
        CancelInvoke(); // Xóa các lệnh chờ cũ nếu có
        Invoke("ReturnToPool", autoDisableTime);
    }

    void ReturnToPool()
    {
        // Tắt rìu để ObjectPooler có thể tái sử dụng (SetActive false)
        gameObject.SetActive(false);
    }

    // Nếu rìu chạm vào sàn hoặc tường thì biến mất luôn cho chân thực
    private void OnCollisionEnter(Collision collision)
    {
        // Bạn có thể kiểm tra tag nếu muốn, ví dụ: if(collision.gameObject.compareTag("Ground"))
        ReturnToPool();
    }
}