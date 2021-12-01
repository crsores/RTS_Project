using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    private List<Collider> colliderList = new List<Collider>(); //�浹�˻�

    [SerializeField]
    private int layerGround; //�׶��� ���̾�
    private const int IGNORE_LAYER = 2; //������ ���̾�

    [SerializeField] private Material green;    // �浹�� ���� �� ������ �ʷϻ� ������
    [SerializeField] private Material red;  // �浹 ��ü�� ���� �� ������ ������ ������

    void Start()
    {
    }

    void Update()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (colliderList.Count > 0) //�浹 ��ü�� �ϳ� �̻��� �� 
        {
        //Debug.Log("������Ʈ");
            SetColor(red);
        }

        else { SetColor(green); /*Debug.Log("������Ʈ22222")*/;
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
            Debug.Log("�浹");
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_LAYER)
        {
            colliderList.Remove(other);
            Debug.Log("���浹");
        }
    }

    public bool isBuildable()
    {
        return colliderList.Count == 0;
    }
}