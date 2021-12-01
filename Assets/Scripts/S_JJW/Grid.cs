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
        //���̿� ���� ���� node�� ã�� ��ȯ�ϱ� ���� �Լ�
        //int x = (int)(rayPosition.x-1);     //index��ȣ��...?
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

    public List<Node> GetNeighbours(Node node)  //������Ƽ�� �������� ������ ���� �� �̵� ������ ��带 ��ȯ
    {
        List<Node> neighbours = new List<Node>();
        int[,] temp = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } }; //���� ��带 �������� ���� �¿�
                                                                    // 0    1
                                                                    // 1    0
                                                                    // 0   -1
                                                                    //-1    0
        bool[] walkableUDLR = new bool[4];  // �̵����������� ���ε� �����¿�� 4���� �غ��Ѵ�

        //�����¿��� ��� ���� ���
        for (int i = 0; i < 4; i++)
        {
            int checkX = node.gridX + temp[i, 0];
            int checkY = node.gridY + temp[i, 1];
            if (checkX >= 0 && checkX < (int)width && checkY >= 0 && checkY < (int)height)
            {  //�˻� ��尡 ��ü ���ȿ� ��ġ�� �ִ��� �˻�

                if (grid[checkX, checkY].walkable) walkableUDLR[i] = true; //�ش� ��忡 ��ֹ��� ���� �� �� �ִٸ� �ش� bool ���� true�� ����
                //��ֹ��� �ִ� ����� false�� �߰�

                neighbours.Add(grid[checkX, checkY]);   //�̿� ��忡 �߰�
            }
        }

        //�밢���� ��带 ���
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
        //�̿����� ���� List�� ��ȯ
    }


    public void SetObstacle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Node node = RayCast();
            if (node != null) //���콺 ����Ʈ�� ���� ���� ���� �ƴ϶�� return
            {
                if (node.start || node.end)      // Ray�� ���� ��尡 ����, ������ �Ǵ� ���������̰ų� �� �����̸� ����/���� ��ġ�� ������ �� �ִ�.
                    StartCoroutine("SwitchStartEnd", node);
                else
                    StartCoroutine("ChangeWalkable", node); //�����̳� ���� �ƴ϶�� ��ֹ��� ����
            }
            return;
        }

    }

    public Node RayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ī�޶� �������� ���콺 ��ġ�� Ray�߻�
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            GameObject obj = hit.collider.gameObject; //���� hit�� ������ ��ȯ
                                                      //  Debug.Log(obj.name);
                                                      //  Debug.Log(obj.transform.position);
            return Grid.gridinstance.NodePoint(obj.transform.position,cellsize);  // ������ ����� x,y ������ grid[x,y]�� ã��
        }
        return null; // ���� collider�� ������ null ��ȯ
    }


    IEnumerator SwitchStartEnd(Node node)   // �巡�׷� ��ŸƮ ���� ��ġ ����
    {
        //node = ���콺 ����Ʈ�� ���� node
        bool start = node.start;    // �� ��尡 start���� 
        Node nodeOld = node;    //noldOld�� ���콺 ����Ʈ�� ���� node�� ����

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
        bool walkable = !node.walkable; // ���� �Ұ��� �ݴ�� ��ȯ

        while (Input.GetMouseButton(0)) //���콺 ��ư�� ������ ���� ��� ����
        {
            node = RayCast();
            if (node != null && !node.start && !node.end) //�ش� ��尡 �־�� �ϰ�, ���۰� ������ �ƴ� �� ����
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
