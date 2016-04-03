using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NameScript : MonoBehaviour {

    private static TextAsset inputFile;

    public static List<string> AvailNames = new List<string>();
    public static List<string> CurrNames = new List<string>();
    public static List<string> FreshNames = new List<string>();

    public static List<string> PlayerNames = new List<string>();

    void Awake()
    {
        TextAsset txtFile = Resources.Load("names") as TextAsset;
        AvailNames = ParseNamesList(txtFile);
        Resources.UnloadAsset(txtFile);
        DontDestroyOnLoad(this);
    }

    private static List<string> ParseNamesList(TextAsset file){

        List<string> cleanNames = new List<string>();
        string[] dirtyNames = file.text.Split('\n');

        foreach (string token in dirtyNames)
        {
            int start = 0;
            int end = 0;

            foreach (char letter in token)
            {
                end++;
                if (letter == ' ')
                    break;
            }

            cleanNames.Add(token.Substring(start, end - start));
        }

        cleanNames.RemoveAt(cleanNames.Count - 1);

        return cleanNames;
    }

    public static string GetName(string callTag) {
        string newName = "Name";

        if ((callTag == "Enemy"))
        {
            if (FreshNames.Count > 0)
            {
                int index = Random.Range(0, FreshNames.Count - 1);
                newName = FreshNames[index];
                FreshNames.RemoveAt(index);
            }
            else if (AvailNames.Count > 0)
            {
                int index = Random.Range(0, AvailNames.Count - 1);
                newName = AvailNames[index];
                AvailNames.RemoveAt(index);
            }
            else
            {
                newName = "Zombie";
            }
        }
        
        else if((callTag == "Player"))
        {
            if (AvailNames.Count > 0)
            {
                int index = Random.Range(0, AvailNames.Count - 1);
                newName = AvailNames[index];
                AvailNames.RemoveAt(index);
            }
            else
            {
                newName = "John";
            } 
        }

        CurrNames.Add(newName);
        return newName;
    }

    public static void GeneratePlayerNames(){
        PlayerNames.Clear();
        for(int i=0; i<4; i++){
            PlayerNames.Add(GetName("Player"));
        }
    }

    public static void ReturnName(string callTag, string name)
    {
        if(callTag == "Player")
        {
            CurrNames.Remove(name);
            FreshNames.Add(name);
        }
        else
        {
            CurrNames.Remove(name);
            AvailNames.Add(name);
        }
    }
}
