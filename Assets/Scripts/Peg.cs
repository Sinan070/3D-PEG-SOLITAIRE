using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Peg 's class*/
public class Peg : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Peg(int rowP, int colP,char typeP)
    {
        row = rowP;
        col = colP;
        type = typeP;
    }
    public int row; // row of peg*/


    public int col; /* col number of peg*/
    public char type; /* type of board*/
}
