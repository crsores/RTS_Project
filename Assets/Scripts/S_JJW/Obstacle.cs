using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{


    private void Start()
    {
        //        Debug.Log(this.transform.position);
        //        Debug.Log(this.transform.localScale);

        StartCoroutine("SetObstacleNode");
        Debug.Log("Dd");
    }

    IEnumerator SetObstacleNode()
    {
        //int Xscale = (int)(this.transform.position.x + this.transform.localScale.x);
        //int Zscale = (int)(this.transform.position.z + this.transform.localScale.z);

        int XPos = (int)this.transform.position.x;
        int ZPos = (int)this.transform.position.z;
        yield return null;
        int X = (int)this.transform.localScale.x;
        int Y = (int)this.transform.localScale.z;

        // �� ������ �ȿ��� �ǹ��� ���� ���� ����� ���� �ɷ��� ���� ���� ������� �Ѵ�.
        int ObstacleRangeX = 0;
        int ObstacleRangeZ = 0;

        if (XPos > 0) ObstacleRangeX = XPos - 1;
        else ObstacleRangeX = XPos;

        if (ZPos > 0) ObstacleRangeZ = XPos - 1;
        else ObstacleRangeZ = ZPos;


        Debug.Log("X : " + X + "Y : " + Y);

        for (int i = -1; i <= X; i++) // �̵� �Ұ� ������ �ǹ����� ���� �� Ŀ�� �Ѵ�.
        {
            for(int j = -1; j <= Y; j++)
            {
                Grid.gridinstance.ObstacleNode(XPos+i, ZPos+j);
                
            }
        }

    }
}
