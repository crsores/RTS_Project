using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public Node start; //길찾기에서 시작이 될 노드
    public Node end;    //길찾기에서 끝이 될 노드

    private float cellsize;

    [SerializeField] GameObject TargetPos;

    //이 스크립트에서 start와 end가 다 정해져야 한다.
    // 받은 start와 end로 pathFinding이 이뤄져야 한다.

    //Grid와 Node에서 받아올 것은 node의 index만 가져와야 한다.

    Vector3[] Path = null; //길찾기에서 찾아진 노드의 위치를 포지션값으로 저장할 변수
    int targetIndex = 0; // wayPoint의 인덱스 값

    float speed = 10f; //이동 속도

    public bool finding;
    public bool success;    //길찾기가 끝났는지 확인

    Rigidbody Rb;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        cellsize = Grid.gridinstance.Getcellsize;
    }

    private void Update()
    {
        
        SetTarget();
    }

    public void OnPathFound()
    {
        StopCoroutine("FollowPath");
        StartCoroutine("FollowPath");
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = Path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= Path.Length) yield break;
            }
            currentWaypoint = Path[targetIndex];
        }
        this.transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
        yield return null;
    }


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
                return Grid.gridinstance.NodePoint(this.transform.position,cellsize);           
    }
    // ================================================================



    // 마우스 포인트로 찍은 노드를 길찾기의 목표지점으로 설정
    public void SetTarget()
    {
        //기존의 다른 end노드를 없애고 마우스로 찍은 노드로 변경_ 아직 구현 안함
        if (Input.GetMouseButtonDown(1))
        {
                openSet.Clear();
                closedSet.Clear();
            SetStartPos();

            //===========================================================

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Vector3 hitPos = hit.point;
                GameObject item = Instantiate(TargetPos);
                item.transform.position = hitPos;
                Debug.Log(hitPos);

                if (end != null) end = null;

             end = Grid.gridinstance.NodePoint(hitPos,cellsize);
                Debug.Log(end.gridX + " : " + end.gridY);
            }


            //===========================================================

                FindPath();
           /// StopCoroutine("FindPath");
           // StopCoroutine("MoveUnit");
           // Node node = RayCast();
           // Node oldnode = end;
            //if (node != null)   //레이로 쏜 곳이 널이 아니고
            //{
            //    if (end != null)    //이미 찍은 곳이 있을 때
            //    {
            //        //end노드가 이미 있을 경우 초기화 후 end지점을 초기화
                   
            //        end = null;
            //    }

            //    end = node;
                

         //   }
        }
    }
    //=================================================================


    // 마우스 포인트에 맞은 해당 노드값을 받아오는 함수
    //public Node RayCast()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //카메라를 기준으로 마우스 위치로 Ray발사
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit, 100f))
    //    {
    //        GameObject obj = hit.collider.gameObject; //맞은 hit의 정보를 반환
    //                                                  //  Debug.Log(obj.name);
    //                                                  //  Debug.Log(obj.transform.position);
    //        return Grid.gridinstance.NodePoint(Input.mousePosition,cellsize);  // 선택한 노드의 x,y 값으로 grid[x,y]를 찾음
    //    }
    //    return null; // 맞은 collider가 없으면 null 반환
    //}

    List<Node> openSet = new List<Node>();      // 이웃노드를 저장할 List
    HashSet<Node> closedSet = new HashSet<Node>(); // 이미 검사한 노드를 저장할 Hash

    // =================================================================
    bool pathSuccess = false;
    public void FindPath()
    {
        //길찾기 함수

        finding = true;

        // List<Node> openSet = new List<Node>();      // 이웃노드를 저장할 List
        // HashSet<Node> closedSet = new HashSet<Node>(); // 이미 검사한 노드를 저장할 Hash

        openSet.Add(start); //Open은 Start지점의 노드를 저장

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0]; // CurrentNode은 처음 노드부터_ 유닛의 위치

            //Open에 fCost가 가장 작은 노드를 찾기
            for (int i = 1; i < openSet.Count; i++)
            { //0은 시작 노드이기 때문에 i는 1부터 시작
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    //
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

            //yield return new WaitUntil(() => finding);
            // yield return new WaitForSeconds(0.1f);
            //yield return null;



            //이웃 노드를 검색
            foreach (Node neighbour in Grid.gridinstance.GetNeighbours(currentNode))
            {
                //이동불가 노드 이거나 이미 검색한 노드는 제외
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

               
                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour,5);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    
                    neighbour.hCost = GetDistance(neighbour, end,10);   // 이거 안들어가고 있음
                    Debug.Log(neighbour.hCost);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
            
                    }
                }
            }
        }

        targetIndex = 0;
        StopCoroutine("MoveUnit");
        StartCoroutine("MoveUnit");


    }

    float closeDistance = 1.0f;

    IEnumerator MoveUnit()
    {
      //  Debug.Log("sss");
        if (success)
        {
           // Debug.Log("유닛 이동");
            Vector3[] Path = RetracePath(start, end);


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

                    if (transform.position.x == currentWaypoint.x && transform.position.z == currentWaypoint.z)
                        //SqrLen < closeDistance * closeDistance)
                    {
                        targetIndex++;
                    }
                    //currentWaypoint.y += 0.5f;
                    Vector3 Dir = (currentWaypoint - this.transform.position).normalized;
                    ////  
                    this.transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                    
                    //Rb.velocity = Dir * 500 * Time.deltaTime;
                    yield return null;
                   // Debug.Log("Path.Lengh : " + Path.Length);
                }
                else yield break;
                
            }

        }

    }

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        //Debug.Log("ggggg");
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        //Debug.Log(waypoints);
        return waypoints;
    }

    //반복되는 이동을 삭제해주며 웨이포인트를 간단하게 만든다.
    Vector3[] SimplifyPath(List<Node> path) // 매개변수로 Node형 List를 받는다.
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i - 1].GetPos(this.transform.position.y, cellsize) /*+ Vector3.up * 0.1f*/);
              
            }
            directionOld = directionNew;
        }
        waypoints.Add(start.GetPos(this.transform.position.y, cellsize)/* + Vector3.up * 0.1f*/);
      //  Debug.Log(waypoints[1]);
        return waypoints.ToArray();
    }

    int GetDistance(Node nodeA, Node nodeB, int i)
    {
        //노드간 거리계산
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        Debug.Log(i);

        if (dstX > dstY) return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }


}
