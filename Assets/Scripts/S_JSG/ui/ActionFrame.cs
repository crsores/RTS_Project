using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class ActionFrame : MonoBehaviour
    {
        public static ActionFrame instance = null;

       // [SerializeField] private Button actionButton = null;
        public Transform layoutGroup = null;

        //private List<Button> buttons = new List<Button>();
       public List<GameObject> buttons = new List<GameObject>();

        public PlayerAction actionsList = null;

       // public List<float> SpawnQueue = new List<float>();
        //public List<GameObject> spawnOrder = new List<GameObject>();

      //  public GameObject spawnPoint = null; //나오는 위치
       // public GameObject spawnPoint2 = null; //나오고 이동할 위치

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
           
        }
        public void SetActionButtons(PlayerAction actions ) //버튼이 샛팅될때 (건물 클릭)
        {
            actionsList = actions;
            //spawnPoint = spawnLocation;
            //spawnPoint2 = spawnLocation2;
            
            if (actions.basicUnits.Count > 0)
            {
                foreach(Units.BasicUnit unit in actions.basicUnits)
                {
                
                    //Button btn = Instantiate(actionButton, layoutGroup);
                    //btn.name = unit.name; 
                    GameObject icon = Instantiate(unit.icon, layoutGroup);
                    icon.name = unit.name;
                    //add text etc?..
                    buttons.Add(icon);
                }
            }
            if (actions.basicBuildings.Count > 0)
            {
                foreach(Building.BasicBuilding building in actions.basicBuildings)
                {
                    Debug.Log(actions.basicUnits);
                    //Button btn = Instantiate(actionButton, layoutGroup);
                   // btn.name = building.name;
                   // GameObject icon = Instantiate(building.icon, btn.transform);
                   // buttons.Add(icon);
                }
            }
            if (actions.Behavior.Count > 0)
            {
                foreach(Behavior.player_Skill p_skill in actions.Behavior)
                {

                    GameObject icon = Instantiate(p_skill.icon, layoutGroup);
                    icon.name = p_skill.name;
                    buttons.Add(icon);
                }
            }
        }

        public void ClearActions() //비활성화시 
        {
            foreach (GameObject btn in buttons)
            {
               // buttons.Remove(btn);
                Destroy(btn.gameObject);
            }
            buttons.Clear();
        }

        //public void StartSpawnTimer(string objectToSpwan) //버튼 클릭시 실행
        //{
        //    if (IsUnit(objectToSpwan))
        //    {

        //        Units.BasicUnit unit = IsUnit(objectToSpwan);
        //        SpawnQueue.Add(unit.spawnTime);
        //        spawnOrder.Add(unit.unitPrefab);
        //    }
        //    else if (Isbuilding(objectToSpwan))
        //    {
        //        Building.BasicBuilding building = Isbuilding(objectToSpwan);
        //        SpawnQueue.Add(building.spawnTime);
        //        spawnOrder.Add(building.buildingPrefab);
        //    }
        //    else
        //    {
        //        Debug.Log($"{objectToSpwan} is not a spawnable object");
        //    }
        //    if (SpawnQueue.Count == 1)
        //    {
        //        ActionTimer.instance.StartCoroutine(ActionTimer.instance.SpawnQueueTimer());
        //    }
        //    else if (SpawnQueue.Count == 0)
        //    {
        //        ActionTimer.instance.StopAllCoroutines();
        //    }
        //}
        //private Units.BasicUnit IsUnit(string name)
        //{
        //    if (actionsList.basicUnits.Count > 0)
        //    {
        //        foreach (Units.BasicUnit unit in actionsList.basicUnits)
        //        {
        //            if (unit.name == name)
        //            {
        //                return unit;
        //            }
        //        }
        //    }
        //    return null;
        //}
        //private Building.BasicBuilding Isbuilding(string name)
        //{
        //    if (actionsList.basicBuildings.Count > 0)
        //    {
        //        foreach (Building.BasicBuilding building in actionsList.basicBuildings)
        //        {
        //            if (building.name == name)
        //            {
        //                return building;
        //            }
        //        }
        //    }
        //    return null;
        //}

        //public void SpawnObject() //오브젝트 생산
        //{
        //    GameObject spawnedObject = Instantiate(spawnOrder[0], new Vector3(spawnPoint.transform.position.x,
        //        spawnPoint.transform.position.y, spawnPoint.transform.position.z), Quaternion.identity);
        //    Units.Player.PlayerUnit pu = spawnedObject.GetComponent<Units.Player.PlayerUnit>();
        //    pu.transform.SetParent(GameObject.Find("P_" + pu.unitType.type.ToString() + "s").transform);


        //    spawnedObject.GetComponent<Units.Player.PlayerUnit>().SetDestinatin(spawnPoint2.transform.position);
        //    // SpawnQueue.Remove(SpawnQueue[0]);
        //    spawnOrder.Remove(spawnOrder[0]);
        //}
    }
}
