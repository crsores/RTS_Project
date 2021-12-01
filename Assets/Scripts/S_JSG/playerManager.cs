using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using InputManager;

namespace RTS.Player
{
    public class playerManager : MonoBehaviour
    {

        public static playerManager instance=null;

        public Transform playerUnits;
        public Transform enemyUnits;
        public Transform playerBuildings;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            SetBasicStats(playerUnits);
            SetBasicStats(enemyUnits);
            SetBasicStats(playerBuildings);
        }
        void Start()
        {

            

        }

        // Update is called once per frame
        void Update()
        {
            InputHandler.instance.HandleUnitMovement();
        }
        public void SetBasicStats(Transform type) //가져온 정보를 유닛에 세팅
        {
           
            foreach (Transform child in type)
            {
                foreach (Transform tf in child)
                {
                    string name = child.name.Substring(0, child.name.Length - 1).ToLower();
                    //var stats = Units.UnitHandler.instance.GetBasicUnitStats(unitName);

                    if (type == playerUnits)
                    {
                        Units.Player.PlayerUnit pU = tf.GetComponent<Units.Player.PlayerUnit>();
                        pU.baseStats = Units.UnitHandler.instance.GetBasicUnitStats(name);
                    }
                    else if (type == enemyUnits)
                    {
                        Units.Enemy.enemyUnit eU = tf.GetComponent<Units.Enemy.enemyUnit>();
                        //set unit stats in each unit
                        eU.baseStats = Units.UnitHandler.instance.GetBasicUnitStats(name);
                    }
                    else if (type == playerBuildings)
                    {
                        Building.Player.PlayerBuilding pB = tf.GetComponent<Building.Player.PlayerBuilding>();
                        pB.baseStats = Building.BuildingHandler.instance.GetBasicBuildingStats(name);
                    }





                }
            }
        }
    }
}
