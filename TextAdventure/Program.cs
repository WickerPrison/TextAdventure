// See https://aka.ms/new-console-template for more information
#nullable disable
using Spectre.Console;
using System.Reflection;
using System.Runtime.CompilerServices;

CharacterStats rhun = new CharacterStats();
rhun.character = Character.RHUN;
rhun.evasion = 0;
rhun.defiance = 2;
rhun.actionDice = 3;
rhun.maxHealth = 20;

OpeningScene openingScene = new OpeningScene();

Utils.CallStoryEvent(openingScene.firstEvent);
