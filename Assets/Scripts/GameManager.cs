using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    
    // Start is called before the first frame update
    // Board of Game
    public  Board board;
    /* Selected Pegs*/
    private Peg first, second;

    /* Selected Peg's Flag, Indicates which peg is selected first and second*/
    private bool flag = false;
    
    /* is the move valid to make move*/
    private bool canMove = false;

    /* Undo pegs*/
    private  static Peg undo1;
    private  static Peg undomid;
    private static Peg undo2;
    
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            UpdateMouseOver();
        }
    }

    /* Get Mouse Action*/
    private void UpdateMouseOver()
    {
        if (!Camera.main)
        {
            Debug.Log("The Main Camera is NOT Found\n");
        }
        else
        {
            RaycastHit raycast;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out raycast,Mathf.Infinity))
            { 
                PlayUser(raycast);
            }
        }
    }
    
    /* Check if the move is valid*/
    private void CanMove()
    {
        bool status = false;
        bool localFlag = false;


        if (second !=null && first != null)
        {
            int midRow = (first.row + second.row) / 2;
            int midCol = (first.col + second.col) / 2;
            

            if ((first.row == second.row && Math.Abs(first.col - second.col) == 2) ||
                    (first.col == second.col && Math.Abs(first.row - second.row) == 2))
            {
                localFlag = true;
            }

            if (second.type == 'e' &&
                    board[midRow, midCol].type == 'p' &&
                    first.type == 'p' && localFlag)
            {
                status = true;
            }
            
        }

        canMove = status;
        
    }
    
    /* Move Peg*/
    private void MovePeg()
    {
        int midRow = (first.row + second.row) / 2;
        int midCol = (first.col + second.col) / 2;
        
        board[midRow, midCol].type = 'e';
        second.type = 'p';
        first.type = 'e';
        undo1 = first;
        undo2 = second;
      
        board[midRow, midCol].GetComponent<Renderer>().enabled = false;
        first.GetComponent<Renderer>().enabled = false;
        second.GetComponent<Renderer>().enabled = true;
        
        
        undomid = board[midRow, midCol];
        

    }

    /*Select Peg*/
    private void SelectPeg(RaycastHit hit)
    {
       if(flag == false)
        {

            first = hit.transform.gameObject.GetComponent<Peg>();
            first.transform.position += (Vector3.up);
            flag = true;
  
        }
        else
        {
            second = hit.transform.gameObject.GetComponent<Peg>();
            flag = false;
            first.transform.position = new Vector3(first.col, 2, first.row);
            second.transform.position += (Vector3.up);
            
            
        }
        

    }
    
    /* Start is called before first frame. Finds created board*/
    private void Start()
    {
        board = GameObject.Find("Board").GetComponent<Board>();
    }

    /* Back to Menu*/
    public void BacktoMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");

    }

    /* Play AUTO*/
    public void PlayAuto()
    {
        if(first!=null)     first.transform.position = new Vector3(first.col, 2, first.row);
        if(second!= null)   second.transform.position = new Vector3(second.col, 2, second.row);
        flag = false;
        while (true)
        {   

        int firstRow = randInt();
        int firstCol = randInt();
        int secondRow = randInt();
        int secondCol = randInt();

            first = board.pegs[firstRow, firstCol];
            second = board.pegs[secondRow, secondCol];
            
            
            CanMove();
            if (canMove)
            {
            
            MovePeg();
                break;
            }
        }
        
        


    }
    
    /* Check if the game is ended or not*/
    private bool IsGameEnded()
    {
        int peg_num = 0;
        int flag = 0;
        int boardsize = board.pegs.GetLength(0);
        for (int i = 0; i < boardsize; ++i)
        {
            for (int j = 0; j < board.pegs.GetLength(0); ++j)
            {
                if (board.pegs[i,j].type == 'p')
                {
                    ++peg_num;
                    if (j - 2 >= 0 && board.pegs[i,j-2].type == 'e' && board.pegs[i,j-1].type == 'p')
                    {
                        flag = 1;
                    }
                    if (j + 2 < boardsize && board.pegs[i, j + 2].type == 'e' && board.pegs[i, j + 1].type == 'p')
                    {
                        flag = 1;
                    }
                    if (i - 2 >= 0 && board.pegs[i-2, j].type == 'e' && board.pegs[i-1, j].type == 'p')
                    {
                        flag = 1;
                    }
                    if (i + 2 < boardsize && board.pegs[i+2, j].type == 'e' && board.pegs[i+1, j].type == 'p')
                    {
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        return false;
                    }
                }
            }
        }
        /* if there is a only one pegs, the game is finished by best score*/
        if (peg_num == 1)
        {
            return true;
        }
        return true;

    }

    /* Play Auto until the end*/
    public void PlayAutoAll()
    {
        while (!IsGameEnded())
        {
            PlayAuto();
        }
        
    }

    /* Play by user*/
    private void PlayUser(RaycastHit hit)
    {  
        SelectPeg(hit);
        
        
        if (second != null)
        {
            
            CanMove();
            if (canMove)
            {
                MovePeg();
                first.transform.position = new Vector3(first.col, 2, first.row);
                
            }
            second.transform.position = new Vector3(second.col, 2, second.row);


        }

    }

    /* Get random int*/
    private int randInt()
    {
        int sayi =UnityEngine.Random.Range(0, board.pegs.GetLength(0));   
        return sayi;
    }
    /* Reset the board*/
    public void Reset()
    {
       UnityEngine.SceneManagement.SceneManager.LoadScene("Game");

    }
    /* Undo the move*/
    public void Undo()
    {
        if (undo2 != null)
        {
            undo1.type = 'p';
            undo2.type = 'e';
            undomid.type = 'p';

            board[undomid.row,undomid.col].GetComponent<Renderer>().enabled = true;
            board[undo1.row, undo1.col].GetComponent<Renderer>().enabled = true;
            board[undo2.row, undo2.col].GetComponent<Renderer>().enabled = false;
        }
          
   
        first.transform.position = new Vector3(first.col, 2, first.row);
        second.transform.position = new Vector3(second.col, 2, second.row);
        flag = false;
    }
    
    /* Save game to textfile but to do*/
    private void Save()
    {
        /*TO DO*/
    }
    
    /* Load game from textfile but to do*/

    private void Load()
    {
        /*TO DO*/
    }
}
