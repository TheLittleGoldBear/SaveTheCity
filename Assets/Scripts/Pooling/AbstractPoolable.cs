using System;

namespace Pooling
{
	public class AbstractPoolable<T> : IPoolable where T : IPool<IPoolable>
	{
		#region Events

		public event Action<IPoolable> ReleasedToPool;

		#endregion

		#region Properties

		public bool IsInPool { get; private set; }

		#endregion

		#region PrivateFields

		private T m_pool;
		private bool m_isPooled;

		#endregion

		#region InterfaceImplementations

		public virtual void OnGetFromPool()
		{
			IsInPool = false;

			CallOnGetFromPool();
		}

		public virtual void OnInsertToPool()
		{
			IsInPool = true;

			CallOnInsertToPool();
		}

		public virtual void OnPoolShutdown()
		{ }

		public virtual void OnReleaseToPool()
		{
			ReleasedToPool?.Invoke(this);
			ReleasedToPool = null;

			IsInPool = true;
			CallOnReleaseToPool();
		}

		public virtual void ReleaseToPool()
		{
			m_pool.Release(this);
		}

		#endregion

		#region ProtectedMethods

		protected virtual void CallOnGetFromPool()
		{ }

		protected virtual void CallOnInsertToPool()
		{ }

		protected virtual void CallOnReleaseToPool()
		{ }

		protected virtual void CallOnPoolShutdown()
		{ }

		#endregion
	}
}
