using Enemy;
using Enemy.Events;
using Enemy.Pool;
using UnityEngine;

namespace Factories
{
	public class EnemyProjectileFactory : AbstractFactory
	{
		#region SerializeFields

		[SerializeField] private EnemyProjectileSystem m_enemyProjectileSystemPrefab;

		#endregion

		#region PublicMethods

		public EnemyProjectileSystem CreateEnemyProjectileSystem(
			EnemyProjectilePool projectilePool,
			Transform root,
			EnemyProjectileEventBus enemyProjectileEventBus
		)
		{
			EnemyProjectileSystem enemyProjectileSystem = Instantiate(m_enemyProjectileSystemPrefab, root);

			enemyProjectileSystem
				.Inject(enemyProjectileEventBus, projectilePool)
				.Initialize();

			return enemyProjectileSystem;
		}

		#endregion
	}
}
