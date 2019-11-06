/*
	╔══╗─     ╔╗───     ╔═══╗     ╔═══╗     ╔╗╔═╗     ╔═══╗
	║╔╗║─     ║║───     ║╔═╗║     ║╔═╗║     ║║║╔╝     ║╔═╗║
	║╚╝╚╗     ║║───     ║║─║║     ║║─╚╝     ║╚╝╝─     ║╚══╗
	║╔═╗║     ║║─╔╗     ║║─║║     ║║─╔╗     ║╔╗║─     ╚══╗║
	║╚═╝║     ║╚═╝║     ║╚═╝║     ║╚═╝║     ║║║╚╗     ║╚═╝║
	╚═══╝     ╚═══╝     ╚═══╝     ╚═══╝     ╚╝╚═╝     ╚═══╝
	
	* https://github.com/blocksteam/Blocks
	* Contact Us: blocksteamcore@gmail.com
 */
using System;

namespace Blocks.Utils
{
	/// <summary>
	/// Vector in space: X, Y, Z.
	/// </summary>
	public class Vector3 : MetaTag
	{
		public double X, Y, Z;
		
		public Vector3()
		{
			X = Y = Z = 0;
		}
		
		public Vector3(double x, double y, double z)
		{
			X = x;
			Y = y;
			Z = z;
		}
		
		public void Add(double x, double y, double z)
		{
			X += x;
			Y += y;
			Z += z;
		}
		
		public void Sub(double x, double y, double z)
		{
			X -= x;
			Y -= y;
			Z -= z;
		}
		
		public void Multi(double x, double y, double z)
		{
			X *= x;
			Y *= y;
			Z *= z;
		}
		
		public void Div(double x, double y, double z)
		{
			X /= x;
			Y /= y;
			Z /= z;
		}
		
		public double Distance(Vector3 pos) 
		{
	        return Math.Sqrt(this.DistanceSquared(pos));
	    }
	
	    public double DistanceSquared(Vector3 pos) 
	    {
	        return Math.Pow(X - pos.X, 2) + Math.Pow(Y - pos.Y, 2) + Math.Pow(Z - pos.Z, 2);
	    }		
	
		public Vector3 Floor
		{
			get { return new Vector3(Math.Floor(X), Math.Floor(Y), Math.Floor(Z)); }
		}
		
		public Vector3 Abs
		{
			get { return new Vector3(Math.Abs(X), Math.Abs(Y), Math.Abs(Z)); }
		}
		
		public float[] Float
		{
			get { return new float[3] { (float) X, (float) Y, (float) Z }; }
		}
		
		public int FloorX
		{
			get { return (int) Math.Floor(X); }
		}
		
		public int FloorY
		{
			get { return (int) Math.Floor(Y); }
		}
		
		public int FloorZ
		{
			get { return (int) Math.Floor(Z); }
		}
		
		public int ChunkX
		{
			get { return FloorX >> 4; }
		}
		
		
		public int ChunkY
		{
			get { return FloorY >> 4; }
		}
		
		public int ChunkZ
		{
			get { return FloorZ >> 4; }
		}
		
		public override string ToString()
		{
			return string.Format("[Vector3 X={0}, Y={1}, Z={2}]", X, Y, Z);
		}

	}
}
