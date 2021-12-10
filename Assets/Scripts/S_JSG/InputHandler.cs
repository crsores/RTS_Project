using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Player;
using UnityEngine.EventSystems;
namespace InputManager
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance = null;

        private RaycastHit hit;

        public List<Transform> selectedUnits = new List<Transform>();

        public Transform selectedBuilding = null;

        public LayerMask interactableLayer = new LayerMask();
        public LayerMask interactableLayer2 = new LayerMask();

        private bool isDragging = false;

        private Vector3 mousePos;
        private void Awake()
        {

            if (instance == null)
                instance = this;

        }
        void Start()
        {
            //instance = this;
        }

        // Update is called once per frame
        private void OnGUI()
        {
            if (isDragging)
            {
                Rect rect = MultiSelect.GetScreenRect(mousePos, Input.mousePosition);
                MultiSelect.DrawScreenRect(rect, new Color(0f, 0f, 0f, 0.25f));
                MultiSelect.DrawScreenRectBorder(rect, 3, Color.blue);
            }
        }
        public void HandleUnitMovement() // 조작
        {
           
           
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;

                }
                mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100, interactableLayer))
                {
                    if (addeUnit(hit.transform, Input.GetKey(KeyCode.LeftShift)))
                    {
                        // be able to do stuff with units
                    }
                    
                }
                else if (Physics.Raycast(ray, out hit, 100, interactableLayer2)) 
                {
                 if (addeBuilding(hit.transform))
                    {
                        //be able td do stuff with building
                    }

                }




                else
                {
                    isDragging = true;
                    DeselectUnits();
                }
            }
            if (Input.GetMouseButtonUp(0))
            {


                foreach (Transform child in RTS.Player.playerManager.instance.playerUnits)
                {
                   // foreach (Transform unit in child)
                    {
                        if (iswithinSelectionBounds(child))//범위안에 유닛이 있을경우
                        {
                            addeUnit(child, true); ;
                        }
                    }
                }
                isDragging = false;
            }
            if (Input.GetMouseButtonDown(1) && HaveSleletedUnits())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    LayerMask layerHit = hit.transform.gameObject.layer;

                    switch (layerHit.value)
                    {
                        case 6: //Units Layer

                           for (int i = 0; selectedUnits[i]; i++)
                            {
                                //selectedUnits[i].GetComponent<>().baseStats.    

                            }

                            break;
                        case 9: //enemy untis layer

                            break;
                        default:
                            foreach (Transform unit in selectedUnits)
                            {
                                PlayerUnit pU = unit.gameObject.GetComponent<PlayerUnit>();
                                pU.SetDestinatin(hit.point);

                            }
                            break;
                    }

                }

            }
            else if (Input.GetMouseButtonDown(1)&&selectedBuilding != null)
            {

                 if (selectedBuilding.GetComponent<Building.Player.PlayerBuilding>().baseStats.spawn==true)
                selectedBuilding.gameObject.GetComponent<Building.BuildingSpawn>().SetSpawnMarkerLocation();//마우스 클릭한곳 스폰 위치 지정
            }
            else if (Input.GetMouseButtonDown(1) && selectedUnits.Count > 0)
            {




            }
        }
        //private void SelectUnit(Transform unit, bool canMultiselect = false) //유닛 선택
        //{
        //    if (!canMultiselect) //다중일시
        //    {
        //        DeselectUnits();

        //    }
        //    if (selectedUnits.Count < 12)
        //    {
        //        selectedUnits.Add(unit);
        //        unit.Find("Highlight").gameObject.SetActive(true);
        //    }

        //}
        private void DeselectUnits() //선택 해제
        {
            if (selectedBuilding)
            {
                selectedBuilding.gameObject.GetComponent<Interactables.IBuilding>().OnInteractExit();
                selectedBuilding = null;
            }
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                selectedUnits[i].gameObject.GetComponent<Interactables.IUnit>().OnInteractExit();
            }
            selectedUnits.Clear();
        }
        private bool iswithinSelectionBounds(Transform tf) //드래그 범위안 체크
        {
            if (!isDragging)
            {
                return false;
            }
            Camera cam = Camera.main;
            Bounds vpBounds = MultiSelect.GetVPBounds(cam, mousePos, Input.mousePosition);
            return vpBounds.Contains(cam.WorldToViewportPoint(tf.position));
        }
        private bool HaveSleletedUnits() //유닛을 선택 했는지
        {
            if (selectedUnits.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private Interactables.IUnit addeUnit(Transform tf, bool canMultiselect = false)
        {
            Interactables.IUnit iUnit = tf.GetComponent<Interactables.IUnit>();
            if (iUnit)
            {
                if (!canMultiselect)
                {
                    DeselectUnits();
                }

                selectedUnits.Add(iUnit.gameObject.transform);

                iUnit.OnInteractEnter();

                return iUnit;

            }
            else
            {
                return null;
            }

            

        }


        private Interactables.IBuilding addeBuilding(Transform tf)
        {
            Interactables.IBuilding iBuilding = tf.GetComponent<Interactables.IBuilding>();

            if (iBuilding)
            {
                DeselectUnits();

                selectedBuilding = iBuilding.gameObject.transform;

                iBuilding.OnInteractEnter();

                return iBuilding;
            }
            else
            {
                return null;
            }

        }
        public void UnitSpawn(string objectToSpwan)
        {
            
            
                selectedBuilding.GetComponent<Building.BuildingSpawn>().StartSpawnTimer(objectToSpwan);
           

        }

        

    }
            

    
}
