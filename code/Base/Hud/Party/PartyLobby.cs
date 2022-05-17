using Sports.PartySystem;

namespace Sports.UI;

[UseTemplate]
public partial class PartyLobby : Panel
{
	public static PartyLobby Instance;

	public PartyLobby()
	{
		Instance = this;
	}

	public Panel PartyCanvas { get; protected set; }
	public Panel InviteList { get; protected set; }
	public Party Party => Local.Client.Components.Get<PartyComponent>()?.Party ?? null;

	public override void Tick()
	{
		base.Tick();
		PartyCanvas.SetClass( "InParty", Party.IsValid() );
	}
	[Event.BuildInput]
	private void BuildInput( InputBuilder builder )
	{

		if ( builder.Down( InputButton.Score ) )
		{
			PartyCanvas.AddClass( "Control" );
		}
		if ( builder.Released( InputButton.Score ) )
		{
			PartyCanvas.RemoveClass( "Control" );
		}

	}

	public static void AddPartyMember( Client client )
	{
		if ( Instance == null )
			return;
		var member = Instance.PartyCanvas.AddChild<PartyMember>();
		member.Client = client;
		member.Avatar.SetTexture( $"avatar:{client.PlayerId}" );
		member.Name.SetText( client.Name );
	}


	public static void OnPartyChanged()
	{
		if ( Instance == null )
			return;

		Instance.PartyCanvas.DeleteChildren();
		foreach ( var member in Instance.Party?.Members )
		{
			AddPartyMember( member );
			Log.Info( "Added party member: " + member.Name );
		}
	}
	[Event.Hotload]
	protected override void PostTemplateApplied()
	{
		base.PostTemplateApplied();
		OnPartyChanged();
	}

	public static void OnInviteReceived( Client client )
	{
		if ( Instance == null )
			return;
		var invite = Instance.InviteList.AddChild<PartyInvite>();
		invite.Client = client;
		invite.InviteText.Text = $"{client.Name} invited you to their party!";
		invite.AddClass( "open" );
	}
}
