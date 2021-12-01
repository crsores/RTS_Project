using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Craft  // 생성할 건물들을 분류할 클래스
{
    public string craftname; //건물 이름
    public GameObject previewCraft; //미리보기 프리펩
    public GameObject BuildCraft; // 실제 지어질 프리펩
    
}
public class BuildManager : MonoBehaviour
{
    [SerializeField] private Craft[] craft = null;  //직렬화를 통해 인스펙터창에서 관리하기 위한 변수

    private GameObject PreviewPrefab = null;    //Craft를 담을 변수와 미리보기에 사용할 변수 선언
    private GameObject InsPrefab = null;    //생성할 건물

    private bool isActivatePreview = false;
   private Vector3 MousePos;
    //
    private RaycastHit hitinfo;
    private Vector3 _location;

    private Vector3 buildPos;

    private void Awake()
    {
        
        
    }
    private void Update()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //마우스의 현재 위치 받기

        if (Input.GetKeyDown(KeyCode.R)) SlotClick(0,0);
        if (Input.GetKeyDown(KeyCode.W)) SlotClick(1,-1);

        int X;
        int Z;

        if (isActivatePreview)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitinfo))
            {
                if (hitinfo.transform != null)
                {
                    _location = hitinfo.point;
                    _location.y = 0.5f;


                    if ((int)_location.x % 2 == 0) X = (int)_location.x;
                    else X = (int)(_location.x) - 1;


                    if ((int)_location.z % 2 == 0) Z = (int)_location.z;
                    else Z = (int)(_location.z) - 1;

                    //Debug.Log(X + " : " + Z);
                    buildPos = new Vector3(X, 1.0f, Z);
                    PreviewPrefab.transform.position = buildPos;
                }
            }
        }

        if (PreviewPrefab != null && PreviewPrefab.GetComponentInChildren<Preview>().isBuildable())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(InsPrefab, buildPos, Quaternion.identity);
                Destroy(PreviewPrefab);
                isActivatePreview = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && PreviewPrefab != null)
        {
            Destroy(PreviewPrefab);
            isActivatePreview = false;
           
        }
    }


    public void SlotClick(int _SlotNumber, float X)
    {
        Vector3 mousePos = MousePos;
        mousePos.y += X;
        PreviewPrefab = Instantiate(craft[_SlotNumber].previewCraft, mousePos, Quaternion.identity);
        InsPrefab = craft[_SlotNumber].BuildCraft;
       
        isActivatePreview = true;
        //BuildNum = _SlotNumber;
    }

  

  

    



}