using Pooling;
using UnityEngine;

namespace Projectile.Pool
{
	public abstract class AbstractProjectilePool<T> : AbstractPool<T> where T : MonoBehaviour, IPoolable
	{ }
}
