namespace Sports;

[HammerEntity]
[Library( "sports_gamemode_orb" )]
[Title( "Gamemode Orb" )]
[Category( "Map Setup" )]
[EditorModel( "models/editor/cone_helper.vmdl" )]
[Particle]
public partial class GamemodeOrb : BaseTrigger
{
	public static HashSet<GamemodeOrb> Orbs { get; set; } = new();

	[Property( "effect_name", Title = "Particle System Name" ), ResourceType( "vpcf" )]
	public string ParticleSystemName { get; set; }
	public BaseGamemode LinkedGamemode { get; set; }

	[Property( "gamemode_name", Title = "Gamemode Name" ), FGDType( "target_destination" )]
	public string Gamemode { get; set; }

	protected TimeSince LastTouch = 1f;

	[Event.Entity.PostSpawn]
	protected void PostEntitiesSpawned()
	{
		LinkedGamemode = SportsGame.Instance?.GetGamemodeFromId( Gamemode );
		if ( LinkedGamemode is not null )
			Log.Debug( "Orb: Linked to gamemode." );
	}

	public override void Spawn()
	{
		base.Spawn();
		Orbs.Add( this );

		Transmit = TransmitType.Always;

		SetupPhysicsFromSphere( PhysicsMotionType.Keyframed, Vector3.Zero, 16f );
		CollisionGroup = CollisionGroup.Trigger;

		_ = new ParticleSystemEntity
		{
			Position = Position,
			ParticleSystemName = ParticleSystemName,
			StartActive = true
		};
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		Orbs.Remove( this );
	}

	public override void StartTouch( Entity other )
	{
		base.StartTouch( other );

		if ( LastTouch < 1f ) return;

		if ( other is PlazaPlayer player )
		{
			Log.Debug( $"Orb trying to add player: {player.Client.Name} to gamemode: {Gamemode}" );
			LinkedGamemode?.AddClient( player.Client );
			LastTouch = 0;
		}
	}

	[Event.Tick]
	private void Tick()
	{
		if ( !Debug.Enabled )
			return;

		DebugOverlay.Text( $"Gamemode Orb: {Gamemode}", Position );
		DebugOverlay.Sphere( Position, 16, Color.White );
	}
}
