using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    enum ConsoleState
    {
        Short,
        Full
    }

    [SerializeField] private int fontSize { get; set; }
    [SerializeField] public Vector2 resolution;
    [SerializeField] private Color consoleColor { get; set; }
    [SerializeField] private RectTransform consoleTransform;
    [SerializeField] private GameObject console;
    [SerializeField] private TMP_InputField input;
    [SerializeField] private TMP_Text log;
    private bool isUsingConsole;
    private bool isOpen = false;
    private bool isExpanded = false;

    static Dictionary<string,ConsoleCommand> commands { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (consoleTransform == null) Debug.Log("Console is not set correctly, this may course problems");
        console.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("console"))
        {
            isOpen = !isOpen;
            console.SetActive(isOpen);
            UpdateState(ConsoleState.Short);
        }

        if(isOpen)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                ProcessConsole();
            }
        }
    }

    public static void RegisterCommand(ConsoleCommand command)
    {
        if(commands == null) commands = new Dictionary<string, ConsoleCommand>();
        if (command == null) return;
        if(!commands.ContainsKey(command.command)) commands.Add(command.command, command);     
    }

    public void ProcessConsole()
    { 
        string consoleInput = input.text;
        if (input.text == "") return;
        AddToLog(consoleInput);
        input.text = "";
        if (consoleInput == "help")
        {
            foreach (KeyValuePair<string, ConsoleCommand> command in commands)
            {
                AddToLog(command.Key + " | " + command.Value.description);
            }
            return;
        }
        else
        {
            string[] args = consoleInput.Split(' ');
            if (commands == null) return;
            if (commands.ContainsKey(args[0]))
            {
                commands[args[0]].Run(args);
            }
        }
        
    }

    public void AddToLog(string value)
    {
        log.text += value + "\n";
    }

    public void InConsole(bool isInConsole)
    {
        isUsingConsole = isInConsole;
    }


    public void ChangeExpandState()
    {
        if (isExpanded)
        {
            UpdateState(ConsoleState.Short);
        }
        else
        {
            UpdateState(ConsoleState.Full);
        }
    }

    void UpdateState(ConsoleState state)
    {
        switch (state)
        {
            case ConsoleState.Short:
                consoleTransform.offsetMin = new Vector2(0, resolution.y-120.0f);
                isExpanded = false;
                break;
            case ConsoleState.Full:
                consoleTransform.offsetMin = new Vector2(0, 0);
                isExpanded = true;
                break;
            default:
                break;
        }
    }
}

public delegate void RunCommand(string[] args);
public class ConsoleCommand
{
    public string command { get; set; }
    public string description { get; set; }

    public RunCommand Run;

}
