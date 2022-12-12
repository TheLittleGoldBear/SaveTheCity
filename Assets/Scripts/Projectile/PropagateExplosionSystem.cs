using System.Collections.Generic;
using System.Linq;
using Enemy;
using Physics.Collisions.DetectionService;
using Physics.Collisions.Relay.Trigger2D;

namespace Projectile
{
	public class PropagateExplosionSystem
	{
		#region PrivateFields

		private EnemyProjectileDetectionService m_enemyProjectileDetectionService;

		#endregion

		#region Constructors

		public PropagateExplosionSystem(Trigger2DRelay detectionTrigger2DRelay)
		{
			m_enemyProjectileDetectionService = new EnemyProjectileDetectionService(detectionTrigger2DRelay);
		}

		#endregion

		#region PublicMethods

		public void OnTearDown()
		{
			m_enemyProjectileDetectionService.OnTearDown();
		}

		public void OnPropagateExplosion()
		{
			List<EnemyProjectileSystem> detectedProjectileSystemsList =
				m_enemyProjectileDetectionService
					.DetectedObjects
					.ToList();

			for (int i = 0; i < detectedProjectileSystemsList.Count; i++)
			{
				detectedProjectileSystemsList[i].Shooted();
			}
		}

		#endregion
	}
}
