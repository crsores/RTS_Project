using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    public class BuildingHandler : MonoBehaviour
    {
        public static BuildingHandler instance = null;

        [SerializeField] private BasicBuilding barraks;



        private void Awake()
        {

            if (instance == null)
            {
                instance = this;
            }

        }
        private void Start()
        {

        }

        public BuildingStatType.Base GetBasicBuildingStats(string type)
        {
            //유닛 정복 가져오기
            BasicBuilding building;
            switch (type)
            {
                case "barrak":
                    building = barraks;
                    break;

                default:
                    Debug.Log($"Unit Type: {type} could not be found or does not exist!");
                    return null;
            }
            return building.baseStats;
        }
    }
}