using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class HomeCode : MonoBehaviour
{
    public TMP_Text usernameText;
    public string nametext;
    private void Start()
    {
        string loggedInUsername = PlayerPrefs.GetString("LoggedInUsername", "Guest");
        usernameText.text = loggedInUsername;
        
    }

    public void CheckUsername(){
        SceneManager.LoadScene("EditUsername");
    }

    public void Playmatch(){

    }

    public void Joinroom(){

    }

    public void Createroom(){

    }

    public void Playhistory(){

    }

}
