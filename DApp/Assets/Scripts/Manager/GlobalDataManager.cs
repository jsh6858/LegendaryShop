using System;
using System.Collections;
using System.Collections.Generic;

namespace LegendFramework
{
    public class GlobalDdataManager
    {
        static private List<Weapon> __weaponList;
        static public List<Weapon> WeaponList
        {
            get
            {
                return __weaponList;
            }

            set
            {
                __weaponList = value;
            }
        }

        static private List<Quest> __questList;
        static public List<Quest> QuestList
        {
            get
            {
                return __questList;
            }

            set
            {
                __questList = value;
            }
        }

        static private List<Item> __itemList;
        static public List<Item> ItemList
        {
            get
            {
                return __itemList;
            }

            set
            {
                __itemList = value;
            }
        }
    }

    public class Hunter
    {
        string Name;
        string ThumbImageId;
        string SDImageId;
        int Power;
        WEAPON_TYPE BestMastery;
        int[] Mastery;

        public Hunter(string _name, string _thumb, string _sd, int _power, WEAPON_TYPE _bestMastery)
        {
            Name = _name;
            ThumbImageId = _thumb;
            SDImageId = _sd;
            Power = _power;
            BestMastery = _bestMastery;
            Mastery = new int[(int)WEAPON_TYPE.WEAPON_END];
        }
    }

    public class Weapon
    {
        int No;
        string Name = "Weapon";
        string ThumbImageId = "";
        int Power = 0;
        WEAPON_TYPE Type = WEAPON_TYPE.SWORD;
        WEAPON_GRADE Grade = WEAPON_GRADE.VERY_HIGH;
        WEAPON_STATE State = WEAPON_STATE.NORMAL;
        string Property;
        string Special_Ability_1;
        string Special_Ability_2;

        static private int Count = 0;

        public Weapon(string _name, string _thumb, int _power
            , WEAPON_TYPE _type, WEAPON_GRADE _grade
            , string _property, string _special1, string _special2)
        {
            No = Count;
            Name = _name;
            ThumbImageId = _thumb;
            Power = _power;
            Type = _type;
            Grade = _grade;
            State = WEAPON_STATE.NORMAL;
            Property = _property;
            Special_Ability_1 = _special1;
            Special_Ability_2 = _special2;

            ++Count;
        }
    }

    public class Quest
    {
        int No;
        string Title;
        DateTime StartTime;
        DateTime PeriodTime;
        string PartyName;
        Hunter[] PartyMembers;
        int PartyPower;
        Item[] Equips;
        string MonsterName;
        string MonsterThumbImageId;
        int MonsterPower;
        Item[] Normal_Trophy;
        Item[] Random_Trophy;
        int Trophy_Count;

        public Quest()
        {

        }
    }

    public class Item
    {
        int No;
        string Name;
        string ThumbImageId;
        ITEM_TYPE Type;
    }
}