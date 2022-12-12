using Enemy.Events;
using Enemy.Pool;
using Projectile;
using Projectile.Pool;
using UnityEngine;

namespace Level
{
	public class PoolManager : MonoBehaviour
	{
		#region SerializeFields

		[SerializeField] private ProjectilePool m_projectilePool;
		[SerializeField] private EnemyProjectilePool m_enemyProjectilePool;

		#endregion

		#region PublicMethods

		public PoolManager Inject(EnemyProjectileEventBus enemyEnemyProjectileEventBus)
		{
			m_enemyProjectilePool.Inject(enemyEnemyProjectileEventBus);

			return this;
		}

		public void Initialize()
		{
			m_projectilePool.Initialize();
			m_enemyProjectilePool.Initialize();
		}

		#endregion
	}
}
