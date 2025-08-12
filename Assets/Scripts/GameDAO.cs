using UnityEditor.Overlays;
using UnityEngine;
using static Unity.Collections.AllocatorManager;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class GameDAO
{
    private GameDAO()
    {
        // Prevent outside instantiation
    }

    private static readonly GameDAO instance = new GameDAO();

    public static GameDAO GetSingleton()
    {
        return instance;
    }

    public void SaveData()
    {
        SaveScoreData();
        SavePlayerName();
    }

    public void LoadData()
    {
        LoadScoreData();
        LoadPlayerName();
    }

    public void SaveScoreData()
    {

    }

    public void LoadScoreData()
    {

    }

    public void SavePlayerName()
    {

    }

    public void LoadPlayerName()
    {

    }

}
