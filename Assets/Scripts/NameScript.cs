using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NameScript : MonoBehaviour {

	private static NameScript firstCopy; //Var for avoiding duplicates

    public static List<string> Avail_Names_M = new List<string>();
    public static List<string> Curr_Names_M = new List<string>();

	public static List<string> Avail_Names_F = new List<string>();
	public static List<string> Curr_Names_F = new List<string>();

	public static string[] Character_Names = {"female", "an", "smMale", "lgMale"};

    void Awake()
    {
		DontDestroyOnLoad(this);

		if (firstCopy != null)
			Destroy(this.gameObject);
		else
			firstCopy = this;
    }

	void Start()
	{
		TextAsset txtFileM = Resources.Load("namesM") as TextAsset;
		Avail_Names_M = ParseNamesList(txtFileM);
		Resources.UnloadAsset(txtFileM);

		TextAsset txtFileF = Resources.Load("namesF") as TextAsset;
		Avail_Names_F = ParseNamesList(txtFileF);
		Resources.UnloadAsset(txtFileF);
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

    public static string GetName(string callTag, char genderChar) {

        string newName = "Name";

		//if andro caller...
		if (genderChar != 'M' & genderChar != 'F')
		{
			if (true)
			{
				int rnd = Random.Range(0, 2);
				if(rnd == 0)
					genderChar = 'M';
				else
					genderChar = 'F';
			}
		}

		//if male caller...
		if(genderChar == 'M')
		{
			//if zombie...
			if ((callTag == "Enemy"))
			{
				//if there are still names available...
				if (Avail_Names_M.Count > 0)
				{
					int index = Random.Range(0, Avail_Names_M.Count - 1);
					newName = Avail_Names_M[index];
					Avail_Names_M.RemoveAt(index);
				}
				//else...
				else
				{
					newName = "Zombie";
				}
			}

			//if player character...
			else if((callTag == "Player"))
			{
				//if there are names left...theroretically always be true
				if (Avail_Names_M.Count > 0)
				{
					int index = Random.Range(0, Avail_Names_M.Count - 1);
					newName = Avail_Names_M[index];
					Avail_Names_M.RemoveAt(index);
				}
				//else...
				else
				{
					newName = "John";
				} 
			}

			//add chosen name to current list to avoid duplicates
			Curr_Names_M.Add(newName);
		}

		//if female caller...
		else if(genderChar == 'F')
		{
				//if zombie...
				if ((callTag == "Enemy"))
				{
					//if there are still names available...
					if (Avail_Names_F.Count > 0)
					{
						int index = Random.Range(0, Avail_Names_F.Count - 1);
						newName = Avail_Names_F[index];
						Avail_Names_F.RemoveAt(index);
					}
					//else...
					else
					{
						newName = "Zombie";
					}
				}
				
				//if player character...
				else if((callTag == "Player"))
				{
					//if there are names left...theroretically always be true
					if (Avail_Names_F.Count > 0)
					{
						int index = Random.Range(0, Avail_Names_F.Count - 1);
						newName = Avail_Names_F[index];
						Avail_Names_F.RemoveAt(index);
					}
					//else...
					else
					{
						newName = "Jane";
					} 
				}
				
				//add chosen name to current list to avoid duplicates
				Curr_Names_F.Add(newName);
		}
		
		return newName;
	}
	
	public static void ReturnName(string callTag, char genderChar, string name)
	{
		//if male caller...
		if (genderChar == 'M')
		{
				Curr_Names_M.Remove(name);
				Avail_Names_M.Add(name);
		}
		
		//if female caller...
		else if (genderChar == 'F')
		{
				Curr_Names_F.Remove(name);
				Avail_Names_F.Add(name);
		}
    }

	public static void InitCharNames()
	{
		Character_Names[0] = GetName("Player", 'F');
		Character_Names[1] = GetName("Player", 'X');
		Character_Names[2] = GetName("Player", 'M');
		Character_Names[3] = GetName("Player", 'M');
	}
}
