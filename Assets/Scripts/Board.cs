using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public GameObject prefabTile;
    public GameObject prefabPeg;
    public Tile[,] tiles;
    public Peg[,] pegs;
    private char[,] grid;
    private  int row;
    private  int col;
    
    public  static int boardChoice =3;
    


    
    
    //Start is called before the first frame update
    private void Awake()
    {
        
        AssignBoardGrid();
        Initilaize();
        CreateBoardTiles();
        CreatePegs();
    }

   
    /* Initiliaze tiles and pegs of board*/
    private void Initilaize()
    {
        tiles = new Tile[row, col];
        pegs = new Peg[row, col];
    }
    
    /* Assign every board*/
    private void AssignBoardGrid()
    {
        GameObject gameObject = GameObject.Find("Border");

        switch (boardChoice)
        {
            case 1:
                row = 7;
                col = 7;
                gameObject.transform.position = new Vector3(3, 0, 3);

                grid = new char[,] {
            { 's', 's' ,'p','p','p','s','s'},
            { 's', 'p' ,'p','p','p','p','s'},
            
            { 'p', 'p' ,'p','e','p','p','p'},
            {'p', 'p' ,'p','p','p','p','p' },
            {'p', 'p' ,'p','p','p','p','p'},

            { 's', 'p' ,'p','p','p','p','s'},
            { 's', 's' ,'p','p','p','s','s'},
        };
               
                break;
            case 2:
                row = 9;
                col = 9;
                gameObject.transform.position = new Vector3(4, 0, 4);

                grid = new char[,] {
            { 's', 's' ,'s','p','p','p','s','s','s'},
            { 's', 's' ,'s','p','p','p','s','s','s'},
            { 's', 's' ,'s','p','p','p','s','s','s'},

            {'p', 'p' ,'p','p','p','p','p','p','p' },
            {'p', 'p' ,'p','p','e','p','p','p','p' },
            {'p', 'p' ,'p','p','p','p','p','p','p' },

            { 's', 's' ,'s','p','p','p','s','s','s'},
            { 's', 's' ,'s','p','p','p','s','s','s'},
            { 's', 's' ,'s','p','p','p','s','s','s'},
        };
                Debug.Log("gectti");
                break;
            case 3:
                row = 8;
                col = 8;
                gameObject.transform.position = new Vector3(3, 0, 4);

                grid = new char[,] {
            { 's' ,'s','p','p','p','s','s','s'},
            { 's' ,'s','p','p','p','s','s','s'},
            { 's' ,'s','p','p','p','s','s','s'},

            {'p' ,'p','p','p','p','p','p','p' },
            {'p' ,'p','p','e','p','p','p','p' },
            {'p' ,'p','p','p','p','p','p','p' },

            {'s' ,'s','p','p','p','s','s','s'},
            {'s' ,'s','p','p','p','s','s','s'},
        };
                break;
            case 4:
                row = 7;
                col = 7;
                gameObject.transform.position = new Vector3(3, 0, 3);

                grid = new char[,] {
            { 's', 's' ,'p','p','p','s','s'},
            { 's', 's' ,'p','p','p','s','s'},

            { 'p', 'p' ,'p','e','p','p','p'},
            {'p', 'p' ,'p','p','p','p','p' },
            {'p', 'p' ,'p','p','p','p','p'},

            { 's', 's' ,'p','p','p','s','s'},
            { 's', 's' ,'p','p','p','s','s'},
        };
                break;
            case 5:
                row = 9;
                col = 9;
                gameObject.transform.position = new Vector3(4, 0, 4);

                grid = new char[,] {
            { 's', 's' ,'s','s','p','s','s','s','s'},
            { 's', 's' ,'s','p','p','p','s','s','s'},
            { 's', 's' ,'p','p','p','p','p','s','s'},
            {'p', 'p' ,'p','p','p','p','p','p','p' },
            
            {'p', 'p' ,'p','p','e','p','p','p','p' },
            
            {'s', 'p' ,'p','p','p','p','p','p','s' },
            { 's', 's' ,'p','p','p','p','p','s','s'},
            { 's', 's' ,'s','p','p','p','s','s','s'},
            { 's', 's' ,'s','s','p','s','s','s','s'},
        };
                break;
        }

    }

    /* Create board tiles*/
    private void CreateBoardTiles()
    {
        
        for (int x = 0; x < row; ++x)
        {
            for (int y = 0; y < col; ++y)
            {   
                if(grid[x,y] != 's')
                {
                    CreateTile(x, y);

                }
            }
        }
    }

    /* Create one tile*/
    private void CreateTile(int x, int y)
    {

        GameObject go = Instantiate(prefabTile) as GameObject;
        go.transform.SetParent(transform);
        Tile tile = go.GetComponent<Tile>();
        
        tiles[x, y] = tile;
        MoveTile(tile, x, y);

    }

    /* Move the tile according to board*/
    private void MoveTile(Tile tile, int x, int y)
    {
        tile.transform.position = new Vector3(y , 1, x );
        
    }

    /* Crete board's peg*/
    private void CreatePegs()
    {
        for (int x = 0; x < row; ++x)
        {
            for (int y = 0; y < col; ++y)
            {
                
                    CreatePeg(x, y,grid[x,y]);
                   
                
            }
        }
        GameObject.Find("Peg").transform.position = new Vector3(-100, 2, 0);
        GameObject.Find("Tile").transform.position = new Vector3(-100, 2, 0);
    }

    /* Create one peg*/
    private void CreatePeg(int x , int y, char type)
    {   
        
        GameObject go = Instantiate(prefabPeg) as GameObject;
        go.transform.SetParent(transform);
        Peg peg = go.GetComponent<Peg>();
        peg.row = x;
        peg.col = y;
        peg.type = type;
        pegs[x, y] = peg;
        MovePeg(peg, x, y);
        if(peg.type == 's')
        {
            peg.GetComponent<Renderer>().enabled = false;
        }
        
    }
    /* Move peg according to board*/
    private void MovePeg(Peg peg, int x, int y)
    {
        peg.transform.position = new Vector3(y, 2, x);
        if(peg.type == 'e')
        {
            peg.GetComponent<Renderer>().enabled = false;
        }
        
    }
    /* Operator overloading for 2d matrix*/
    public Peg this[int row, int column]
    {
        get
        {
            return pegs[row, column];
        }
        set
        {
            pegs[row, column] = value;
        }
    }

    


}
