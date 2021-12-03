using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RTS.Player;
namespace Units
{
    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler instance=null;

        [SerializeField] private BasicUnit worker, warrior, healer;

        public LayerMask pUnitLayer, eUnitLayer;

        private void Awake()
        {

            if (instance == null)
            {
                instance = this;
            }

        }
        private void Start()
        {
            //pUnitLayer = LayerMask.NameToLayer("playerUnits");
            //eUnitLayer = LayerMask.NameToLayer("enemyUnits");
        }

        //public UnitStatType.Base GetBasicUnitStats(string type)
        //{
        //    //유닛 정복 가져오기
        //    BasicUnit unit;
        //    switch (type)
        //    {
        //        case "worker":
        //            unit = worker;
        //            break;
        //        case "warrior":
        //            unit = warrior;
        //            break;

        //        case "healer":
        //            unit = healer;
        //            break;
        //        default:
        //            Debug.Log($"Unit Type: {type} could not be found or does not exist!");
        //            return null;
        //    }
        //    return unit.baseStats;
        //}
       
    }
}

