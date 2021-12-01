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
            public float cost,cost2,cost3,aggroRange, atkRange,atkspeed, attack,attackplus, health, armor,armorplus,speed, eyesight;

            public int DrodCount;

            public bool ground;

            public enum characteristic
            {
                normal,
                vibration,
                explosion




            }

            public enum size
            {
                small,
                normal,
                big

            }










        }
    }
}
