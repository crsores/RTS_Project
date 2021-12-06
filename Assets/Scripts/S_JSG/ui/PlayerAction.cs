using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UI.HUD
{

    [CreateAssetMenu(fileName ="NewButtonList",menuName = "NewButtonList")]
    public class PlayerAction : ScriptableObject
    {
        [Space(5)]
        [Header("Units")]
        public  List <Units.BasicUnit> basicUnits = new List<Units.BasicUnit>();
        

        [Header("Buildings")]
        [Space(15)]

        public List<Building.BasicBuilding> basicBuildings = new List<Building.BasicBuilding>();

        [Space(15)]


        [Header("Button")]
        public List<Behavior.player_Skill>Behavior = new List<Behavior.player_Skill>();
    }
}