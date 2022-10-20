using System.ComponentModel.DataAnnotations.Schema;

namespace ModLoader;

public class StateMachine
{
    private State? _currentState;
    private IBlackBoard _memory;

    public StateMachine(State state, IBlackBoard blackboard = null)
    {
        this._memory = blackboard;
        TransitionToState(state);
    }

    public void UpdateState()
    {
        if (!_currentState.Equals(null))
        {
            _currentState.OnStateUpdate();
        }
    }

    public void TransitionToState(State state)
    {
        if (_currentState != null)
        {
            var args = new StateMachineLeaveEventArgs(state);
            this._currentState.OnStateLeave(args);
            if (args.CancelEvent) return;
        }

        var EnterArgs = new StateMachineEnterEventArgs(_currentState);
        this._currentState = state;
        this._currentState.SetController(this, _memory);
        this._currentState.OnStateEnter(EnterArgs);

        if (EnterArgs.CancelEvent)
        {
            this._currentState = EnterArgs.PreviousState;
        }
    }
}


public interface IBlackBoard
{

}