using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public Node start; //��ã�⿡�� ������ �� ���
    public Node end;    //��ã�⿡�� ���� �� ���

    private float cellsize;

    [SerializeField] GameObject TargetPos;

    //�� ��ũ��Ʈ���� start�� end�� �� �������� �Ѵ�.
    // ���� start�� end�� pathFinding�� �̷����� �Ѵ�.

    //Grid�� Node���� �޾ƿ� ���� node�� index�� �����;� �Ѵ�.

    Vector3[] Path = null; //��ã�⿡�� ã���� ����� ��ġ�� �����ǰ����� ������ ����
    int targetIndex = 0; // wayPoint�� �ε��� ��

    float speed = 10f; //�̵� �ӵ�

    public bool finding;
    public bool success;    //��ã�Ⱑ �������� Ȯ��

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
    //������ ���� ��ġ�� ��ã���� ���� ��ġ�� ����
    public void SetStartPos()
    {
        if (start != null)
        {
            //��ŸƮ ��尡 �̹� �ִ� ��� ������ ��ŸƮ�� �ʱ�ȭ ��Ų��.
           
            start = null;
        }
        start = CurrentPos(); //���� ���� ������Ʈ�� �ִ� ��ġ�� ���
        
        //��ŸƮ ��带 ���� ������ ��ġ�Ʒ� ���� ����
    }
    public Node CurrentPos()
    {
        //Debug.Log(this.transform.position);
                return Grid.gridinstance.NodePoint(this.transform.position,cellsize);           
    }
    // ================================================================



    // ���콺 ����Ʈ�� ���� ��带 ��ã���� ��ǥ�������� ����
    public void SetTarget()
    {
        //������ �ٸ� end��带 ���ְ� ���콺�� ���� ���� ����_ ���� ���� ����
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
            //if (node != null)   //���̷� �� ���� ���� �ƴϰ�
            //{
            //    if (end != null)    //�̹� ���� ���� ���� ��
            //    {
            //        //end��尡 �̹� ���� ��� �ʱ�ȭ �� end������ �ʱ�ȭ
                   
            //        end = null;
            //    }

            //    end = node;
                

         //   }
        }
    }
    //=================================================================


    // ���콺 ����Ʈ�� ���� �ش� ��尪�� �޾ƿ��� �Լ�
    //public Node RayCast()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ī�޶� �������� ���콺 ��ġ�� Ray�߻�
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit, 100f))
    //    {
    //        GameObject obj = hit.collider.gameObject; //���� hit�� ������ ��ȯ
    //                                                  //  Debug.Log(obj.name);
    //                                                  //  Debug.Log(obj.transform.position);
    //        return Grid.gridinstance.NodePoint(Input.mousePosition,cellsize);  // ������ ����� x,y ������ grid[x,y]�� ã��
    //    }
    //    return null; // ���� collider�� ������ null ��ȯ
    //}

    List<Node> openSet = new List<Node>();      // �̿���带 ������ List
    HashSet<Node> closedSet = new HashSet<Node>(); // �̹� �˻��� ��带 ������ Hash

    // =================================================================
    bool pathSuccess = false;
    public void FindPath()
    {
        //��ã�� �Լ�

        finding = true;

        // List<Node> openSet = new List<Node>();      // �̿���带 ������ List
        // HashSet<Node> closedSet = new HashSet<Node>(); // �̹� �˻��� ��带 ������ Hash

        openSet.Add(start); //Open�� Start������ ��带 ����

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0]; // CurrentNode�� ó�� ������_ ������ ��ġ

            //Open�� fCost�� ���� ���� ��带 ã��
            for (int i = 1; i < openSet.Count; i++)
            { //0�� ���� ����̱� ������ i�� 1���� ����
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    //
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            // ���� ��尡 �������� while�� Ż��
            if (currentNode == end)
            {
                //pathSuccess = true;
                success = true;
                break;
            }

            //yield return new WaitUntil(() => finding);
            // yield return new WaitForSeconds(0.1f);
            //yield return null;



            //�̿� ��带 �˻�
            foreach (Node neighbour in Grid.gridinstance.GetNeighbours(currentNode))
            {
                //�̵��Ұ� ��� �̰ų� �̹� �˻��� ���� ����
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

               
                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour,5);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    
                    neighbour.hCost = GetDistance(neighbour, end,10);   // �̰� �ȵ��� ����
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
           // Debug.Log("���� �̵�");
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

    //�ݺ��Ǵ� �̵��� �������ָ� ��������Ʈ�� �����ϰ� �����.
    Vector3[] SimplifyPath(List<Node> path) // �Ű������� Node�� List�� �޴´�.
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
        //��尣 �Ÿ����
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        Debug.Log(i);

        if (dstX > dstY) return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }


}
