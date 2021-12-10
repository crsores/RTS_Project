using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Building
{
    public class BuildingSpawn : MonoBehaviour
    {
        public GameObject spawnMakrer = null; //유닛이 생산되는 위치 
        public GameObject spawnMakrer2 = null; //생산되고 이동할 위치 

        public List<float> SpawnQueue = new List<float>();
        public List<GameObject> spawnOrder = new List<GameObject>();




        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void SetSpawnMarkerLocation()//마우스 클릭한곳 스폰 위치 지정
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                spawnMakrer2.transform.position = hit.point;
            }

        }
        public void StartSpawnTimer(string objectToSpwan) //버튼 클릭시 실행
        {
            if (IsUnit(objectToSpwan))
            {

                Units.BasicUnit unit = IsUnit(objectToSpwan);
                if (spawnOrder.Count < 5 && SpawnQueue.Count < 5)
                {

                    SpawnQueue.Add(unit.spawnTime);
                    spawnOrder.Add(unit.unitPrefab);
                }
                else
                {
                    Debug.Log("full");
                }
            }
            else if (Isbuilding(objectToSpwan))
            {
                Building.BasicBuilding building = Isbuilding(objectToSpwan);
                SpawnQueue.Add(building.spawnTime);
                spawnOrder.Add(building.buildingPrefab);
            }
            else
            {
                Debug.Log($"{objectToSpwan} is not a spawnable object");
            }
            if (SpawnQueue.Count == 1)
            {
                StartCoroutine(SpawnQueueTimer());
            }
            else if (SpawnQueue.Count == 0)
            {
                StopCoroutine(SpawnQueueTimer());
                //StopAllCoroutines();
            }
        }
        private Units.BasicUnit IsUnit(string name)
        {
            if (UI.HUD.ActionFrame.instance.
                actionsList.basicUnits.Count > 0)
            {
                foreach (Units.BasicUnit unit in UI.HUD.ActionFrame.instance.actionsList.basicUnits)
                {
                    if (unit.name == name)
                    {
                        return unit;
                    }
                }
            }
            return null;
        }
        private Building.BasicBuilding Isbuilding(string name)
        {
            if (UI.HUD.ActionFrame.instance.actionsList.basicBuildings.Count > 0)
            {
                foreach (Building.BasicBuilding building in UI.HUD.ActionFrame.instance.actionsList.basicBuildings)
                {
                    if (building.name == name)
                    {
                        return building;
                    }
                }
            }
            return null;
        }

        public void SpawnObject() //오브젝트 생산
        {
            GameObject spawnedObject = Instantiate(spawnOrder[0], new Vector3(spawnMakrer.transform.position.x,
                spawnMakrer.transform.position.y, spawnMakrer.transform.position.z), Quaternion.identity);
            Units.Player.PlayerUnit pu = spawnedObject.GetComponent<Units.Player.PlayerUnit>();
            //pu.transform.SetParent(GameObject.Find("P_" + pu.unitType.type.ToString() + "s").transform);
            pu.transform.SetParent(RTS.Player.playerManager.instance.playerUnits);

            spawnedObject.GetComponent<Units.Player.PlayerUnit>().SetDestinatin(spawnMakrer2.transform.position);
            // SpawnQueue.Remove(SpawnQueue[0]);
            spawnOrder.Remove(spawnOrder[0]);
        }
        public IEnumerator SpawnQueueTimer()
        {
            if (SpawnQueue.Count > 0)
            {
                Debug.Log($"Wating for {SpawnQueue[0]}");

                yield return new WaitForSeconds(SpawnQueue[0]);
                SpawnObject();

                SpawnQueue.Remove(SpawnQueue[0]);

                if (SpawnQueue.Count > 0)
                {
                    StartCoroutine(SpawnQueueTimer());
                }
            }

        }

    }
}
