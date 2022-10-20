namespace ModLoader;

public abstract class State
{
    protected StateMachine controller;

    protected IBlackBoard _memory;

    public void SetController(StateMachine controller, IBlackBoard blackboard)
    {
        this.controller = controller;
        this._memory = blackboard;
    }

    public virtual void OnStateEnter(StateMachineEnterEventArgs args)
    { }

    public virtual void OnStateUpdate()
    { }
    public virtual void OnStateLeave(StateMachineLeaveEventArgs args)
    { }

    public override string ToString()
    {
        var name = GetType().Name.Replace("State", "");
        return string.Concat(name.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
    }
}

public abstract class State<TBlackboard> : State where TBlackboard : IBlackBoard
{
    protected TBlackboard BlackBoard => (TBlackboard)_memory;
}