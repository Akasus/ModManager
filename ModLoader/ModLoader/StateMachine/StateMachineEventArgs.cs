// File: StateMachineEventArgs.cs
// Project: ModLoader
// Author: Akasus (akasus.nevelas@gmail.com)
// Description:

namespace ModLoader;

public class StateMachineLeaveEventArgs
{
    public StateMachineLeaveEventArgs(State newState)
    {
        CancelEvent = false;
        NextState = newState;
    }

    public bool CancelEvent { get; set; }
    public State NextState { get; set; }
}

public class StateMachineEnterEventArgs
{
    public StateMachineEnterEventArgs(State previousState)
    {
        CancelEvent = false;
        PreviousState = previousState;
    }

    public bool CancelEvent { get; set; }
    public State PreviousState { get; set; }
}