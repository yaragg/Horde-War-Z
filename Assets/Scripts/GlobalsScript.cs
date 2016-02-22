using UnityEngine;
using System.Collections;

public class GlobalsScript : MonoBehaviour {

    public static string HighScoreName;
    public static int HighScoreInt;

    public static string CurrPlayer;

    //Awake() is called when script is being loaded, before Start()
    void Awake() {

        DontDestroyOnLoad(this);
    }
}
