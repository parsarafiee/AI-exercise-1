using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ManagerAstar : MonoBehaviour
{
    public Vector3Int startPosition;
    public Vector3Int endPosition;

    List<Node> openList;
    List<Node> closeList;
    List<Node> finalList;


    public Tilemap white;
    public Tile wallsTile;
    public Tile whiteTile;
    public Tile endTile;
    public Tile closeTile;
    public Tile openTile;
    bool started;

    // Start is called before the first frame update
    void Start()
    {
        openList = new List<Node>();
        closeList = new List<Node>();
        finalList = new List<Node>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !started)
        {
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPosition = white.WorldToCell(mp);

            Debug.Log(white.GetTile(startPosition).name);
            white.SetTile(startPosition, null);

            Node firstTile = new Node(0, H_Calculatior(startPosition, endPosition), F_Calculatior(H_Calculatior(startPosition, endPosition), 0), startPosition);
            closeList.Add(firstTile);

            Debug.Log(startPosition);
            started = true;
            Debug.Log(startPosition);
            // Debug.Log(endPosition);
            AStar(firstTile, endPosition);


        }

    }
    public void AStar(Node currentNode, Vector3Int endPosition)
    {

    }

        public int H_Calculatior(Vector3Int tilePostion, Vector3Int endPosition)
    {
        return Mathf.Abs((tilePostion.x - endPosition.x)) + Mathf.Abs((tilePostion.y - endPosition.y));
    }
    public int F_Calculatior(int h, int g)
    {
        return h + g;
    }
    public class Node
    {
        public Node P;
        public int G;
        public int H;
        public int F;
        public Vector3Int pos;
        public int N;
        public Node(Node p,int g, int h, int f, Vector3Int _pos)
        {
            P = p;
            G = g;
            H = h;
            F = f;
            pos = _pos;

        }
        public int GetF()
        {
            return F;
        }
        public Vector3Int GetPos()
        {
            return pos;
        }
        public int GetG()
        {
            return G;
        }

    }
}
