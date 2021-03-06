using Sports.StateSystem;

namespace Sports.Football.States;

public class FootballPostGameState : BaseState<FootballStateMachine>
{
	public override void CheckSwitchState()
	{
		base.CheckSwitchState();

		StateMachine.Game.Finish();
	}

	public override void OnEnter()
	{
		base.OnEnter();
		StateMachine.GameActive = false;
	}
}
