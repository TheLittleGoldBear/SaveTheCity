using System;

namespace Pooling
{
	public interface IPoolable
	{
		#region Events

		public event Action<IPoolable> ReleasedToPool;

		#endregion

		#region Properties

		public bool IsInPool { get; }

		#endregion

		#region PublicMethods

		void OnGetFromPool();
		void OnInsertToPool();
		void OnReleaseToPool();
		void OnPoolShutdown();
		void ReleaseToPool();

		#endregion
	}
}
