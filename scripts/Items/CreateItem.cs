using Godot;
using System;
using System.Collections.Generic;

public partial class CreateItem : Control
{
    private LineEdit[] TextLines;
    private Button AddButton, FindButton;
    public override void _Ready()
    {
        var textList = new List<LineEdit>();
        for (var i = 1; i < 7; i++) 
            textList.Add(GetNode<LineEdit>("LineEdit" + i));
        
        TextLines = textList.ToArray();
        AddButton = GetNode<Button>("Button");
        FindButton = GetNode<Button>("Button2");

        AddButton.Pressed += Add;
    }

    void Add()
    {
        bool bol = true;
        foreach (var line in TextLines)
            if (line.Text.Length == 0) {
                bol = false;   
                GetNode<Label>("Label").Text = "Fatal error. In " + line.Name + " line missing any text.";
            }

        if (bol)
        {
            
        }
                
    }
}
