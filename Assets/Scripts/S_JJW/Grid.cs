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

    Node[,] grid;   //Node의 index번호를 저장할 grid 변수

    private void Awake()
    {
        if (null == gridinstance) gridinstance = this;
        else Destroy(this.gameObject);
        CreateGrid();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //for (int x = 0; x < (int)width; x++)
        //{
        //    Debug.DrawLine(new Vector3(x * cellsize, 0, 0), new Vector3(x * cellsize, 0, width * cellsize), Color.red);
        //}

        //for (int y = 0; y < (int)height; y++)
        //{
        //    Debug.DrawLine(new Vector3(0, 0, y * cellsize), new Vector3(height * cellsize, 0, y * cellsize), Color.red);
        //}
    }

    float LU = 0;
    float LB = 0;
    float RU = 0;
    float RB = 0;

    Vector2 Center;
    public void CreateGrid()
    {
        grid = new Node[(int)width, (int)height];   // grid를 가로 세로 만큼 배열 생성
        Vector3 worldBottomLeft = Vector3.zero - Vector3.right * width / 2 - Vector3.forward * height / 2;

        for (int x = 0; x < (int)width; x++)
        {
            for (int y = 0; y < (int)height; y++)
            {
                RaycastHit hit;

                int layerMask = (-1) - (1 << LayerMask.NameToLayer("Player_Unit"));


                //  Vector3 worldPoint = worldBottomLeft + Vector3.right * (x + 0.5f) + Vector3.forward * (y + 0.5f);

                if (Physics.Raycast(new Vector3(x * cellsize, 10, y * cellsize), Vector3.down, out hit, 100f,layerMask))
                {
                    LB = hit.point.y;
                }
                if (Physics.Raycast(new Vector3(x * cellsize, 10, y * cellsize + cellsize), Vector3.down, out hit, 100f, layerMask))
                {
                    LU = hit.point.y;
                }
                if (Physics.Raycast(new Vector3(x * cellsize + cellsize, 10, y * cellsize), Vector3.down, out hit, 100f, layerMask))
                {
                    RB = hit.point.y;
                }
                if (Physics.Raycast(new Vector3(x * cellsize + cellsize, 10, y * cellsize + cellsize), Vector3.down, out hit, 100f, layerMask))
                {
                    RU = hit.point.y;
                }


                // Debug.Log(LB +" : "+ LU + " : " + RB + " : " + RU);

                //                grid[x, y] = new Node(true, x, y, LB, LU, RB, RU, Center);

                if (JudgeObstacle(LB, LU, RB, RU)) grid[x, y] = new Node(true, x, y, LB, LU, RB, RU);
                else grid[x, y] = new Node(false, x, y, LB, LU, RB, RU);

                if (grid[x, y].YDepthLB <= 0) grid[x, y].walkable = false;

     

                //if (LB == RU&&LB==LU&&LB==RB&&LU==RB&&LU==RU&&RB==RU)
                //{
                //    grid[x, y] = new Node(true, x, y, LB, LU, RB, RU,Center);
                //   // Debug.Log("1");
                //}
                //else
                //{
                //    grid[x, y] = new Node(false, x, y, LB, LU, RB, RU, Center);
                //  //  Debug.Log("2");
                ////    Debug.Log(x + " : " + y);
                //}
                //  //  Debug.Log((LB == LU && LU == RU && RU == RB));

                // Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                // Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
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

        int x = (int)(rayPosition.x / cellsize);
        int z = (int)(rayPosition.z / cellsize);

        return grid[x, z];
    }


    public Vector3[] ReturnPosArr(List<Node> path, float cellsize)
    {
        List<Vector3> waypoints = new List<Vector3>();
        for (int i = 0; i < path.Count; i++)
        {
            float X = path[i].gridX * cellsize + cellsize / 2;
            float Y = path[i].YDepthLB;
            float Z = path[i].gridY * cellsize + cellsize / 2;

            Debug.Log("hihi");

            waypoints.Add(new Vector3(X, Y, Z));
        }
        return waypoints.ToArray();
    }

    public Vector3 ReturnPos(Node node, float Y, float cellsize)
    {
        float X = node.gridX * cellsize;
        float Z = node.gridY * cellsize;




        return new Vector3(X, 0, Z);
    }

    public void ObstacleNode(int x, int y)
    {
        //int x = (int)(Obstacle.x + width / 2);
        //int y = (int)(Obstacle.z + height / 2);
        //grid[x, y].ChangeNode = false;
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
                                                                    // 0    1       1
                                                                    // 1    0   1   0   1
                                                                    // 0   -1       1
                                                                    //-1    0

        bool[] walkableUDLR = new bool[4];  // 이웃 노드의 이동가능 여부를 위한 bool값 배열 준비한다

        //상하좌우의 노드 먼저 계산
        for (int i = 0; i < 4; i++)
        {

            int checkX = node.gridX + temp[i, 0];
            //0,1,0,-1

            int checkY = node.gridY + temp[i, 1];
            //1,0,-1,0

            //0,1
            //1,0
            //0,-1
            //-1,0

            // if node.gridX = 1,1

            //1,2
            //2,1
            //1,0
            //0,1

            if (checkX >= 0 && checkX < (int)width && checkY >= 0 && checkY < (int)height)
            {  //검사 노드가 전체 노드안에 위치해 있는지 검사



                if (grid[checkX, checkY].walkable) walkableUDLR[i] = true; //해당 노드에 장애물이 없어 갈 수 있다면 해당 bool 값을 true로 변경
                                                                           //장애물이 있는 노드라면 false로 추가



                if (Mathf.Abs(node.YDepthLB - grid[checkX, checkY].YDepthLB) > 0.5f)
                {
                    // node.walkable = false;
                    grid[checkX, checkY].walkable = false;
                }





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


                    if (Mathf.Abs(node.YDepthLB - grid[checkX, checkY].YDepthLB) > 1.0f)
                    {
                        // node.walkable = false;
                        grid[checkX, checkY].walkable = false;


                    }

                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
        //이웃으로 넣은 List를 반환
    }

    bool JudgeObstacle(float LB, float LU, float RB, float RL)
    {
        return Mathf.Abs(LB - LU) < 0.3f && Mathf.Abs(RU - LU) < 0.3f && Mathf.Abs(RU - RB) < 0.3f && Mathf.Abs(RB - LB) < 0.3f;
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
            return Grid.gridinstance.NodePoint(obj.transform.position, cellsize);  // 선택한 노드의 x,y 값으로 grid[x,y]를 찾음
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
