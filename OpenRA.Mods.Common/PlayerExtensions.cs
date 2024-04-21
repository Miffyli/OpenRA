#region Copyright & License Information
/*
 * Copyright (c) The OpenRA Developers and Contributors
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */
#endregion

using System.Linq;
using OpenRA.Mods.Common.Traits;

namespace OpenRA.Mods.Common
{
	public static class PlayerExtensions
	{
		public static bool HasNoRequiredUnits(this Player player, bool shortGame)
		{
			if (shortGame)
				return !player.World.ActorsHavingTrait<MustBeDestroyed>(t => t.Info.RequiredForShortGame).Any(a => a.Owner == player);
			// TODO note for future: we might want to change this to change the rules when using parallel worlds
			return !player.World.ActorsHavingTrait<MustBeDestroyed>().Any(a => a.Owner == player && a.IsInAnyWorld);
		}
	}
}
