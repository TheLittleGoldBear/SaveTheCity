namespace Turret.Events
{
	public class TurretHitEvent : ITurretEvent
	{
		#region Properties

		public TurretSystem TurretSystem { get; }

		#endregion

		#region Constructors

		public TurretHitEvent(TurretSystem turretSystem)
		{
			TurretSystem = turretSystem;
		}

		#endregion
	}
}
