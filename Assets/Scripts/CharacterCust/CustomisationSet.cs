using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class CustomisationSet : MonoBehaviour
{

    #region Variables
    [Header("Texture List")]
    //Texture2D List for skin,hair, mouth, eyes, armour, clothes
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();

    [Header("Index")]
    //index numbers for our current skin, hair, mouth, eyes textures
    public int skinIndex;
    public int hairIndex, mouthIndex, eyeIndex, armourIndex, clothesIndex;
    [Header("Renderer")]
    //renderer for our character mesh so we can reference a material list
    public Renderer character;
    [Header("Max Index")]
    //max amount of skin, hair, mouth, eyes textures that our lists are filling with
    public int skinMax;
    public int hairMax, mouthMax, eyeMax, armourMax, clothesMax;

    [Header("Character Name")]
    //name of our character that the user is making
    public string charName = "Adventurer";
    [Header("Stats")]
    //class

    public string[] statArray = new string[6];
    public int[] stats = new int[6];
    public int[] tempStats = new int[6];
    public int points = 10;

    public CharacterClass characterClass = CharacterClass.Barbarian;
    public string[] selectedClass = new string[8];
    public int selectedIndex = 0;
    public string[] statName;
    public int[] statData;
    #endregion

    public InputField characterName;
    public Dropdown cClass;
    public Text className;
    public Text statPoints;
    public Text strength;
    public Text dexterity;
    public Text constitution;
    public Text wisdom;
    public Text intelligence;
    public Text charisma;


    [Header("Point Change Buttons")]
    public Button strUp;
    public Button strDn;
    public Button dexUp;
    public Button dexDn;
    public Button conUp;
    public Button conDn;
    public Button wisUp;
    public Button wisDn;
    public Button intUp;
    public Button intDn;
    public Button chrUp;
    public Button chrDn;

    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        string[] enumNames = Enum.GetNames(typeof(CharacterClass));
        List<string> names = new List<string>(enumNames);
        cClass.AddOptions(names);


        tempStats[0] = 0;
        tempStats[1] = 0;
        tempStats[2] = 0;
        tempStats[3] = 0;
        tempStats[4] = 0;
        tempStats[5] = 0;



        statArray = new string[] { "Strength", "Dexterity", "Constitution", "Wisdom", "Intelligence", "Charisma" };
        selectedClass = new string[] { "Barbarian", "Bard", "Cleric", "Druid", "Fighter", "Monk", "Paladin", "Ranger", "Rogue", "Sorcerer", "Warlock", "Wizard", };



        #region for loop to pull textures from file
        //for loop looping from 0 to less than the max amount of skin textures we need
        for (int i = 0; i < skinMax; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Skin_#
            Texture2D temp = Resources.Load("Character/Skin_" + i.ToString()) as Texture2D;
            //add our temp texture that we just found to the skin List
            skin.Add(temp);
        }

        for (int i = 0; i < hairMax; i++)
        {

            Texture2D temp = Resources.Load("Character/Hair_" + i.ToString()) as Texture2D;

            hair.Add(temp);
        }

        for (int i = 0; i < eyeMax; i++)
        {

            Texture2D temp = Resources.Load("Character/Eyes_" + i.ToString()) as Texture2D;

            eyes.Add(temp);
        }

        for (int i = 0; i < mouthMax; i++)
        {

            Texture2D temp = Resources.Load("Character/Mouth_" + i.ToString()) as Texture2D;

            mouth.Add(temp);
        }

        for (int i = 0; i < clothesMax; i++)
        {

            Texture2D temp = Resources.Load("Character/Clothes_" + i.ToString()) as Texture2D;

            clothes.Add(temp);
        }

        for (int i = 0; i < armourMax; i++)
        {

            Texture2D temp = Resources.Load("Character/Armour_" + i.ToString()) as Texture2D;

            armour.Add(temp);
        }


        #endregion

        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();

        SetTexture("Skin", skinIndex = 0);
        SetTexture("Eyes", eyeIndex = 0);
        SetTexture("Hair", hairIndex = 0);
        SetTexture("Armour", armourIndex = 0);
        SetTexture("Mouth", mouthIndex = 0);
        SetTexture("Clothes", clothesIndex = 0);

        ChooseClass(selectedIndex);




    }

    #region SetTexture
    void SetTexture(string type, int dir)
    {
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];

        #region Switch Material
        switch (type)
        {
            case "Skin":
                index = skinIndex;
                max = skinMax;
                textures = skin.ToArray();
                matIndex = 1;
                break;

            case "Hair":
                index = hairIndex;
                max = hairMax;
                textures = hair.ToArray();
                matIndex = 3;
                break;

            case "Mouth":
                index = mouthIndex;
                max = mouthMax;
                textures = mouth.ToArray();
                matIndex = 4;
                break;

            case "Eyes":
                index = eyeIndex;
                max = eyeMax;
                textures = eyes.ToArray();
                matIndex = 2;
                break;

            case "Armour":
                index = armourIndex;
                max = armourMax;
                textures = armour.ToArray();
                matIndex = 6;
                break;

            case "Clothes":
                index = clothesIndex;
                max = clothesMax;
                textures = clothes.ToArray();
                matIndex = 5;
                break;
        }
        #endregion
        #region Outside Switch
        index += dir;
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }
        Material[] mat = character.materials;
        mat[matIndex].mainTexture = textures[index];
        character.materials = mat;
        #endregion
        #region Set Material Switch
        switch (type)
        {
            case "Skin":
                skinIndex = index;
                break;

            case "Hair":
                hairIndex = index;
                break;

            case "Mouth":
                mouthIndex = index;
                break;

            case "Eyes":
                eyeIndex = index;
                break;

            case "Armour":
                armourIndex = index;
                break;

            case "Clothes":
                clothesIndex = index;
                break;
        }
        #endregion
    }
    #endregion

    public void Update()
    {
        statPoints.text = "Points " + points;

        strength.text = statArray[0] + "" + (stats[0] + tempStats[0]);
        dexterity.text = statArray[1] + " " + (stats[1] + tempStats[1]);
        constitution.text = statArray[2] + " " + (stats[2] + tempStats[2]);
        wisdom.text = statArray[3] + " " + (stats[3] + tempStats[3]);
        intelligence.text = statArray[4] + " " + (stats[4] + tempStats[4]);
        charisma.text = statArray[5] + " " + (stats[5] + tempStats[5]);



        if (points < 10 && tempStats[0] > 0)
        {
            strDn.interactable = true;

        }

        if (points < 10 && tempStats[1] > 0)
        {
            dexDn.interactable = true;

        }

        if (points < 10 && tempStats[2] > 0)
        {
            conDn.interactable = true;

        }

        if (points < 10 && tempStats[3] > 0)
        {
            wisDn.interactable = true;

        }

        if (points < 10 && tempStats[4] > 0)
        {
            intDn.interactable = true;

        }

        if (points < 10 && tempStats[5] > 0)
        {
            chrDn.interactable = true;

        }


        if (tempStats[0] < 1 && points >= 10)
        {
            strDn.interactable = false;

        }

        if (tempStats[1] < 1 && points >= 10)
        {
            dexDn.interactable = false;

        }

        if (tempStats[2] < 1 && points >= 10)
        {
            conDn.interactable = false;

        }

        if (tempStats[3] < 1 && points >= 10)
        {
            wisDn.interactable = false;

        }

        if (tempStats[4] < 1 && points >= 10)
        {
            intDn.interactable = false;

        }

        if (tempStats[5] < 1 && points >= 10)
        {
            chrDn.interactable = false;

        }



        if (points <= 0)
        {
            strUp.interactable = false;
            dexUp.interactable = false;
            conUp.interactable = false;
            wisUp.interactable = false;
            intUp.interactable = false;
            chrUp.interactable = false;

        }
        if (points > 0)
        {
            strUp.interactable = true;
            dexUp.interactable = true;
            conUp.interactable = true;
            wisUp.interactable = true;
            intUp.interactable = true;
            chrUp.interactable = true;
        }

    }


    void Save()
    {

    }

    #region Mesh Buttons
    public void SkinBackward()
    {
        SetTexture("Skin", -1);
    }

    public void SkinForward()
    {
        SetTexture("Skin", 1);
    }

    public void HairBackward()
    {
        SetTexture("Hair", -1);
    }

    public void HairForward()
    {
        SetTexture("Hair", 1);
    }

    public void EyesForward()
    {
        SetTexture("Eyes", 1);
    }

    public void EyesBackward()
    {
        SetTexture("Eyes", -1);
    }

    public void ArmourForward()
    {
        SetTexture("Armour", 1);
    }

    public void ArmourBackward()
    {
        SetTexture("Armour", -1);
    }

    public void ClothesForward()
    {
        SetTexture("Clothes", 1);
    }

    public void ClothesBackward()
    {
        SetTexture("Clothes", -1);
    }

    public void MouthForward()
    {
        SetTexture("Mouth", 1);
    }

    public void MouthBackward()
    {
        SetTexture("Mouth", -1);
    }

    #endregion

    #region Random and Reset Buttons
    public void ResetButton()
    {
        SetTexture("Skin", skinIndex = 0);
        SetTexture("Eyes", eyeIndex = 0);
        SetTexture("Hair", hairIndex = 0);
        SetTexture("Armour", armourIndex = 0);
        SetTexture("Mouth", mouthIndex = 0);
        SetTexture("Clothes", clothesIndex = 0);
    }

    public void Rand()
    {
        SetTexture("Skin", UnityEngine.Random.Range(0, skinMax - 1));
        SetTexture("Eyes", UnityEngine.Random.Range(0, eyeMax - 1));
        SetTexture("Hair", UnityEngine.Random.Range(0, hairMax - 1));
        SetTexture("Armour", UnityEngine.Random.Range(0, armourMax - 1));
        SetTexture("Mouth", UnityEngine.Random.Range(0, mouthMax - 1));
        SetTexture("Clothes", UnityEngine.Random.Range(0, clothesMax - 1));
    }




    #endregion

    public void NameButton()


    {
        charName = characterName.text;

    }

    void ChooseClass(int className)
    {
        switch (className)
        {
            case 0:
                stats[0] = 15;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 5;
                characterClass = CharacterClass.Barbarian;
                break;

            case 1:
                stats[0] = 5;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 15;
                characterClass = CharacterClass.Bard;
                break;

            case 2:
                stats[0] = 10;
                stats[1] = 5;
                stats[2] = 10;
                stats[3] = 15;
                stats[4] = 10;
                stats[5] = 5;
                characterClass = CharacterClass.Cleric;
                break;

            case 3:
                stats[0] = 5;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 15;
                stats[4] = 10;
                stats[5] = 10;
                characterClass = CharacterClass.Druid;
                break;

            case 4:
                stats[0] = 15;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 5;
                stats[5] = 10;
                characterClass = CharacterClass.Fighter;
                break;

            case 5:
                stats[0] = 10;
                stats[1] = 10;
                stats[2] = 5;
                stats[3] = 15;
                stats[4] = 10;
                stats[5] = 10;
                characterClass = CharacterClass.Monk;
                break;

            case 6:
                stats[0] = 10;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 5;
                stats[5] = 15;
                characterClass = CharacterClass.Paladin;
                break;

            case 7:
                stats[0] = 10;
                stats[1] = 10;
                stats[2] = 5;
                stats[3] = 15;
                stats[4] = 10;
                stats[5] = 10;
                characterClass = CharacterClass.Ranger;
                break;

            case 8:
                stats[0] = 5;
                stats[1] = 15;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 10;
                characterClass = CharacterClass.Rogue;
                break;

            case 9:
                stats[0] = 10;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 5;
                stats[4] = 10;
                stats[5] = 15;
                characterClass = CharacterClass.Sorcerer;
                break;

            case 10:
                stats[0] = 10;
                stats[1] = 10;
                stats[2] = 5;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 15;
                characterClass = CharacterClass.Warlock;
                break;

            case 11:
                stats[0] = 5;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 15;
                stats[5] = 10;
                characterClass = CharacterClass.Wizard;
                break;
        }
    }

    public void ClassButton(int index)
    {
        /*CharacterClass names = (CharacterClass)index;
        className.text = name.ToString();*/
    }

    //I know there is a way to make this just two functions I just couldn't work it out
    #region Point Change Buttons
    public void StrengthUpButton()
    {
        points--;
        tempStats[0]++;
    }

    public void StrengthDownButton()
    {
        if (tempStats[0] > 0)
        {
            points++;
            tempStats[0]--;
        }
        strDn.interactable = false;
    }


    public void DexUpButton()
    {
        points--;
        tempStats[1]++;
    }

    public void DexDownButton()
    {
        if (tempStats[1] > 0)
        {
            points++;
            tempStats[1]--;
        }
        dexDn.interactable = false;
    }

    public void ConUpButton()
    {
        points--;
        tempStats[2]++;
    }

    public void ConDownButton()
    {
        if (tempStats[2] > 0)        {
            points++;
            tempStats[2]--;
        }
        conDn.interactable = false;
    }

    public void WisUpButton()
    {
        points--;
        tempStats[3]++;
    }

    public void WisDownButton()
    {
        if (tempStats[3] > 0)
        {
            points++;
            tempStats[3]--;
        }
        wisDn.interactable = false;
    }

    public void IntUpButton()
    {
        points--;
        tempStats[4]++;
    }

    public void IntDownButton()
    {
        if (tempStats[4] > 0)
        {
            points++;
            tempStats[4]--;
        }
        wisDn.interactable = false;
    }

    public void ChrUpButton()
    {
        points--;
        tempStats[5]++;
    }

    public void ChrDownButton()
    {
        if (tempStats[5] > 0)
        {
            points++;
            tempStats[5]--;
        }
        chrDn.interactable = false;
    }

    #endregion


}


public enum CharacterClass
{
    Barbarian,
    Bard,
    Cleric,
    Druid,
    Fighter,
    Monk,
    Paladin,
    Ranger,
    Rogue,
    Sorcerer,
    Warlock,
    Wizard,
}
