using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class PlayerScoreManager : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public TMP_InputField playerScoreInput;
    public Button addScoreButton;
    public Button editScoreButton;
    public Button removeScoreButton;
    public TMP_Text scoreText;

    private Dictionary<string, int> playerScores = new Dictionary<string, int>();

    void Start()
    {
        addScoreButton.onClick.AddListener(AddScore);
        editScoreButton.onClick.AddListener(EditScore);
        removeScoreButton.onClick.AddListener(RemoveScore);
        UpdateScoreDisplay(); // Hiển thị ban đầu
    }

    bool TryGetInput(out string name, out int score)
    {
        name = playerNameInput.text.Trim();
        string scoreStr = playerScoreInput.text.Trim();

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(scoreStr))
        {
            scoreText.text = "Vui lòng nhập tên và điểm!";
            score = 0;
            return false;
        }

        if (!int.TryParse(scoreStr, out score))
        {
            scoreText.text = "Điểm phải là số nguyên!";
            return false;
        }

        return true;
    }

    public void AddScore()
    {
        if (TryGetInput(out string name, out int score))
        {
            if (playerScores.ContainsKey(name))
            {
                scoreText.text = $" Người chơi \"{name}\" đã tồn tại!";
            }
            else
            {
                playerScores.Add(name, score);
                scoreText.text = $" Đã thêm \"{name}\" với điểm {score}";
            }

            UpdateScoreDisplay();
        }
    }

    public void EditScore()
    {
        if (TryGetInput(out string name, out int score))
        {
            if (playerScores.ContainsKey(name))
            {
                playerScores[name] = score;
                scoreText.text = $" Đã cập nhật điểm của \"{name}\" thành {score}";
            }
            else
            {
                scoreText.text = $" Không tìm thấy người chơi \"{name}\"!";
            }

            UpdateScoreDisplay();
        }
    }

    public void RemoveScore()
    {
        string name = playerNameInput.text.Trim();

        if (string.IsNullOrEmpty(name))
        {
            scoreText.text = " Vui lòng nhập tên người chơi để xóa!";
            return;
        }

        if (playerScores.Remove(name))
        {
            scoreText.text = $" Đã xóa người chơi \"{name}\"";
        }
        else
        {
            scoreText.text = $" Không tìm thấy người chơi \"{name}\"!";
        }

        UpdateScoreDisplay();
    }
void UpdateScoreDisplay()
{
    scoreText.text = ""; // Reset nội dung trước

    if (playerScores.Count == 0)
    {
        scoreText.text = "Chưa có người chơi nào.";
        return;
    }

    scoreText.text = "Danh sách điểm (cao đến thấp):\n";

    // Sắp xếp theo điểm giảm dần
    var sortedScores = playerScores.OrderByDescending(entry => entry.Value);

    foreach (var entry in sortedScores)
    {
        scoreText.text += $"• {entry.Key}: {entry.Value}\n";
    }
}

}
