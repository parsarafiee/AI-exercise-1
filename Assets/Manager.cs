using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Manager : MonoBehaviour
{
    public Vector3Int startPosition;
    public Vector3Int endPosition;

    public Tilemap white;
    public Tile wallsTile;
    public Tile whiteTile;
    public Tile endTile;
    public Tile pathFindingTile;


    public float timeToAdd = 1;


    float timer;
    bool started;
    bool canSearch;

    int G = 0;
    int N = 0;

    List<Node> openList;
    List<Node> closeList;
    List<Node> finalList;

    private void Start()
    {
        timer = timeToAdd;
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

            Node firstTile = new Node(0, H_Calculatior(startPosition, endPosition), F_Calculatior(H_Calculatior(startPosition, endPosition), G), startPosition);
            closeList.Add(firstTile);

            Debug.Log(startPosition);
            started = true;
            Debug.Log(startPosition);
            // Debug.Log(endPosition);
            AStar(firstTile, endPosition);
            

        }
        if (canSearch)
        {

            FindTheFinalPath(endPosition);
        }

    }
    public void FindTheFinalPath(Vector3Int _endPosition)
    {
        bool done;
        List<Node> finalNode = new List<Node>();
        if (closeList.Count > 0)
        {
            for (int i = 0; i < closeList.Count; i++)
            {
                finalNode = FindALltheNeighbour(_endPosition);
            }
            Node min = finalNode[0];
            foreach (var item in finalNode)
            {
                if (item.GetG() < min.GetG())
                {
                    min = item;
                }
            }
            if (min.GetPos() == startPosition)
            {
                return;
            }

            white.SetTile(min.GetPos(), endTile);

            FindTheFinalPath(min.GetPos());

        }






    }
    public List<Node> FindALltheNeighbour(Vector3Int currentPositon)
    {
        List<Node> nodes = new List<Node>();
        Vector3Int right = new Vector3Int(1, 0, 0);
        Vector3Int left = new Vector3Int(-1, 0, 0);
        Vector3Int up = new Vector3Int(0, 1, 0);
        Vector3Int down = new Vector3Int(0, -1, 0);

        TileBase rightTile = white.GetTile(currentPositon + right);
        TileBase leftTile = white.GetTile(currentPositon + left);
        TileBase upTile = white.GetTile(currentPositon + up);
        TileBase downTile = white.GetTile(currentPositon + down);

        for (int i = 0; i < closeList.Count; i++)
        {
            if (closeList[i].GetPos() == (currentPositon + right) || closeList[i].GetPos() == (currentPositon + left) || closeList[i].GetPos() == (currentPositon + up) || closeList[i].GetPos() == (currentPositon + down))
            {
                nodes.Add(closeList[i]);
            }
        }

        return nodes;
    }
    public void AStar(Node currentNode, Vector3Int endPosition)
    {

        bool reachThePoint = false;
        G= currentNode.G;
        Vector3Int right = new Vector3Int(1, 0, 0);
        Vector3Int left = new Vector3Int(-1, 0, 0);
        Vector3Int up = new Vector3Int(0, 1, 0);
        Vector3Int down = new Vector3Int(0, -1, 0);

        TileBase rightTile = white.GetTile(currentNode.GetPos() + right);
        TileBase leftTile = white.GetTile(currentNode.GetPos() + left);
        TileBase upTile = white.GetTile(currentNode.GetPos() + up);
        TileBase downTile = white.GetTile(currentNode.GetPos() + down);



        if (rightTile) // check if tile is not null 
        {

            if (rightTile.name == endTile.name)
            {
                canSearch = true;
                reachThePoint = true;
                return;

            }
            if (rightTile.name != wallsTile.name && rightTile.name == whiteTile.name) // check if its not wall and its white wall
            {
                bool find = false;
                Node nextOpenRight = new Node(G, H_Calculatior(currentNode.GetPos() + right, endPosition), F_Calculatior(H_Calculatior(currentNode.GetPos() + right, endPosition), G), currentNode.GetPos() + right);
                for (int i = 0; i < closeList.Count; i++)
                {

                    if (closeList[i].GetPos() == nextOpenRight.GetPos())
                    {
                        find = true;
                        break;

                    }


                }
                for (int i = 0; i < openList.Count; i++)
                {
                    if (openList[i].GetPos() == nextOpenRight.GetPos())
                    {
                        find = true;
                        break;
                    }
                }
                if (!find)
                {
                    openList.Add(nextOpenRight);

                }

            }

        }

        if (leftTile)
        {

            if (leftTile.name == endTile.name)
            {
                canSearch = true;
                reachThePoint = true;
                return;

            }
            if (leftTile.name != wallsTile.name && leftTile.name == whiteTile.name)
            {
                bool find = false;
                Node nextOpenLeft = new Node(G, H_Calculatior(currentNode.GetPos() + left, endPosition), F_Calculatior(H_Calculatior(currentNode.GetPos() + left, endPosition), G), currentNode.GetPos() + left);
                for (int i = 0; i < closeList.Count; i++)
                {
                    if (closeList[i].GetPos() == nextOpenLeft.GetPos())
                    {
                        find = true;
                        break;
                    }
                }
                for (int i = 0; i < openList.Count; i++)
                {
                    if (openList[i].GetPos() == nextOpenLeft.GetPos())
                    {
                        find = true;
                        break;
                    }
                }
                if (!find)
                {
                    openList.Add(nextOpenLeft);

                }
            }
        }
        if (upTile)
        {
            if (upTile.name == endTile.name)
            {
                canSearch = true;
                reachThePoint = true;
                return;

            }
            if (upTile.name != wallsTile.name && upTile.name == whiteTile.name)
            {
                bool find = false;
                Node nextOpenUp = new Node(G, H_Calculatior(currentNode.GetPos() + up, endPosition), F_Calculatior(H_Calculatior(currentNode.GetPos() + up, endPosition), G), currentNode.GetPos() + up);
                for (int i = 0; i < closeList.Count; i++)
                {


                    if (closeList[i].GetPos() == nextOpenUp.GetPos())
                    {
                        find = true;
                        break;

                    }


                }
                for (int i = 0; i < openList.Count; i++)
                {
                    if (openList[i].GetPos() == nextOpenUp.GetPos())
                    {
                        find = true;
                        break;
                    }
                }
                if (!find)
                {
                    openList.Add(nextOpenUp);

                }

            }

        }
        if (downTile)
        {
            if (downTile.name == endTile.name)
            {

                reachThePoint = true;
                return;

            }
            if (downTile.name != wallsTile.name && downTile.name == whiteTile.name)
            {
                bool find = false;
                Node nextOpenDown = new Node(G, H_Calculatior(currentNode.GetPos() + down, endPosition), F_Calculatior(H_Calculatior(currentNode.GetPos() + down, endPosition), G), currentNode.GetPos() + down);
                for (int i = 0; i < closeList.Count; i++)
                {

                    if (closeList[i].GetPos() == nextOpenDown.GetPos())
                    {
                        find = true;
                        break;

                    }



                }
                for (int i = 0; i < openList.Count; i++)
                {
                    if (openList[i].GetPos() == nextOpenDown.GetPos())
                    {
                        find = true;
                        break;
                    }
                }
                if (!find)
                {
                    openList.Add(nextOpenDown);

                }


            }

        }
        for (int i = 0; i < closeList.Count; i++)
        {
            white.SetTile(closeList[i].GetPos(), pathFindingTile);

        }
        int[] array = new int[openList.Count];
        if (openList.Count != 0)
        {
            for (int i = 0; i < openList.Count; i++)
            {
                array[i] = openList[i].GetF();

            }

        }
        Node posForNextAstar = null;
        for (int i = 0; i < openList.Count; i++)
        {
            if (Mathf.Min(array) == openList[i].GetF())
            {
                posForNextAstar = openList[i];
                closeList.Add(openList[i]);
                white.SetTile(openList[i].GetPos(), null);
                openList.Remove(openList[i]);

                break;
            }


        }

        if (!reachThePoint)
        {
            StartCoroutine(Wait(0.1f, posForNextAstar));


        }
        else
        {
            Debug.Log("ali");
            
            return;
        }





    }
    IEnumerator Wait(float waitTime, Node posForNextAstar)
    {
        yield return new WaitForSeconds(waitTime);
        AStar(posForNextAstar, endPosition);
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
        public int G;
        public int H;
        public int F;
        public Vector3Int pos;
        public int N;
        public Node(int g, int h, int f, Vector3Int _pos)
        {
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
