using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class PlayerData
{
    public float speed;
}

[System.Serializable]
public class PulpitData
{
    public float minPulpitDestroyTime;
    public float maxPulpitDestroyTime;
    public float pulpitSpawnTime;
}

[System.Serializable]
public class GameData
{
    public PlayerData player_data;
    public PulpitData pulpit_data;
}

public class GameDataManager : MonoBehaviour
{
    public string jsonURL = "https://s3.ap-south-1.amazonaws.com/superstars.assetbundles.testbuild/doofus_game/doofus_diary.json";
    public GameData gameData;

    void Start()
    {
        StartCoroutine(DownloadAndApplyGameData());
    }

    IEnumerator DownloadAndApplyGameData()
    {
        UnityWebRequest request = UnityWebRequest.Get(jsonURL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonText = request.downloadHandler.text;
            gameData = JsonUtility.FromJson<GameData>(jsonText);

            // Apply data to DoofusController
            DoofusController doofusController = FindObjectOfType<DoofusController>();
            if (doofusController != null)
            {
                doofusController.speed = gameData.player_data.speed; // Corrected this line
            }

            // Apply data for PulpitManager
            PulpitManager pulpitManager = FindObjectOfType<PulpitManager>(); // Corrected this line
            if (pulpitManager != null)
            {
                pulpitManager.minPulpitDestroyTime = gameData.pulpit_data.minPulpitDestroyTime; // Corrected this line
                pulpitManager.maxPulpitDestroyTime = gameData.pulpit_data.maxPulpitDestroyTime; // Corrected this line
                pulpitManager.pulpitSpawnTime = gameData.pulpit_data.pulpitSpawnTime; // Corrected this line and added the semicolon
            }

            // Log the data to verify
            Debug.Log($"Speed: {gameData.player_data.speed}");
            Debug.Log($"Min Pulpit Destroy Time: {gameData.pulpit_data.minPulpitDestroyTime}");
            Debug.Log($"Max Pulpit Destroy Time: {gameData.pulpit_data.maxPulpitDestroyTime}");
            Debug.Log($"Pulpit Spawn Time: {gameData.pulpit_data.pulpitSpawnTime}");
        }
        else
        {
            Debug.LogError("Failed to download JSON file.");
        }
    }
}
