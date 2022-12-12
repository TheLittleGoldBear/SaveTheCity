using Enemy.Events;
using Factories;
using Projectile.Pool;
using UnityEngine;

namespace Enemy.Pool
{
	public class EnemyProjectilePool : AbstractProjectilePool<EnemyProjectileSystem>
	{
		#region SerializeFields

		[SerializeField] protected EnemyProjectileFactory m_enemyProjectileFactory;

		#endregion

		#region PrivateFields

		private EnemyProjectileEventBus m_enemyProjectileEventBus;

		#endregion

		#region PublicMethods

		public EnemyProjectilePool Inject(EnemyProjectileEventBus enemyProjectileEventBus)
		{
			m_enemyProjectileEventBus = enemyProjectileEventBus;

			return this;
		}

		#endregion

		#region ProtectedMethods

		protected override EnemyProjectileSystem CreateObjectPool()
		{
			return m_enemyProjectileFactory.CreateEnemyProjectileSystem(
				this,
				m_poolRoot,
				m_enemyProjectileEventBus
			);
		}

		#endregion
	}
}
