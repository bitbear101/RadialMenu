using Godot;
using System;

[Tool]
public partial class RadialActionMenu : Container
{
    [ExportSubgroup("Radial Menu Properties")]
    [Export] private float _radius = 150f; //The distance away from the centre of the circle to draw the buttons
    [Export] private float _currentAngle = 1.571f;//Set the current angle to zero every time the function is run, the current value ensures the info panel is at the bottom at all times

    //The buttons for the actions
    [ExportSubgroup("Actions Buttons")]
    [Export] Button btnUpgrade;
    [Export] Button btnDisband;
    [Export] Button btnExplore;
    [Export] Button btnCollect;
    [Export] Button btnBuild;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Error check if the buttons are loaded into the available slots created in the script
        if (btnUpgrade == null) GD.PrintErr("The upgrade button has not been set in the inspector");
        if (btnDisband == null) GD.PrintErr("The disband button has not been set in the inspector");
        if (btnExplore == null) GD.PrintErr("The explore button has not been set in the inspector");
        if (btnCollect == null) GD.PrintErr("The collect button has not been set in the inspector");
        if (btnBuild == null) GD.PrintErr("The build button has not been set in the inspector");

        //Connect the buttons to the requiered function
        btnUpgrade.ButtonUp += () => OnUpgrade(); //Add the function to the event buttonUp from the button signals 
        btnDisband.ButtonUp += () => OnDisband(); //Add the function to the event buttonUp from the button signals 
        btnExplore.ButtonUp += () => OnExplore(); //Add the function to the event buttonUp from the button signals 
        btnCollect.ButtonUp += () => OnCollect(); //Add the function to the event buttonUp from the button signals 
        btnBuild.ButtonUp += () => OnBuild(); //Add the function to the event buttonUp from the button signals

        //Register the listener for the call to the UI element to display
        // CallUIActionEvent.RegisterListener(OnCallUIActionEvent);


    }

    public override void _Process(double delta)
    {
        ArrangeButtonsInCircle();
    }

    private void ArrangeButtonsInCircle()
    {
        int _numberOfChildren = 0; //Reset the nuber of children every time the function is called 
        //Set the number of buttons to the children in the container
        foreach (Control control in GetChildren())
        {
            if (control.Visible) _numberOfChildren++;
        }
        //Set the incriment ration for hte buttons acording to how many buttons are visible
        float _angleIncrement = Mathf.Tau / _numberOfChildren;

        float _startingAngle = _currentAngle;

        for (int i = 0; i < GetChildCount(); i++)
        {
            //Grab the child button 
            Control control = (Control)GetChild(i);
            //If the button is visible we preform the actions on it
            if (control.Visible)
            {
                //Set the position of the button
                Vector2 _controlPosition = new Vector2(_radius * Mathf.Cos(_startingAngle), _radius * Mathf.Sin(_startingAngle));
                //Set the buttons new position 
                control.Position = _controlPosition - (control.Size / 2); //the new button position minus the buttons size to centre the button correctly
                _startingAngle += _angleIncrement; //Update the angle for the next iteration
            }
        }
    }

    public void OnUpgrade()
    {
        GD.Print("Upgrade was pressed");
    }
    public void OnDisband()
    {
        GD.Print("Disband was pressed");
    }
    public void OnExplore()
    {
        GD.Print("Explore was pressed");
    }
    public void OnCollect()
    {
        GD.Print("Collect was pressed");
    }
    public void OnBuild()
    {
        GD.Print("Build was pressed");
    }

    private void HideAllButtons()
    {
        //Hide all the buttons in the radial menu
        btnUpgrade.Visible = false;
        btnDisband.Visible = false;
        btnExplore.Visible = false;
        btnCollect.Visible = false;
        btnBuild.Visible = false;
    }

}
