using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class QueueDemo : MonoBehaviour
{
    public float SpawnFrequency = 2f;
    private float _lastSpawnTime = 0f;
    private bool _isSpawning;
    private Queue<string> _queue = new();

    public Button Enqueue;
    public Button StartToSpawning;
    public Button Pause;

    public TMP_Text NormalEnemyNameNotice;
    public TMP_Text BossComingNotice;
    public TMP_Text QueueStatusText;
    public TMP_InputField InputField; // Gán từ Inspector

    void Start()
    {
        List<string> wave = new() { "Enemy", "Enemy", "Boss", "Enemy" };
        GetComponent<QueueDemo>().AddQueue(wave);
    }
    void Update()
    {
        if (!_isSpawning || _queue.Count == 0)
        {
            UpdateQueueUI(); // Luôn cập nhật UI
            return;
        }

        if (Time.time > _lastSpawnTime + SpawnFrequency)
        {
            string npc = _queue.Dequeue();
            _lastSpawnTime = Time.time;

            if (npc == "Boss")
            {
                BossComingNotice.text = "⚠️ Boss is coming!";
                NormalEnemyNameNotice.text = "";
                Debug.Log("Boss is coming");
            }
            else
            {
                NormalEnemyNameNotice.text = $"Spawned: {npc}";
                BossComingNotice.text = "";
                Debug.Log($"Spawned: {npc}");
            }

            UpdateQueueUI(); // Cập nhật sau khi dequeue
        }
    }


    // Thêm một danh sách NPC vào hàng đợi
    public void AddToQueue()
    {
        string inputText = InputField.text.Trim();

        if (!string.IsNullOrEmpty(inputText))
        {
            _queue.Enqueue(inputText);
            Debug.Log($"Đã thêm vào hàng đợi: {inputText}");
            UpdateQueueUI(); // Cập nhật UI sau khi thêm
            InputField.text = ""; // Xóa nội dung sau khi thêm
        }
        else
        {
            Debug.LogWarning("Không có nội dung để thêm!");
        }
    }

    public void StartSpawning()
    {
        _isSpawning = true;
    }

    public void PauseSpawning()
    {
        _isSpawning = false;
    }
    private void UpdateQueueUI()
    {
        int count = _queue.Count;
        QueueStatusText.text = $"NPCs Remaining: {count}\nOrder: ";

        foreach (string npc in _queue)
        {
            QueueStatusText.text += npc + " → ";
        }

        if (count == 0)
        {
            QueueStatusText.text += "None";
        }
    }
    public void AddQueue(List<string> npcList)
    {
        foreach (string npc in npcList)
        {
            _queue.Enqueue(npc);
        }
    }

}
