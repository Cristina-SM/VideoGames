using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuController : MonoBehaviour
{
    public void startGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void endGame(){
        Application.Quit();
    }
    public void menuBack(){
        GameObject.Find("Menu/Exit").gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
