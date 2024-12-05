using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [Header("Background Settings")]
    [SerializeField] private Sprite[] backgrounds; // Mảng các background
    [SerializeField] private SpriteRenderer backgroundRenderer; // SpriteRenderer để thay đổi background

    private float timeElapsed;
    private int currentIndex = 0; // Chỉ mục của background hiện tại

    private void Start()
    {
        // Xáo trộn mảng backgrounds để đảm bảo các background không bị lặp lại
        ShuffleBackgrounds();
    }

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

        // Nếu đã đi hết tất cả background, xáo trộn lại và bắt đầu lại chu kỳ
        if (currentIndex >= backgrounds.Length)
        {
            currentIndex = 0;
            ShuffleBackgrounds();
        }
    }

    private void ShuffleBackgrounds()
    {
        // Xáo trộn mảng backgrounds để đảm bảo mỗi chu kỳ sẽ có một thứ tự ngẫu nhiên
        for (int i = 0; i < backgrounds.Length; i++)
        {
            Sprite temp = backgrounds[i];
            int randomIndex = Random.Range(i, backgrounds.Length);
            backgrounds[i] = backgrounds[randomIndex];
            backgrounds[randomIndex] = temp;
        }
    }
}
