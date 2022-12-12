using System;
using UnityEngine;

namespace Pooling
{
	public class AbstractMonoBehaviourPoolable<T> : MonoBehaviour, IPoolable where T : IPool<IPoolable>
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
			gameObject.SetActive(true);
		}

		public virtual void OnInsertToPool()
		{
			IsInPool = true;

			CallOnInsertToPool();
			gameObject.SetActive(false);
		}

		public virtual void OnReleaseToPool()
		{
			IsInPool = true;

			CallOnReleaseToPool();

			ReleasedToPool?.Invoke(this);
			ReleasedToPool = null;

			gameObject.SetActive(false);
		}

		public virtual void OnPoolShutdown()
		{ }

		public virtual void ReleaseToPool()
		{
			m_pool.Release(this);
		}

		#endregion

		#region PublicMethods

		public void Inject(T pool)
		{
			m_pool = pool;
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
