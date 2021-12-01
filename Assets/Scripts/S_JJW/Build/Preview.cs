using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    private List<Collider> colliderList = new List<Collider>(); //충돌검사

    [SerializeField]
    private int layerGround; //그라운드 레이어
    private const int IGNORE_LAYER = 2; //무시할 레이어

    [SerializeField] private Material green;    // 충돌이 없을 때 보여줄 초록색 프리뷰
    [SerializeField] private Material red;  // 충돌 물체가 있을 때 보여줄 빨간색 프리뷰

    void Start()
    {
    }

    void Update()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (colliderList.Count > 0) //충돌 물체가 하나 이상일 때 
        {
        //Debug.Log("업데이트");
            SetColor(red);
        }

        else { SetColor(green); /*Debug.Log("업데이트22222")*/;
        }
    }

    private void SetColor(Material mat)
    {
        Debug.Log("0000");
        foreach (Transform thistransform in transform)
        {
            Debug.Log("1111");
            var newMaterials = new Material[thistransform.GetComponent<Renderer>().materials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                Debug.Log("2222");
                newMaterials[i] = mat;
            }
            thistransform.GetComponent<Renderer>().materials = newMaterials;
        }

    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_LAYER)
        {
            colliderList.Add(other);
            Debug.Log("충돌");
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_LAYER)
        {
            colliderList.Remove(other);
            Debug.Log("안충돌");
        }
    }

    public bool isBuildable()
    {
        return colliderList.Count == 0;
    }
}