using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public class PlayerDataManager : MonoBehaviour
{
    FirebaseFirestore db;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            if (task.Result == DependencyStatus.Available)
            {
                db = FirebaseFirestore.DefaultInstance;
                Debug.Log("Firebase listo.");
            }
            else
            {
                Debug.LogError("No se pudo conectar con Firebase: " + task.Result);
            }
        });
    }

    public void SavePlayerData(string playerId, int coins, int level1Progress, int level2Progress)
    {
        DocumentReference docRef = db.Collection("players").Document(playerId);

        Dictionary<string, object> playerData = new Dictionary<string, object>
        {
            { "coins", coins },
            { "level1", level1Progress },
            { "level2", level2Progress }
        };

        docRef.SetAsync(playerData).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Datos guardados correctamente.");
            }
            else
            {
                Debug.LogError("Error al guardar: " + task.Exception);
            }
        });
    }

    public void LoadPlayerData(string playerId)
    {
        DocumentReference docRef = db.Collection("players").Document(playerId);

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result.Exists)
            {
                Dictionary<string, object> data = task.Result.ToDictionary();
                Debug.Log("Monedas: " + data["coins"]);
                Debug.Log("Nivel 1: " + data["level1"]);
                Debug.Log("Nivel 2: " + data["level2"]);
            }
            else
            {
                Debug.Log("No hay datos guardados para este jugador.");
            }
        });
    }
}
