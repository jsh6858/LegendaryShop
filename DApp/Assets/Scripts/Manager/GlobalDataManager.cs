using System;
using System.Collections;
using System.Collections.Generic;

namespace LegendFramework
{
    public class GlobalDdataManager
    {
        static private string __playerName;
        static public string PlayerName
        {
            get
            {
                return __playerName;
            }

            set
            {
                __playerName = value;
            }
        }

        static private List<Hunter> __hunterList;
        static public List<Hunter> HunterList;

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

        static private List<Party> __partyList;
        static public List<Party> PartyList
        {
            get
            {
                return __partyList;
            }

            set
            {
                __partyList = value;
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

        static private List<Quest> __questProgressList;
        static public List<Quest> QuestProgressList
        {
            get
            {
                return __questProgressList;
            }

            set
            {
                __questProgressList = value;
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

        static private List<Monster> __monsterList;
        static public List<Monster> MonsterList
        {
            get
            {
                return __monsterList;
            }

            set
            {
                __monsterList = value;
            }
        }
    }

    public class Hunter
    {
        public int No;
        public string Name;
        public string ThumbImageId;
        public string SDImageId;
        public int Power;
        public WEAPON_TYPE BestMastery;
        public int[] Mastery;

        public Hunter(int _no, string _name, string _thumb, string _sd, int _power, WEAPON_TYPE _bestMastery, int[] _mastery)
        {
            No = _no;
            Name = _name;
            ThumbImageId = _thumb;
            SDImageId = _sd;
            Power = _power;
            BestMastery = _bestMastery;
            Mastery = _mastery;
        }
    }

    public class Weapon
    {
        public int No;
        public string Name;
        public string ThumbImageId;
        public int Power = 0;
        public WEAPON_TYPE Type;
        public WEAPON_GRADE Grade;
        public WEAPON_STATE State;
        public string Property;
        public string Special_Ability_1;
        public string Special_Ability_2;
        
        public Weapon(int _no, string _name, string _thumb, int _power
            , WEAPON_TYPE _type, WEAPON_GRADE _grade
            , string _property, string _special1, string _special2)
        {
            No = _no;
            Name = _name;
            ThumbImageId = _thumb;
            Power = _power;
            Type = _type;
            Grade = _grade;
            State = WEAPON_STATE.NORMAL;
            Property = _property;
            Special_Ability_1 = _special1;
            Special_Ability_2 = _special2;
        }
    }

    public class Party
    {
        public int No;
        public string PartyName;
        public int PartyPower;
        public int[] PartyMembers;
        public int[] Equips;

        public Party(int _no, string _name, int _power, int[] _members, int[] _equips)
        {
            No = _no;
            PartyName = _name;
            PartyPower = _power;
            PartyMembers = _members;
            Equips = _equips;
        }
        public void SetEquips(int _idx, int _no)
        {
            Equips[_idx] = _no;
        }
    }

    public class Monster
    {
        public int No;
        public string MonsterName;
        public string MonsterThumbImageId;
        public string MonsterSdId;
        public int MonsterPower;

        public Monster(int _no, string _name, string _thumb, string _sd, int _power)
        {
            No = _no;
            MonsterName = _name;
            MonsterThumbImageId = _thumb;
            MonsterSdId = _sd;
            MonsterPower = _power;
        }
    }

    public class Quest
    {
        public int No;
        public string Title;
        public int PeriodTime;

        public int MyParty;
        public int MyMonster;

        public int[] Normal_Trophy;    //2, random
        public int[] Random_Trophy;    //6, random
        public int Trophy_Count_Possible;
        public int Trophy_Count_Selected;
        public bool[] Trophy_Checked;
        
        public Quest(int _no, string _title, int _periodTime, int _party, int _monster
            , int[] _normalTrophy, int[] _randomTrophy)
        {
            No = _no;
            Title = _title;
            PeriodTime = _periodTime;

            MyParty = _party;
            MyMonster = _monster;
            
            Normal_Trophy = _normalTrophy;
            Random_Trophy = _randomTrophy;
            Trophy_Count_Possible = 2;
            Trophy_Count_Selected = 2;
            Trophy_Checked = new bool[] { true, true, false, false, false, false };
        }

        public void SetPeriodTime()
        {
            --PeriodTime;
        }
        public void AddCountPick()
        {
            ++Trophy_Count_Possible;
        }
        public void SelectTrophy(int _idx)
        {
            if(Trophy_Count_Possible > Trophy_Count_Selected)
            {
                Trophy_Checked[_idx] = true;
                ++Trophy_Count_Selected;
            }
        }
        public void DeselectTrophy(int _idx)
        {
            if (2 < Trophy_Count_Selected)
            {
                Trophy_Checked[_idx] = true;
                --Trophy_Count_Selected;
            }
        }
    }

    public class Item
    {
        public int No;
        public string Name;
        public string ThumbImageId;
        public ITEM_TYPE Type;
        public string Property;
        public string IconImageId;
        
        public Item(int _no, string _name, string _thumb, ITEM_TYPE _type, string _property, string _icon)
        {
            No = _no;
            Name = _name;
            ThumbImageId = _thumb;
            Type = _type;
            Property = _property;
            IconImageId = _icon;
        }
    }
}