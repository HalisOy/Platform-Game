using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int Level;
    public int Coin;
    public int SkinID;
    public int TotalHealt;
    public List<int> LevelStar;

    public GameData(int LevelData, int CoinData, int SkinIDData, int TotalHealtData, List<int> LevelStarData)
    {
        Level = LevelData;
        Coin = CoinData;
        SkinID = SkinIDData;
        TotalHealt = TotalHealtData;
        LevelStar = LevelStarData;
    }
}
