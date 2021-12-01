using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UI.HUD
{

    [CreateAssetMenu(fileName ="NewPlayerActions",menuName ="PlayerActios")]
    public class PlayerAction : ScriptableObject
    {
        [Space(5)]
        [Header("Units")]
        public  List <Units.BasicUnit> basicUnits = new List<Units.BasicUnit>();
        

        [Header("Buildings")]
        [Space(15)]

        public List<Building.BasicBuilding> basicBuildings = new List<Building.BasicBuilding>();
    }
}