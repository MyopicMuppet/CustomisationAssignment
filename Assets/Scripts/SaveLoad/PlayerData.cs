using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public List<Texture2D> skin;
    public List<Texture2D> hair;
    public List<Texture2D> mouth;
    public List<Texture2D> eyes;
    public List<Texture2D> armour;
    public List<Texture2D> clothes;

    public int skinIndex;
    public int hairIndex, mouthIndex, eyeIndex, armourIndex, clothesIndex;

    public string charName;

    public int[] stats;

    public CharacterClass characterClass;

    public PlayerData (CustomisationSet set)
    {
        skin = set.skin;
        hair = set.hair;
        mouth = set.mouth;
        eyes = set.eyes;
        armour = set.armour;
        clothes = set.clothes;

        skinIndex = set.skinIndex;
        hairIndex = set.hairIndex;
        mouthIndex = set.mouthIndex;
        eyeIndex = set.eyeIndex;
        armourIndex = set.armourIndex;
        clothesIndex = set.clothesIndex;

        charName = set.charName;

        stats = new int[6];
        stats[0] = set.stats[0] + set.tempStats[0];
        stats[1] = set.stats[1] + set.tempStats[1];
        stats[2] = set.stats[2] + set.tempStats[2];
        stats[3] = set.stats[3] + set.tempStats[3];
        stats[4] = set.stats[4] + set.tempStats[4];
        stats[5] = set.stats[5] + set.tempStats[5];

        CharacterClass characterClass = set.characterClass;
    }
}
