using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public GameObject Path_ = null;


    //==============================================

    public Node start; //길찾기에서 시작이 될 노드
    public Node end;    //길찾기에서 끝이 될 노드

    private float cellsize; //cell 크기를 나타낼 변수

    [SerializeField] GameObject TargetPos;
    //마우스 우클릭 시 생성될 오브젝트
    //이 오브젝트의 위치값을 Node의 index로 변환 후 길찾기를 함

    //이 스크립트에서 start와 end가 다 정해져야 한다.
    // 받은 start와 end로 pathFinding이 이뤄져야 한다.

    //Grid와 Node에서 받아올 것은 node의 index만 가져와야 한다.

    Vector3[] Path = null; //길찾기에서 찾아진 노드의 index를 가지고 위치값으로 저장할 배열

    int targetIndex = 0; // wayPoint의 인덱스 값

    float speed = 10f; //이동 속도

    public bool finding;
    public bool success;    //길찾기가 끝났는지 확인

    private Rigidbody Rb = null;
    Vector3 thisPos = Vector3.zero;



    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        cellsize = Grid.gridinstance.Getcellsize;   //Grid에서 설정한 cellsize저장
         
    }

    private void Update()
    {
        SetTarget();
        //thisPos = this.transform.position;

        //    thisPos.y = Grid.gridinstance.NodePoint(this.transform.position, cellsize).YDepthLB + 1.0f;
        //    this.transform.position = thisPos;


        //====================================================

        //    Vector3 YPOs = this.transform.position;
        //RaycastHit hit;
        //if(Physics.Raycast(this.transform.position,Vector3.forward*0.5f,out hit, 1.0f))
        //{
        //    YPOs.y = hit.point.y;
        //    Debug.DrawLine(this.transform.position, Vector3.forward, Color.red);
        //}
        //    this.transform.position = YPOs;


    }

    //public void OnPathFound()
    //{
    //    StopCoroutine("FollowPath");
    //    StartCoroutine("FollowPath");
    //}

    //IEnumerator FollowPath()
    //{
    //    Vector3 currentWaypoint = Path[0];  //currenWaypoint를 path배열로 선언
    //    while (true)
    //    {
    //        if (transform.position == currentWaypoint)  //현재 위치가 currentWaypoint와 같을 때
    //        {
    //            targetIndex++;  
    //            if (targetIndex >= Path.Length) yield break;
    //        }
    //        currentWaypoint = Path[targetIndex];    // path배열의 index번호 증가
    //    }
    //    this.transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
    //    yield return null;
    //}


    // ==========================================================
    //유닛의 현재 위치를 길찾기의 시작 위치로 설정
    public void SetStartPos()
    {
        if (start != null)
        {
            //스타트 노드가 이미 있는 경우 기존의 스타트를 초기화 시킨다.

            start = null;
        }
        start = CurrentPos(); //현재 게임 오브젝트가 있는 위치의 노드

        //스타트 노드를 현재 유닛의 위치아래 노드로 변경
    }
    public Node CurrentPos()
    {
        //Debug.Log(this.transform.position);
        return Grid.gridinstance.NodePoint(this.transform.position, cellsize);

    }
    // ================================================================





    // 마우스 포인트로 찍은 노드를 길찾기의 목표지점으로 설정
    public void SetTarget()
    {
        if (Input.GetMouseButtonDown(1))
        {
            openSet.Clear();
            closedSet.Clear();
            SetStartPos();
            //마우스 우클릭 시 새로운 길찾기를 위해 Open과 close를 리셋, 시작 위치도 현재 게임 오브젝트의 위치로 초기화

            //===========================================================

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Vector3 hitPos = hit.point;
                GameObject item = Instantiate(TargetPos, hit.point, Quaternion.identity);
                //마우스에서 Ray를 쏴서 맞은 곳에 오브젝트를 생성
                //목적지를 시각화 하기 위한 오브젝트

                if (end != null) end = null;
                //end값에 이미 어떤 값이 들어가있다면 초기화

                end = Grid.gridinstance.NodePoint(hitPos, cellsize);
                //end에 hitPos에 대응하는 NodeIndex를 대입
            }

            FindPath();
            //start와 목적지가 정해졌으면 FindPath함수 실행
        }
    }

    List<Node> openSet = new List<Node>();      // 이웃노드를 저장할 List
    HashSet<Node> closedSet = new HashSet<Node>(); // 이미 검사한 노드를 저장할 Hash

    // =================================================================

    bool pathSuccess = false;

    public void FindPath()
    {
        //길찾기 함수
        finding = true;

        openSet.Add(start); //start를 첫 열린 목록에 추가


        if (openSet.Count > 0)
        {
            while (openSet.Count > 0)   //열린 목록에 인자가 없을 때 까지 반복
            {
                Node currentNode = openSet[0]; // CurrentNode은 처음 노드부터_ 유닛의 위치

                //Open에 fCost가 가장 작은 노드를 찾기
                for (int i = 1; i < openSet.Count; i++)
                { //0은 시작 노드이기 때문에 i는 1부터 시작
                    if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                // 현재 노드가 목적지면 while문 탈출
                if (currentNode == end)
                {
                    //pathSuccess = true;
                    success = true;
                    break;
                }





                //이웃 노드를 검색
                foreach (Node neighbour in Grid.gridinstance.GetNeighbours(currentNode))
                {
                    //이동불가 노드 이거나 이미 검색한 노드는 제외
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }


                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;

                        neighbour.hCost = GetDistance(neighbour, end);

                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);

                        }
                    }
                }
            }
        }

        

        targetIndex = 0;
        StopCoroutine("MoveUnit");
        StartCoroutine("MoveUnit");
    }

    float closeDistance = 1.0f;

  
    
    //==============================================================================

    //Vector3[] RetracePath(Node startNode, Node endNode)
    //{
    //    //Debug.Log("ggggg");
    //    List<Node> path = new List<Node>();
    //    Node currentNode = endNode;

    //    while (currentNode != startNode)
    //    {
    //        path.Add(currentNode);
    //        currentNode = currentNode.parent;
    //    }
    //    Vector3[] waypoints = SimplifyPath(path);
    //    Array.Reverse(waypoints);
    //    //Debug.Log(waypoints);
    //    return waypoints;
    //}


    ////반복되는 이동을 삭제해주며 웨이포인트를 간단하게 만든다.
    //Vector3[] SimplifyPath(List<Node> path) // 매개변수로 Node형 List를 받는다.
    //{
    //    List<Vector3> waypoints = new List<Vector3>();
    //    Vector2 directionOld = Vector2.zero;

    //    for (int i = 1; i < path.Count; i++)
    //    {
    //        Vector2 directionNew = new Vector2((path[i - 1].gridX - path[i].gridX) / 0.25f, (path[i - 1].gridY - path[i].gridY) / 0.25f);
    //        if (directionNew != directionOld)
    //        {
    //            waypoints.Add(path[i - 1].GetPos(this.transform.position.y, cellsize));
    //        }
    //        directionOld = directionNew;
    //    }
    //    waypoints.Add(start.GetPos(this.transform.position.y, cellsize));
    //    //  Debug.Log(waypoints[1]);
    //    return waypoints.ToArray();
    //}

    //=========================================================================================


    Vector3[] RetracePath2(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = Grid.gridinstance.ReturnPosArr(path, cellsize);
        Array.Reverse(waypoints);
        return waypoints;
    }




    int GetDistance(Node nodeA, Node nodeB)
    {
        //노드간 거리계산
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        //Debug.Log(i);

        if (dstX > dstY) return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }


    IEnumerator MoveUnit()
    {
        // Debug.Log("sss");
        if (success)
        {
            //   Debug.Log("sadasdsdasad");
            // Debug.Log("유닛 이동");
            Vector3[] Path = RetracePath2(start, end);   //start와 end사이의 이동 포인트를 저장하는 배열 path

            for (int i = 0; i < Path.Length; i++)
            {
                Vector3 NodePos = Path[i];
                NodePos.y += 0.5f;


               // GameObject DD = Instantiate(Path_, NodePos, Quaternion.Euler(90, 0, 0));
               
            }


            Vector3 currentWaypoint = this.transform.position;

            while (true)
            {
                if (targetIndex < Path.Length)
                {
                    //Debug.Log("TargetIndex : " + targetIndex);
                    currentWaypoint = Path[targetIndex];

                    float AxisX = currentWaypoint.x - transform.position.x;
                    float AxisY = currentWaypoint.z - transform.position.z;
                    Vector3 Movedis = new Vector3(AxisX, 0, AxisY);

                    float SqrLen = Movedis.sqrMagnitude;
                    float SqrLen2 = Movedis.magnitude;

                    if (/*transform.position.x == currentWaypoint.x && transform.position.z == currentWaypoint.z)*/
                        SqrLen < 1.0f)
                    {
                        targetIndex++;
                        //Debug.Log(targetIndex);
                    }
           
                    this.transform.LookAt(new Vector3(currentWaypoint.x, this.transform.position.y, currentWaypoint.z));
                    ////  

                    this.transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

                    thisPos = this.transform.position;
                    thisPos.y = Grid.gridinstance.NodePoint(currentWaypoint, cellsize).YDepthLB + 0.6f;
                    this.transform.position = thisPos;

                    yield return null;

                }
                else yield break;

            }

        }

    }
}
