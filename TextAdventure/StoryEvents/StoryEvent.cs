#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StoryEvent
{
    public string[] text;
    public StoryEvent next;
    public StoryEventOption[] options;
    public IAmTrigger[] triggers;
}

public struct StoryEventOption
{
    public string display;
    public StoryEvent next;
}
