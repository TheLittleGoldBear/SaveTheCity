using UnityEngine;
using Utils.R;

namespace Physics.Collisions.Relay.Tag
{
	public static class TagExtension
	{
		#region PublicMethods

		public static bool CompareTag(this Collider2D collider2D, ETag[] acceptedTags)
		{
			for (int i = 0; i < acceptedTags.Length; i++)
			{
				if (collider2D.CompareTag(acceptedTags[i].ToTag()))
				{
					return true;
				}
			}

			return false;
		}

		public static bool CompareTag(this UnityEngine.Collision2D collision2D, ETag[] acceptedTags)
		{
			Rigidbody2D rigidbody2D = collision2D.rigidbody;
			Collider2D collider2D = collision2D.collider;

			for (int i = 0; i < acceptedTags.Length; i++)
			{
				if (rigidbody2D != null)
				{
					if (rigidbody2D.CompareTag(acceptedTags[i].ToTag()))
					{
						return true;
					}
				}
				else
				{
					if (collider2D != null && collider2D.CompareTag(acceptedTags[i].ToTag()))
					{
						return true;
					}
				}
			}

			return false;
		}

		#endregion

		#region PrivateMethods

		private static string ToTag(this ETag tag)
		{
			return tag switch
			{
				ETag.Projectile => R.Tags.PROJECTILE_TAG,
				ETag.EnemyProjectile => R.Tags.ENEMY_PROJECTILE_TAG,
				ETag.Building => R.Tags.BUILDING_TAG,
				ETag.Turret => R.Tags.TURRET_TAG,
				_ => null
			};
		}

		#endregion
	}
}
