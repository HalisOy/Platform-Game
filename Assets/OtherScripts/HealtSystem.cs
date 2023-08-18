using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealtSystem
{
    public int Healt;

    public void SetHealt(int healt)
    {
        Healt = Mathf.Clamp(healt, 0, Healt);
    }

    public void Damage(int damage)
    {
        Healt -= damage;
        Healt = Mathf.Clamp(Healt, 0, Healt);
    }
}
