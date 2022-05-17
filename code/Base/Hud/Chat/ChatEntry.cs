﻿using Sandbox.UI.Construct;


namespace Sports.UI;

public partial class SportsChatEntry : Panel
{
	public Label ChatType { get; internal set; }
	public Label NameLabel { get; internal set; }
	public Label Message { get; internal set; }
	public Image Avatar { get; internal set; }

	public RealTimeSince TimeSinceBorn = 0;

	public SportsChatEntry()
	{
		Avatar = Add.Image();
		ChatType = Add.Label( "[GLOBAL]", "chat-type" );
		NameLabel = Add.Label( "Name", "name" );
		Message = Add.Label( "Message", "message" );
	}

	public override void Tick()
	{
		base.Tick();

		if ( TimeSinceBorn > 10 )
		{
			Delete();
		}
	}
}