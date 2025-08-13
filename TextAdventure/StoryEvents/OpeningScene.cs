using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OpeningScene: IAmScene
{
    public StoryEvent firstEvent { get; set; }

    public OpeningScene()
    {
        StoryEvent crestPeak = new StoryEvent();
        //firstEvent = crestPeak;
        StoryEvent choosePath = new StoryEvent();
        StoryEvent forest = new StoryEvent();
        StoryEvent rain = new StoryEvent();
        StoryEvent reachShrine = new StoryEvent();
        StoryEvent glawddTrial = new StoryEvent();
        firstEvent = glawddTrial;


        crestPeak.text = ["Rhun crested the top of a peak and there it was, merely a mile away. The shrine where he would finally become a priest of Glawdd."];
        crestPeak.next = choosePath;

        choosePath.text = ["He saw two paths forward. He could stick to the forest and keep out of the rain that had been growing in strength since he started his journey, or he could travel along the crest of the mountain and face the rain head on."];
        StoryEventOption _forest = new StoryEventOption();
        _forest.display = "Stick to the forest";
        _forest.next = forest;
        StoryEventOption _rain = new StoryEventOption();
        _rain.display = "Face the rain";
        _rain.next = rain;
        choosePath.options = [_forest, _rain];

        int evasionAmount = 5;
        forest.text = [$"Rhun took the slightly longer route through the trees, sheltered from the rain. +{evasionAmount} Evasion"];
        forest.triggers = [new IncrementStatTrigger(StatTypes.EVASION, Character.RHUN, evasionAmount)];
        forest.next = reachShrine;

        int defianceAmount = 1;
        rain.text = [$"Rhun marched forward, straight through the ever worsening storm. +{defianceAmount} Defiance"];
        rain.triggers = [new IncrementStatTrigger(StatTypes.DEFIANCE, Character.RHUN, defianceAmount)];
        rain.next = reachShrine;

        reachShrine.text = [
          "As Rhun reached the shrine, the storm rose to levels of ferocity reserved only for the preists of Glawdd.",
          "He pulled the small, but sturdy, stone door open and slid into the dark room. The only source of light was a candle held by a middle aged man seated in the middle of the room.",
          "\"Welcome to the shrine of Glawdd\", the man said as he stepped to one corner of the room and gestured for Rhun to stand in front of the altar.",
          "Rhun stepped forward and warily eyed the chunk of scaly flesh wriggling on the altar.",
          "\"The priests of Glawdd are the only line of defense between the people and Glawdd, the God of Storms.\", the man continued, \"In joining our ranks you will incur his wrath. He will seek to destroy you at every turn. But in turn, you will bring saftey to the people. Do you understand?\"",
          "\"Yes\", Rhun said, keeping his eyes focused on the scrap of flesh. A scrap of flesh torn from the body of Glawdd himself by a High Priest, and the final test Rhun would pass to become a priest.",
          "\"Very well. Draw your sword and be warned, it will fight back\""
        ];
        reachShrine.next = glawddTrial;

        glawddTrial.text = ["Rhun drew his sword and prepared for combat."];
        glawddTrial.triggers = [new StartCombatTrigger(new List<IAmEnemy>() { new FleshOfGlawdd() })];
    }
}
