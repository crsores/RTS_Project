using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    [CreateAssetMenu(fileName = "Building", menuName = "New Building/Basic")]
    public class BasicBuilding : ScriptableObject
    {
        public enum buildingType
        {
            CommandCenter,
            SupplyDepot,
            VespeneRefinery,
            Barracks,
            EngineeringBay,
            Academy,
            MissileTurret,
            Bunker,
            Factory,
            Armory,
            Starport,
            ScinceFacility,


        }

        [Space(15)]
        [Header("Building Settings")]

        public buildingType type;
        public new string name;
        public GameObject buildingPrefab;
        public GameObject icon;
       // public List<GameObject> icon2 = new List<GameObject>();
        public float spawnTime;

        [Space(15)]
        public BuildingStatType.Base baseStats;
      

      

    }
}
