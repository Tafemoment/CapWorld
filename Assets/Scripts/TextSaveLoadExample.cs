using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Read and Write to a file
public class TextSaveLoadExample : MonoBehaviour
{
    [SerializeField] private string path;
    [SerializeField] private bool append;
    [Header("Save Data")]
    public string lineA;
    public string[] splitA;
    public int a, b;
    #region CreateTextFile Example
    public void CreateTextFile()
    {
        //Path of the file
        path = Application.dataPath + "/CharacterName.txt";
        //Create File if it doesnt exist
        if(!File.Exists(path))
        {
            File.WriteAllText(path, "Character Saves\nCharacterName: " + System.DateTime.Now.ToString());
        }
        //Content of the save data 
        string content = "\nCharacterName: "+System.DateTime.Now.ToString();
        //Save it 
       // File.AppendAllText(path, content);    //Adds
       File.WriteAllText (path, content);       //Replaces 
    }
    #endregion
    private void Start()
    {
        path = Application.dataPath + "/CharacterName.txt";
        // CreateTextFile();
        WriteToFile(path, "109|1");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            ReadFromFile(path);
        }
    }
    //Application.dataPath + "/CharacterName.txt"
    public void WriteToFile(string passThroPath, string content)
    {
        path = passThroPath;       

        StreamWriter fileWriter = new StreamWriter(path,append);
        fileWriter.Write(content);
        fileWriter.Close();
    }
    public void ReadFromFile(string passThroPath)
    {
        StreamReader fileReader = new StreamReader(passThroPath);

        lineA = fileReader.ReadLine();
        splitA = lineA.Split('|');
        a = int.Parse(splitA[0]);
        b = int.Parse(splitA[1]);
        fileReader.Close();
    }
}
