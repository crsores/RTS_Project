using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    public class BuildingStatType : ScriptableObject
    {
        [System.Serializable]
        public class Base
        {
            public float mineral, gas, health, armor, eyesight;


            public enum size
            {
                small,
                normal,
                big
            }

            public size building_size;

            public bool ground;

            public bool mechanic;

            public bool building;

            public bool spawn;

        }
    }
}
