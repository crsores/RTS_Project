using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Building.Player
{
    public class PlayerBuilding : MonoBehaviour
    {
        public BasicBuilding buildingType;

        
        public BuildingStatType.Base baseStats;

        public Units.UnitStatDisplay statDisplay;

        private void Start()
        {
            baseStats = buildingType.baseStats;
            statDisplay.SetStatDisplayBasicBuilding(baseStats, true);
        }
    }
}