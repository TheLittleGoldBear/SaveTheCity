using Enemy.Events;
using Enemy.Pool;
using Projectile;

namespace Enemy
{
	public class EnemyProjectileSystem : AbstractProjectileSystem
	{
		#region PrivateFields

		private EnemyProjectileEventBus m_enemyProjectileEventBus;

		#endregion

		#region PublicMethods

		public void Shooted()
		{
			m_enemyProjectileEventBus.Publish(new ShootedEnemyProjectileEvent(this));
			
			ReleaseToPool();
		}

		public EnemyProjectileSystem Inject(EnemyProjectileEventBus enemyProjectileEventBus, EnemyProjectilePool projectilePool)
		{
			base.Inject(projectilePool);

			m_enemyProjectileEventBus = enemyProjectileEventBus;

			return this;
		}

		public override void HitExplosion()
		{
			m_enemyProjectileEventBus.Publish(new EnemyProjectileExplosionEvent(this));
			
			ReleaseToPool();
		}

		#endregion
	}
}
