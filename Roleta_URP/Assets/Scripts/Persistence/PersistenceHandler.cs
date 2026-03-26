using System.IO;
using UnityEngine;

public class PersistenceHandler : MonoBehaviour
{
    private string fileName = "gameData.json";
    private string filePath;

    private void Awake()
    {
        filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
    }

    [ContextMenu("Save Data")]
    public void SaveData(GameDataSO _so)
    {
        GameDataDAO data = new()
        {
            numberOfSlots = _so.NumberOfSlots,
            durationRange = _so.DurationRange,
            speedRange = _so.SpeedRange,
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Data saved to: " + filePath);
    }

    [ContextMenu("Load Data")]
    public GameDataDAO LoadData()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Save file not found.");
            return null;
        }

        string json = File.ReadAllText(filePath);

        GameDataDAO data = JsonUtility.FromJson<GameDataDAO>(json);
        return data;
    }
}
