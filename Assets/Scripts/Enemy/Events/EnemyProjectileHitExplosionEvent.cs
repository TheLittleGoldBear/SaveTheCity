namespace Enemy.Events
{
	public class EnemyProjectileExplosionEvent : IEnemyProjectileEvent
	{
		#region Properties

		public EnemyProjectileSystem EnemyProjectileSystem { get; }

		#endregion

		#region Constructors

		public EnemyProjectileExplosionEvent(EnemyProjectileSystem enemyProjectileSystem)
		{
			EnemyProjectileSystem = enemyProjectileSystem;
		}

		#endregion
	}
}
