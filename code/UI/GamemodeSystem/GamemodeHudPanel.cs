namespace Sports;

public partial class GamemodeHudPanel : Panel
{
	public Panel Panel { get; set; }

	[Events.Client.LocalGamemodeChanged]
	public void GamemodeChanged( BaseGamemode gamemode )
	{
		if ( !gamemode.IsValid() )
		{
			Panel?.Delete( true );
			Panel = null;

			return;
		}

		Panel = gamemode.CreateHud();

		if ( Panel is null ) return;

		Panel.Parent = this;
	}
}
