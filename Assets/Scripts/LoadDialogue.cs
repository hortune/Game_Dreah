using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadDialogue : MonoBehaviour {
    public Text Dialogue;

    private string Filename;

    void Start()
    {
        LoadTextFile("Credit");
    }

    void LoadTextFile(string filename)
    {
        Dialogue.text = ((TextAsset)Resources.Load(filename)).text;
    }
	
}
