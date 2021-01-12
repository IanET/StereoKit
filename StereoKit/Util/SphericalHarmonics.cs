﻿using System.Numerics;
using System.Runtime.InteropServices;

namespace StereoKit
{
	/// <summary>A light source used for creating SphericalHarmonics data.</summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct SHLight
	{
		/// <summary>Direction to the light source.</summary>
		public Vector3 directionTo;
		/// <summary>Color of the light! Values here can exceed 1.</summary>
		public Color color;
	}

	/// <summary>Spherical Harmonics are kinda like Fourier, but on a sphere. That
	/// doesn't mean terribly much to me, and could be wrong, but check out [here](http://www.ppsloan.org/publications/StupidSH36.pdf)
	/// for more details about how Spherical Harmonics work in this context!
	/// 
	/// However, the more prctical thing is, SH can be a function that describes a
	/// value over the surface of a sphere! This is particularly useful for lighting,
	/// since you can basically store the lighting information for a space in this 
	/// value! This is often used for lightmap data, or a light probe grid, but StereoKit
	/// just uses a single SH for the entire scene. It's a gross oversimplification, 
	/// but looks quite good, and is really fast! That's extremely great when you're 
	/// trying to hit 60fps, or even 144fps.</summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct SphericalHarmonics
	{
		private Vector3 coefficient1;
		private Vector3 coefficient2;
		private Vector3 coefficient3;
		private Vector3 coefficient4;
		private Vector3 coefficient5;
		private Vector3 coefficient6;
		private Vector3 coefficient7;
		private Vector3 coefficient8;
		private Vector3 coefficient9;

		/// <summary>Look up the color information in a particular direction!</summary>
		/// <param name="normal">The direction to look in. Should be normalized.</param>
		/// <returns>The Color represented by the SH in the given direction.</returns>
		public Color Sample(Vector3 normal) => NativeAPI.sh_lookup(this, normal);

		/// <summary>Creates a SphericalHarmonics approximation of the irradiance given
		/// from a set of directional lights!</summary>
		/// <param name="directional_lights">A list of directional lights!</param>
		/// <returns>A SphericalHarmonics approximation of the irradiance given
		/// from a set of directional lights!</returns>
		public static SphericalHarmonics FromLights(SHLight[] directional_lights)
			=> NativeAPI.sh_create(directional_lights, directional_lights.Length);

	}
}
