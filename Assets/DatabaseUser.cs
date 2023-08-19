using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class DatabaseUser : MonoBehaviour
{
    private MySqlConnection connection;
    private string connectionString = "Server=localhost;Database=userandpassword;User=root;Password='';";

    // Start is called before the first frame update
    void Start()
    {
        connection = new MySqlConnection(connectionString);

        try
        {
            connection.Open();
            Debug.Log("Connected to MySQL Server!");

            // Perform database operations here

        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
