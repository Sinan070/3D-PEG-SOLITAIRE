using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    Button btn;
    // Start is called before the first frame update
    public void PlayMenu()
    {
        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "Standart":
                Board.boardChoice = 1;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
                Debug.Log("Gecti");
                break;
            case "Asiymmetric":
                Board.boardChoice = 2;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Game");

                break;
            case "French":
                Board.boardChoice = 3;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Game");

                break;
            case "German":
                Board.boardChoice = 4;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Game");

                break;
            case "Diamond":
                Board.boardChoice = 5;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Game");

                break;
            case "Load":
                                
                break;
        }
       
            
        
    }
    

    // Update is called once per frame

}
