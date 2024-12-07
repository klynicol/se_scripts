IMyShipController controller;

// The angle at which the pistons are fully deployed in the deploy sequence.
private const float DEPLOY_HING_AGNLE = 0.49f;

enum State
{
    Deploying,
    Deployed,
    Retracting,
    Retracted
}

State state = State.Retracted;

private const string FRONT_LEFT_PISTON = "Piston FL";
private const string FRONT_RIGHT_PISTON = "Piston FR";
private const string BACK_LEFT_PISTON = "Piston BL";
private const string BACK_RIGHT_PISTON = "Piston BR";

// PID controller constants
private const float Kp = 0.5f;  // Proportional gain
private const float Ki = 0.01f;  // Integral gain
private const float Kd = 0.1f;  // Derivative gain

// Reference to pistons
private IMyPistonBase frontLeftPiston;
private IMyPistonBase frontRightPiston;
private IMyPistonBase backLeftPiston;
private IMyPistonBase backRightPiston;

private Vector3D lastError = Vector3D.Zero;
private Vector3D errorSum = Vector3D.Zero;

MoveFlag flag = MoveFlag.None;
const float SENS = 0.35355f;// The dead zone to help seperate controller inputs.

public Program()
{
    // Constructor - runs when program is compiled
    Runtime.UpdateFrequency = UpdateFrequency.Update10; // Run every 10 ticks

    // Get a ship controller (cockpit/control seat)
    controller = GridTerminalSystem.GetBlockWithName("Control Station 2") as IMyShipController;
    // controller = GridTerminalSystem.GetBlocksOfType<IMyShipController>().FirstOrDefault();

    Me.GetSurface(0).ContentType = ContentType.TEXT_AND_IMAGE;

    if (controller == null)
        throw new Exception("No ship controller found!");

    Echo("Controller found");

    // check if key "1" is pressed.
    
}

public void Main(string argument, UpdateType updateSource)
{
    // Get current orientation
    Vector3D gravity = Vector3D.Normalize(controller.GetNaturalGravity());
    Vector3D up = Me.CubeGrid.WorldMatrix.Up;

    // Calculate error (how far from level we are)
    Vector3D error = Vector3D.Cross(up, Vector3D.Up);

    Read(controller);
    Me.GetSurface(0).WriteText($"Vector: {error.ToString()} \nArgument: {argument} \nFlag: {flag}");
}

private void AdjustPistons(Vector3D correction)
{
    float pitch = (float)correction.Z;
    float roll = (float)correction.X;

    // Adjust piston velocities based on correction
    frontLeftPiston.Velocity = -pitch - roll;
    frontRightPiston.Velocity = -pitch + roll;
    backLeftPiston.Velocity = pitch - roll;
    backRightPiston.Velocity = pitch + roll;

    // Clamp velocities
    ClampPistonVelocity(frontLeftPiston);
    ClampPistonVelocity(frontRightPiston);
    ClampPistonVelocity(backLeftPiston);
    ClampPistonVelocity(backRightPiston);
}

private void ClampPistonVelocity(IMyPistonBase piston)
{
    const float MAX_VELOCITY = 0.5f;
    piston.Velocity = Math.Max(-MAX_VELOCITY, Math.Min(MAX_VELOCITY, piston.Velocity));
}

void Read(IMyShipController controller)
{
    Vector3D vector = Vector3D.Normalize(controller.MoveIndicator);
    Vector2 rotation = Vector2.Normalize(controller.RotationIndicator);
    flag = MoveFlag.None;// Nothing has been pressed.
    if (vector.Z < -SENS)// Forward
        flag |= MoveFlag.Forward;
    else if (vector.Z > SENS)// Backward
        flag |= MoveFlag.Backward;
    if (vector.X < -SENS)// Left
        flag |= MoveFlag.StrafeLeft;
    else if (vector.X > SENS)// Right
        flag |= MoveFlag.StrafeRight;
    if (vector.Y > SENS)// Jump
        flag |= MoveFlag.Up;
    else if (vector.Y < -SENS)// Crouch
        flag |= MoveFlag.Down;
    if (controller.RollIndicator < -SENS)// Roll Left
        flag |= MoveFlag.RollLeft;
    else if (controller.RollIndicator > SENS)// Roll Right
        flag |= MoveFlag.RollRight;
    if (rotation.X < -SENS)// Pitch Down
        flag |= MoveFlag.PitchDown;
    else if (rotation.X > SENS)// Pitch Up
        flag |= MoveFlag.PitchUp;
    if (rotation.Y < -SENS)// Yaw Left
        flag |= MoveFlag.YawLeft;
    else if (rotation.Y > SENS)// Yaw Right
        flag |= MoveFlag.YawRight;
}

public enum MoveFlag : ushort
{
    None = 0,

    Forward = 1,
    Backward = 1 << 1,
    StrafeLeft = 1 << 2,
    StrafeRight = 1 << 3,
    Up = 1 << 4,
    Down = 1 << 5,
    RollLeft = 1 << 6,
    RollRight = 1 << 7,

    PitchUp = 1 << 8,// Added in mouse controls thanks to Maril.
    PitchDown = 1 << 9,
    YawLeft = 1 << 10,
    YawRight = 1 << 11
}
