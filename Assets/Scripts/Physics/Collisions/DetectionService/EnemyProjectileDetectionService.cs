using Enemy;
using Physics.Collisions.Relay.Trigger2D;

namespace Physics.Collisions.DetectionService
{
	public class EnemyProjectileDetectionService : AbstractDetectionService<EnemyProjectileSystem>
	{
		#region Constructors

		public EnemyProjectileDetectionService(Trigger2DRelay trigger2DRelay)
			: base(trigger2DRelay)
		{ }

		#endregion
	}
}
