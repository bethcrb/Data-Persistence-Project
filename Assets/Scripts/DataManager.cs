using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string PlayerName;
    public string BestPlayer;
    public int BestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadPlayerData();
    }

    [System.Serializable]
    class SaveData
    {
        public string PlayerName;
        public string BestPlayer;
        public int BestScore;
    }

    public void SavePlayerData()
    {
        SaveData data = new SaveData();
        data.PlayerName = PlayerName;
        data.BestPlayer = BestPlayer;
        data.BestScore = BestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PlayerName = data.PlayerName;
            BestPlayer = data.BestPlayer;
            BestScore = data.BestScore;
        }
    }
}
