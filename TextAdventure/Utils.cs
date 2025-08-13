#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using Spectre.Console;
using System.Reflection;

public static class Utils
{
    public static Random random = new Random();

    public static void CallStoryEvent(StoryEvent storyEvent)
    {
        foreach(string line in storyEvent.text)
        {
            WriteLine("");
            WriteLine(line);

            Console.ReadKey();
        }

        if(storyEvent.triggers != null)
        {
            foreach(IAmTrigger trigger in storyEvent.triggers)
            {
                trigger.FireTrigger();
            }
        }

        if(storyEvent.next != null)
        {
            CallStoryEvent(storyEvent.next);
            return;
        }

        if(storyEvent.options != null)
        {
            StoryEventOption option = AnsiConsole.Prompt(
                new SelectionPrompt<StoryEventOption>()
                .AddChoices(storyEvent.options)
                .UseConverter(option => option.display));

            CallStoryEvent(option.next);
        }
    }




    public static string GetJsonFile(string fileName)
    {
        string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string filePath = Path.Combine(exePath, fileName);
        string jsonString = File.ReadAllText(filePath);
        return jsonString;
    }

    public static string GetValueFromJson(string jsonString, string[] valuePath)
    {
        JsonNode rootNode = JsonNode.Parse(jsonString);
        JsonNode node = rootNode;

        for (int i = 0; i < valuePath.Length; i++)
        {
            node = node[valuePath[i]];
        }
        string printLine = node?.GetValue<string>();

        return printLine;
    }

    public static void WriteLine(string printLine)
    {
        if (printLine == null) throw new Exception("Cannot print null");
        AnsiConsole.WriteLine(printLine);
    }

    public static string[] JsonArrayToString(JsonArray jsonArray)
    {
        return jsonArray.Select(node => node.ToString()).ToArray();
    }
}
