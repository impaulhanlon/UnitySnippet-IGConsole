using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCommands : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Console.RegisterCommand(new ConsoleCommand {command = "give", description = "give a <i>PLAYER</i> this <i>QUANTITY</i> of this <i>ITEM</i>", Run = GiveItem });
        Console.RegisterCommand(new ConsoleCommand { command = "tp", description = "Move a <i>PLAYER</i> to possition <i>X Y</i>", Run = TeleportPlayer});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GiveItem(string[] args)
    {
        if (args.Length != 4) return;
        string who = args[1];
        string item = args[3];
        string quantity = args[2];
        Debug.Log("Give " + who + " " + quantity + " " + item); 
    }

    void TeleportPlayer(string[] args)
    {
        if (args.Length != 4) return;
        string who = args[1];
        string x = args[2];
        string y = args[3];
        Debug.Log("Move player " + who + " to position+ ("+x+","+y+")");
    }

}
