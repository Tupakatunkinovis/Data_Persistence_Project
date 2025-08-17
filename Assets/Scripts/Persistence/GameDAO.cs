using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Analytics.IAnalytic;

[System.Serializable]
public class SaveData
{
    public Player[] players = new Player[10];
}


public class GameDAO
{
    private static readonly GameDAO instance = new GameDAO();
    private string savePath;

    public Player currentPlayer;
    public SaveData saveData;

    // Constructor privado para evitar la creación de instancias externas
    private GameDAO()
    {
        // Establecer la ruta del archivo de guardado
        savePath = Application.persistentDataPath + "/savefile.json";

        // Actualizar los datos actuales
        UpdateCurrentData();

        currentPlayer = new Player();

        Debug.Log("GameDAO initialized, save path: " + savePath);
    }

    public static GameDAO GetInstance()
    {        
        return instance;
    }

    public void UpdateFile()
    {
        if (File.Exists(savePath))
        {
            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(savePath, json);
        }
    }

    public void AddCurrentPlayerToRankings()
    {
        Debug.Log("Trying to add player: Name: " + currentPlayer.name + ", score: " + currentPlayer.score);
        // We check if player is valid to work with it
        if (currentPlayer is null)
        {
            Debug.Log("Player is null");
            return;
        }
        else if (currentPlayer.name is null)
        {
            Debug.Log("Player name is empty");
            return;
        }
        else if (currentPlayer.score == 0)
        {
            Debug.Log("Player score is 0");
            return;
        }

        // Update data to be sure all is ok before proceding
        //UpdateCurrentData();

        // comprobar si el jugador actual ha hecho nuevo record
        if (hasAchievedEnoughPointsToAppearOnRanking())
        {
            Debug.Log("hasAchievedEnoughPointsToAppearOnRanking: 1");
            Player temp = null;
            bool isCurrentPlayerPut = false;

            for (int i = 0; i < saveData.players.Length; i++)
            {
                Debug.Log(saveData.players[i] + " " + i + ": " + saveData.players[i].name + ", " + saveData.players[i].score);
                if (isCurrentPlayerPut)
                {
                    // We swapp the values of temp and the current player in score using a tuple.
                    (saveData.players[i], temp) = (temp, saveData.players[i]);
                    /*Player temp2 = saveData.rankingPlayers[i];
                    saveData.rankingPlayers[i] = temp;
                    temp = temp2;*/
                }
                else if (currentPlayer.score > saveData.players[i].score)
                {
                    isCurrentPlayerPut = true;
                    temp = saveData.players[i];
                    saveData.players[i] = currentPlayer;
                }
            }
            //update data file with the new data.
            UpdateFile();
        }
    }

    public bool hasAchievedEnoughPointsToAppearOnRanking()
    {
        if (saveData.players[9].score < currentPlayer.score)
        {
            return true;
        }

        return false;
    }

    public void UpdateCurrentData()
    {
        if (File.Exists(savePath)) // If the file already exists gets the information
        {
            string json = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        else // If it does not exists creates a new file with empty data.
        {
            saveData = new SaveData
            {
                players = new Player[10]
            };
            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(savePath, json);
        }
        Debug.Log("Update Process finished");
    }

    public Player GetPlayer(int n) {

        return saveData.players[n];

    }
}
