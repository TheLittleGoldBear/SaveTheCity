namespace Enemy.Events
{
	public class ShootedEnemyProjectileEvent : IEnemyProjectileEvent
	{
		#region Properties

		public EnemyProjectileSystem EnemyProjectileSystem { get; }

		#endregion

		#region Constructors

		public ShootedEnemyProjectileEvent(EnemyProjectileSystem enemyProjectileSystem)
		{
			EnemyProjectileSystem = enemyProjectileSystem;
		}

		#endregion
	}
}
