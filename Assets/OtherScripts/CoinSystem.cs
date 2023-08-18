using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoinSystem
{
    public int Coin;

    public void CoinCollect()
    {
        Coin++;
    }
    public bool CoinHave(int Item)
    {
        int HaveCoin = Coin - Item;
        if (HaveCoin > 0)
            return true;
        else
            return false;
    }
    public void SellItem(int ItemCoin)
    {
        Coin = Mathf.Clamp(Coin - ItemCoin, 0, int.MaxValue);
    }
}
