using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using TMPro;
using UnityEngine.SceneManagement;

public class EditData : MonoBehaviour
{
   private MySqlConnection connection;
    private string connectionString = "Server=localhost;Database=userandpassword;User=root;Password='';";
    public TMP_InputField Username_Edit;
    public TMP_InputField Password_Edit;
    public TMP_InputField Email_Edit;
    public TMP_Text veft;
    
    // Start is called before the first frame update
    void Start()
    {
        connection = new MySqlConnection(connectionString);
        connection.Open();
        string loggedInUsername = PlayerPrefs.GetString("LoggedInUsername", "Guest");
        string query = "SELECT * FROM users WHERE username = @loggedInUsername";
        MySqlCommand cmd = new MySqlCommand(query,connection);
        cmd.Parameters.AddWithValue("@loggedInUsername",loggedInUsername);
        MySqlDataReader reader = cmd.ExecuteReader();

        if(reader.Read()){
            string Username = reader.GetString("username");
            string Password = reader.GetString("password");
            string Email = reader.GetString("email");
            Username_Edit.text = Username;
            Password_Edit.text = Password;
            Email_Edit.text = Email;
        }

        reader.Close();
    }

    public void ButtonshowPasssword(){
        if(Password_Edit.contentType == TMP_InputField.ContentType.Password){
            Password_Edit.contentType = TMP_InputField.ContentType.Standard;
            Password_Edit.ForceLabelUpdate();
        }
        else
        {
            Password_Edit.contentType = TMP_InputField.ContentType.Password;
            Password_Edit.ForceLabelUpdate();
        }
    }

    public void Back(){
        SceneManager.LoadScene("EditUsername");
    }

    public void Edit(){
        connection = new MySqlConnection(connectionString);
        connection.Open();
        string UsernameEd = Username_Edit.text;
        string PasswordEd = Password_Edit.text;
        string EmailEd = Email_Edit.text;
        string query = "SELECT * FROM users WHERE username = @UsernameEd";
        MySqlCommand cmd = new MySqlCommand(query,connection);
        cmd.Parameters.AddWithValue("@UsernameEd", UsernameEd);
        MySqlDataReader reader = cmd.ExecuteReader();
        if (string.IsNullOrEmpty(UsernameEd) || string.IsNullOrEmpty(PasswordEd) || string.IsNullOrEmpty(EmailEd) ){
            veft.text = "Please enter the complete information.";
            veft.color = Color.red;
            reader.Close();
            connection.Close();
            return;
        }
        else if(PasswordEd.Length < 8){
            veft.text = "Enter a password of 8 or more characters.";
            veft.color = Color.red;
            reader.Close();
            connection.Close();
            return;
        }
        else if(!EmailEd.Contains("@")){
            veft.text = "Email Please Enter @";
            veft.color = Color.red;
            reader.Close();
            connection.Close();
            return; // ออกจากเมท็อดเพื่อไม่ทำคำสั่ง UPDATE
        }
        else
        {
            reader.Close(); // ปิด reader ก่อนที่จะใช้ connection สร้างคำสั่งใหม่

            string insertQuery = "UPDATE users SET password = @PasswordEd, email = @EmailEd WHERE username = @UsernameEd";
            MySqlCommand updateCmd = new MySqlCommand(insertQuery, connection);
            updateCmd.Parameters.AddWithValue("@UsernameEd", UsernameEd);
            updateCmd.Parameters.AddWithValue("@PasswordEd", PasswordEd);
            updateCmd.Parameters.AddWithValue("@EmailEd", EmailEd);
            updateCmd.ExecuteNonQuery();
            veft.text = "Edit successful!";
            veft.color = Color.green;
            connection.Close();
            SceneManager.LoadScene("EditUsername");
        }
    }
}
