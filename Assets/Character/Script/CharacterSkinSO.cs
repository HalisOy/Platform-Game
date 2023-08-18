using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Skin", menuName = "ScriptableObjects/Character Skin")]
public class CharacterSkinSO : ScriptableObject
{
    public int ID;
    public RuntimeAnimatorController PlayerAnimator;
    public Sprite PlayerImage;
}
