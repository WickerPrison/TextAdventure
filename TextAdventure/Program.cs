// See https://aka.ms/new-console-template for more information
#nullable disable
using Spectre.Console;
using System.Reflection;
using System.Runtime.CompilerServices;

Rhun rhun = new Rhun();

OpeningScene openingScene = new OpeningScene();

Utils.CallStoryEvent(openingScene.firstEvent);
