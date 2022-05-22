﻿using Sandbox;

namespace Sports;

public class BowlingPlayerAnimator : PawnAnimator
{
	private BowlingMoveType currentMoveType = BowlingMoveType.Move;

	public override void Simulate()
	{
		float inputLeft = 0;

		// Switch between move types
		if ( Input.Released( InputButton.Reload ) )
			currentMoveType = currentMoveType == BowlingMoveType.Move ? BowlingMoveType.Rotate : BowlingMoveType.Move;

		if ( currentMoveType == BowlingMoveType.Move )
			inputLeft = Input.Left;

		if ( currentMoveType == BowlingMoveType.Rotate )
			Rotation *= Rotation.FromYaw( Input.Left * 2 );

		SetAnimParameter( "move_x", MathX.LerpTo( AnimPawn.GetAnimParameterFloat( "move_x" ), inputLeft * -50f, Time.Delta * 10f ) );
		Position += AnimPawn.RootMotion * Rotation * Time.Delta * 1.5f;

		if ( Debug.Enabled )
			DebugOverlay.ScreenText( "[BOWLING MOVEMENT]\n" +
				$"Move type: {currentMoveType}"
				);
	}

	public void DoResultAnimation( bool wasGoodBowl )
	{
		if ( wasGoodBowl )
		{
			SetAnimParameter( "b_bowling_positive_hit", true );
			return;
		}

		SetAnimParameter( "b_bowling_negative_hit", true );
	}

	public void DoThrow()
	{
		SetAnimParameter( "b_bowling_throw", true );
	}
}
