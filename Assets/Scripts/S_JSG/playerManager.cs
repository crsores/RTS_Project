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


        
        public int B_atkUpCount, B_armorUpCount, M_GroundatkUpCount, M_GroundarmorUpCount, M_AiratkUpCount, M_AirarmorUpCount;


        //유닛별 업그레이드 체크
        public bool MarineatkRangCheck;
        public bool GoliathRangCheck;
        public bool GhosteyeCheck;
        public bool GhosMpCheck;
        public bool MedicMpCheck;
        public bool VulturespeedCheck;
        public bool WraithMpCheck;
        public bool ScineVesselMpCheck;
        public bool BattleCruiserMpCheck;


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            //SetBasicStats(playerUnits);
           // SetBasicStats(enemyUnits);
            //SetBasicStats(playerBuildings);
        }
        void Start()
        {
            MarineatkRangCheck = false;
            GoliathRangCheck = false;
            GhosteyeCheck = false;
            GhosMpCheck = false;
            MedicMpCheck = false;
            VulturespeedCheck = false;
            WraithMpCheck = false;
            ScineVesselMpCheck = false;
            BattleCruiserMpCheck = false;



            B_atkUpCount = 0;
            B_armorUpCount = 0;
            M_GroundatkUpCount = 0;
            M_GroundarmorUpCount = 0;
            M_AiratkUpCount = 0;
            M_AirarmorUpCount = 0;







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
                        //pU.baseStats = Units.UnitHandler.instance.GetBasicUnitStats(name);
                    }
                    else if (type == enemyUnits)
                    {
                        Units.Enemy.enemyUnit eU = tf.GetComponent<Units.Enemy.enemyUnit>();
                        //set unit stats in each unit
                        //eU.baseStats = Units.UnitHandler.instance.GetBasicUnitStats(name);
                    }
                    else if (type == playerBuildings)
                    {
                        Building.Player.PlayerBuilding pB = tf.GetComponent<Building.Player.PlayerBuilding>();
                        pB.baseStats = Building.BuildingHandler.instance.GetBasicBuildingStats(name);
                    }





                }
            }
        }

        public void B_atkupPlus()
        {
            B_atkUpCount++;


        }
        public void B_armorupPlus()
        {
            B_armorUpCount++;
        }
        public void M_GroundAtkUpPlus()
        {
            M_GroundatkUpCount++;

        }

        public void M_GroundarmorUpPlus()
        {
            M_GroundarmorUpCount++;
        }

        public void M_AiratkUpPlus()
        {
            M_AiratkUpCount++;
        }
        public void M_AirarmorpPlus()
        {
            M_AirarmorUpCount++;
        }


    }
}
