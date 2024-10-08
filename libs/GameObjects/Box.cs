namespace libs;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json; 

public class Box : GameObject
{
    private List<DialogNode> dialogNodes;
    private DialogNode currentNode;
    public string DialogFilePath { get; set; } // Add this line


    public Box(int posX, int posY, string dialogFilePath) : base(posX, posY)
    {
        DialogFilePath = dialogFilePath; // And this line
        if (string.IsNullOrEmpty(dialogFilePath))
        {
            throw new ArgumentException("Dialog file path cannot be null or empty.", nameof(dialogFilePath));
        }

        Type = GameObjectType.Box;
        CharRepresentation = '■';
        Color = ConsoleColor.DarkGreen;
        string dialogData = System.IO.File.ReadAllText(dialogFilePath);
        dialogNodes = JsonConvert.DeserializeObject<List<DialogNode>>(dialogData);
        currentNode = dialogNodes.Find(node => node.DialogID == "1");
    }

    public bool Interact()
    {
        while (true)
        {
            Console.WriteLine(currentNode.Text);
            if (currentNode.Responses.Count == 0)
            {
                break;
            }

            for (int i = 0; i < currentNode.Responses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {currentNode.Responses[i].ResponseText}");
            }

            int choice;
            Console.Write("Please enter your choice: "); // Prompt the user
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > currentNode.Responses.Count)
            {
                Console.WriteLine("Invalid choice, please try again.");
            }

            Response selectedResponse = currentNode.Responses[choice - 1];
            currentNode = dialogNodes.FirstOrDefault(node => node.DialogID == selectedResponse.NextNode.DialogID);

            if (selectedResponse.IsCorrect)
            {
                return true;
            }
        }

        return false;
    }
}