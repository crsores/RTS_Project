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

        // 맵 사이즈 안에서 건물을 지을 때와 사이즈에 끝에 걸려서 지을 때를 나누어야 한다.
        int ObstacleRangeX = 0;
        int ObstacleRangeZ = 0;

        if (XPos > 0) ObstacleRangeX = XPos - 1;
        else ObstacleRangeX = XPos;

        if (ZPos > 0) ObstacleRangeZ = XPos - 1;
        else ObstacleRangeZ = ZPos;


        Debug.Log("X : " + X + "Y : " + Y);

        for (int i = -1; i <= X; i++) // 이동 불가 지역은 건물보다 조금 더 커야 한다.
        {
            for(int j = -1; j <= Y; j++)
            {
                Grid.gridinstance.ObstacleNode(XPos+i, ZPos+j);
                
            }
        }

    }
}
