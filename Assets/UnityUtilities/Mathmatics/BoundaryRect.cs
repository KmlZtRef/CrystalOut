using System;
using UnityEngine;

namespace UnityUtilities.Mathmatics
{
	[Serializable]
	public struct BoundaryRect
	{
		public int minX;
		public int minY;
		public int maxX;
		public int maxY;
		
		public Vector2Int Min => new Vector2Int(minX, minY);
		public Vector2Int Max => new Vector2Int(maxX, maxY);
		public Vector2Int Size => Max - Min;
		public Vector2 Center => (Vector2)(Min + Max) / 2;

		public BoundaryRect(int minX, int minY, int maxX, int maxY)
		{
			this.minX = minX;
			this.minY = minY;
			this.maxX = maxX;
			this.maxY = maxY;
		}

		public BoundaryRect(Vector2Int min, Vector2Int max)
		{
			this.minX = min.x;
			this.minY = min.y;
			this.maxX = max.x;
			this.maxY = max.y;
		}
	}
}