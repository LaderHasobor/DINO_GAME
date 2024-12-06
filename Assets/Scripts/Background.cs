using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [Header("Background Settings")]
    [SerializeField] private Sprite[] backgrounds; // Mảng các background
    [SerializeField] private SpriteRenderer backgroundRenderer; // SpriteRenderer để thay đổi background

    private float timeElapsed;
    private int currentIndex = 0; // Chỉ mục của background hiện tại

    private void Update()
    {
        // Cập nhật thời gian đã trôi qua
        timeElapsed += Time.deltaTime;

        // Kiểm tra nếu đã qua 15 giây, thay đổi background
        if (timeElapsed >= 15f)
        {
            ChangeBackground();
            timeElapsed = 0f; // Đặt lại thời gian sau khi thay đổi background
        }
    }

    private void ChangeBackground()
    {
        // Đặt background theo chỉ mục hiện tại
        backgroundRenderer.sprite = backgrounds[currentIndex];

        // Cập nhật chỉ mục để chuyển sang background tiếp theo
        currentIndex++;

        // Nếu đã đi hết tất cả background, bắt đầu lại từ đầu
        if (currentIndex >= backgrounds.Length)
        {
            currentIndex = 0;
        }
    }
}
