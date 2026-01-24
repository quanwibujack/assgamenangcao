using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Thông số di chuyển")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Lấy đầu vào di chuyển
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        moveDirection = new Vector3(x, 0, z);

        // Cập nhật các tham số cho Animator 
        if (anim != null)
        {
            // 1. Tốc độ di chuyển
            anim.SetFloat("Speed", moveDirection.magnitude);
            // 2. Trạng thái chạm đất để chuyển đổi Jump_Start -> Jump_Land 
            anim.SetBool("isGrounded", isGrounded);
        }

        // Logic nhảy vật lý 
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Sử dụng lực Impulse để tạo cú nhảy tức thì
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

            // Kích hoạt hoạt ảnh nhảy
            if (anim != null) anim.SetTrigger("Jump");
        }
    }

    void FixedUpdate()
    {
        // Xử lý di chuyển vật lý để đảm bảo mượt mà 
        if (moveDirection.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

            // Xoay nhân vật theo hướng di chuyển
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Kiểm tra va chạm với mặt đất thông qua Tag 
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}