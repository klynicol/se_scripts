// code from dareamus

void Main(string argument)
{ // omitted ...
  // parse the argument string 
    if (!String.IsNullOrEmpty(argument))
    {
        var parts = argument.Split(' ');
        if (parts.Length > 0) angle = Convert.ToSingle(parts[0]);
        if (parts.Length > 1) rotorName = argument.Substring(parts[0].Length).Trim();
    }
    var rotor = GridTerminalSystem.GetBlockWithName(rotorName);
    // omitted ...
    // Set rotation 
    var rotation = rotor.GetValue<float>("UpperLimit");
    rotation += angle; 
    if (rotation >= 360) rotation += angle;
    if (rotation >= 360) rotation -= 360;
    else if (rotation < 0) rotation += 360;
    rotor.SetValue("LowerLimit", rotation);
    rotor.SetValue("UpperLimit", rotation);

}