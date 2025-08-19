using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class QueueDemo : MonoBehaviour
{
    // add visual for enemy and boss
    
    public GameObject EnemyPrefab;
    public GameObject BossPrefab;
    public Transform SpawnPoint; // Vị trí xuất hiện
    // valuation spawn
    public float SpawnFrequency = 2f;
    private float _lastSpawnTime = 0f;
    private bool _isSpawning;
    public List<string> InitialQueue = new(); // Hiển thị trong Inspector
    private Queue<string> _queue = new();     // Dùng trong logic game
    // button declaration
    public Button Enqueue;
    public Button StartToSpawning;
    public Button Pause;
    // tmp
    public TMP_Text NormalEnemyNameNotice;
    public TMP_Text BossComingNotice;
    public TMP_Text QueueStatusText;
    public TMP_InputField InputField; // Gán từ Inspector

    void Start()
    {
        foreach (string npc in InitialQueue)
        {
            _queue.Enqueue(npc);
        }
        UpdateQueueUI(); // Cập nhật UI ban đầu
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
                GameObject spawnedBoss = Instantiate(BossPrefab, SpawnPoint.position, Quaternion.identity);
                spawnedBoss.transform.SetAsLastSibling(); // Hiển thị trên cùng
                Destroy(spawnedBoss, SpawnFrequency); // Hủy sau SpawnFrequency giây

            }
            else
            {
                NormalEnemyNameNotice.text = $"Spawned: {npc}";
                BossComingNotice.text = "";
                Debug.Log($"Spawned: {npc}");
                GameObject spawnedEnemy = Instantiate(EnemyPrefab, SpawnPoint.position, Quaternion.identity);
                spawnedEnemy.transform.SetSiblingIndex(0); // Hiển thị phía sau
                Destroy(spawnedEnemy, SpawnFrequency); // Hủy sau SpawnFrequency giây


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
