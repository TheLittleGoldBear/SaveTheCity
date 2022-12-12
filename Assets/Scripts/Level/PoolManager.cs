using Projectile;
using Projectile.Events;
using UnityEngine;

namespace Level
{
	public class PoolManager : MonoBehaviour
	{
		#region SerializeFields

		[SerializeField] private ProjectilePool m_projectilePool;
		[SerializeField] private ProjectilePool m_enemyProjectilePool;

		#endregion

		#region PublicMethods

		public PoolManager Inject(ProjectileEventBus projectileEventBus, ProjectileEventBus enemyProjectileEventBus)
		{
			m_projectilePool.Inject(projectileEventBus);
			m_enemyProjectilePool.Inject(enemyProjectileEventBus);

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
