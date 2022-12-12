namespace Pooling
{
	public interface IPool<T> where T : class, IPoolable
	{
		#region PublicMethods

		void Initialize();
		T Get();
		void Insert(T objectToInsert);
		void Release(T objectToRelease);
		void ReleaseAll();
		void Shutdown();

		#endregion
	}
}
