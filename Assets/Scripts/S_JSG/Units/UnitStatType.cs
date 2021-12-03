using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public class UnitStatType : ScriptableObject
    {
        [System.Serializable]
        public class Base
        {

            public enum atk_type
            {
                normal,
                vibration,
                blast
            }

            public enum size
            {
                small,
                normal,
                big
            }
            public float mineral, gas, supply, health, armor, armorplus, eyesight, speed, maxmp, mp;
            public float atkRange,atkspeed, attack,airattack, airattackrange,attackplus,airattackplus;

            public int DrodCount;

            public bool bionic;

            public bool mechanic;

            public bool ground;

            public bool air;

            public bool floating;

            public atk_type ground_type;
            public atk_type air_type;
            public size unit_size;

            







        }
    }
}
