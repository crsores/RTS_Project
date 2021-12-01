using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid gridinstance = null;

    public Node startnode;
    public Node endnode;
    public int width;
    public int height;
    public float cellsize;

    Node[,] grid;

    private void Awake()
    {
        if (null == gridinstance) gridinstance = this;
        else Destroy(this.gameObject);
    }
    void Start()
    {
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateGrid()
    {
        grid = new Node[(int)width, (int)height];
        Vector3 worldBottomLeft = Vector3.zero - Vector3.right * width / 2 - Vector3.forward * height / 2;
        for(int x = 0; x < (int)width; x++)
        {
            for(int y = 0; y < (int)height; y++)
            {
                Vector3 worldPoint = worldBottomLeft+Vector3.right*(x+0.5f)+Vector3.forward*(y+0.5f);
                grid[x, y] = new Node(true, x, y,this.transform);
                //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                //Debug.Log(GetWorldPosition(x,y));
            }
        }
        //Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width , height), Color.white, 100f);
        //Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellsize;
    }

    public Node NodePoint(Vector3 rayPosition, float cellsize)
    {
        //레이에 맞은 곳의 node를 찾아 반환하기 위한 함수
        //int x = (int)(rayPosition.x-1);     //index번호가...?
        //int y = (int)(rayPosition.z);
        //Debug.Log("X : " + x + " Z : " + y);

        int x = (int)(rayPosition.x / cellsize);
        int z = (int)(rayPosition.z / cellsize);

      //  Debug.Log(x + " : " + z);

        return grid[x, z];
    }
    
    public Vector3 ReturnPos(Node node,float cellsize)
    {
        float X = node.gridX*cellsize;
        float Z = node.gridY*cellsize;
        return new Vector3(X, 0, Z);
    }

    public void ObstacleNode(int x, int y)
    {
        //int x = (int)(Obstacle.x + width / 2);
        //int y = (int)(Obstacle.z + height / 2);
        grid[x, y].ChangeNode = false;
    }

    public void ExitObstacleNode(Vector3 Obstacle)
    {
        int x = (int)(Obstacle.x + width / 2);
        int y = (int)(Obstacle.z + height / 2);
        grid[x, y].ChangeNode = true;
    }

    public List<Node> GetNeighbours(Node node)  //프로퍼티를 기준으로 주위의 노드들 중 이동 가능한 노드를 반환
    {
        List<Node> neighbours = new List<Node>();
        int[,] temp = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } }; //현재 노드를 기준으로 상하 좌우
                                                                    // 0    1
                                                                    // 1    0
                                                                    // 0   -1
                                                                    //-1    0
        bool[] walkableUDLR = new bool[4];  // 이동가능한지의 여부도 상하좌우로 4개씩 준비한다

        //상하좌우의 노드 먼저 계산
        for (int i = 0; i < 4; i++)
        {
            int checkX = node.gridX + temp[i, 0];
            int checkY = node.gridY + temp[i, 1];
            if (checkX >= 0 && checkX < (int)width && checkY >= 0 && checkY < (int)height)
            {  //검사 노드가 전체 노드안에 위치해 있는지 검사

                if (grid[checkX, checkY].walkable) walkableUDLR[i] = true; //해당 노드에 장애물이 없어 갈 수 있다면 해당 bool 값을 true로 변경
                //장애물이 있는 노드라면 false로 추가

                neighbours.Add(grid[checkX, checkY]);   //이웃 노드에 추가
            }
        }

        //대각선의 노드를 계산
        for (int i = 0; i < 4; i++)
        {
            if (walkableUDLR[i] || walkableUDLR[(i + 1) % 4])
            {
                int checkX = node.gridX + temp[i, 0] + temp[(i + 1) % 4, 0];
                int checkY = node.gridY + temp[i, 1] + temp[(i + 1) % 4, 1];
                if (checkX >= 0 && checkX < (int)width && checkY >= 0 && checkY < (int)height)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
        //이웃으로 넣은 List를 반환
    }


    public void SetObstacle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Node node = RayCast();
            if (node != null) //마우스 포인트에 닿은 것이 널이 아니라면 return
            {
                if (node.start || node.end)      // Ray에 맞은 노드가 시작, 끝인지 판단 시작지점이거나 끝 지점이면 시작/끝의 위치를 변경할 수 있다.
                    StartCoroutine("SwitchStartEnd", node);
                else
                    StartCoroutine("ChangeWalkable", node); //시작이나 끝이 아니라면 장애물로 변경
            }
            return;
        }

    }

    public Node RayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //카메라를 기준으로 마우스 위치로 Ray발사
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            GameObject obj = hit.collider.gameObject; //맞은 hit의 정보를 반환
                                                      //  Debug.Log(obj.name);
                                                      //  Debug.Log(obj.transform.position);
            return Grid.gridinstance.NodePoint(obj.transform.position,cellsize);  // 선택한 노드의 x,y 값으로 grid[x,y]를 찾음
        }
        return null; // 맞은 collider가 없으면 null 반환
    }


    IEnumerator SwitchStartEnd(Node node)   // 드래그로 스타트 엔드 위치 변경
    {
        //node = 마우스 포인트에 맞은 node
        bool start = node.start;    // 그 노드가 start인지 
        Node nodeOld = node;    //noldOld에 마우스 포인트에 맞은 node값 대입

        while (Input.GetMouseButton(0))
        {
            node = RayCast();
            if (node != null && node != nodeOld)
            {
                if (start && !node.end)
                {
                    
                    startnode = node;
                   
                    nodeOld = node;
                }
                else if (!start && !node.start)
                {
                     endnode = node;
                     nodeOld = node;
                }
            }
            yield return null;
        }
    }

    IEnumerator ChangeWalkable(Node node)
    {
        bool walkable = !node.walkable; // 현재 불값을 반대로 변환

        while (Input.GetMouseButton(0)) //마우스 버튼을 누르는 동안 계속 실행
        {
            node = RayCast();
            if (node != null && !node.start && !node.end) //해당 노드가 있어야 하고, 시작과 끝점이 아닐 때 실행
            {
                node.ChangeNode = walkable;
            }
            yield return null;
        }
    }

    public float Getcellsize
    {
        get
        {
            return cellsize;
        }
    }

}
