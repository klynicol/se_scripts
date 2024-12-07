public class AutoLevelSystem : MyGridProgram
{
    // Piston names - adjust these to match your pistons
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
    
    public Program()
    {
        // Constructor - runs when program is compiled
        Runtime.UpdateFrequency = UpdateFrequency.Update10; // Run every 10 ticks
        
        // Initialize pistons
        frontLeftPiston = GridTerminalSystem.GetBlockWithName(FRONT_LEFT_PISTON) as IMyPistonBase;
        frontRightPiston = GridTerminalSystem.GetBlockWithName(FRONT_RIGHT_PISTON) as IMyPistonBase;
        backLeftPiston = GridTerminalSystem.GetBlockWithName(BACK_LEFT_PISTON) as IMyPistonBase;
        backRightPiston = GridTerminalSystem.GetBlockWithName(BACK_RIGHT_PISTON) as IMyPistonBase;
    }
    
    public void Main(string argument, UpdateType updateSource)
    {
        if (!ValidatePistons()) return;
        
        // Get current orientation
        Vector3D gravity = Vector3D.Normalize(Me.GetNaturalGravity());
        Vector3D up = Me.CubeGrid.WorldMatrix.Up;
        
        // Calculate error (how far from level we are)
        Vector3D error = Vector3D.Cross(up, Vector3D.Up);
        
        // PID calculations
        Vector3D derivative = (error - lastError) / 0.1f; // 0.1 seconds between updates
        errorSum += error * 0.1f;
        
        // Calculate correction
        Vector3D correction = (error * Kp) + (errorSum * Ki) + (derivative * Kd);
        
        // Apply corrections to pistons
        AdjustPistons(correction);
        
        lastError = error;
    }
    
    private bool ValidatePistons()
    {
        return frontLeftPiston != null && frontRightPiston != null && 
               backLeftPiston != null && backRightPiston != null;
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
}