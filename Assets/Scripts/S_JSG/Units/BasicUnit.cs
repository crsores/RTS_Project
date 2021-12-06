using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "New Unit/Basic", order = 1)]

    public class BasicUnit : ScriptableObject
    {

        public enum unitType
        {
            SCV,
            Marine,
            Firebat,
            Medic,
            Ghost,
            Vulture,
            SiegeTank,
            Goliath,
            Wraith,
            Dropship,
            ScinceVessel,
            BattleCruiser,
            Valkyrie,
            SpiderMine



        };
        //public bool isPlayerUnit;
        [Space(15)]
        [Header("Unit Settings")]
        public unitType type;

        public new string name;

        public GameObject unitPrefab;
        //public GameObject enemyPrefab;
        
        public GameObject icon;
        public float spawnTime;


        [Space(15)]
        [Header("Unit Base Stats")]
        [Space(15)]

        public UnitStatType.Base baseStats;


    }
}
