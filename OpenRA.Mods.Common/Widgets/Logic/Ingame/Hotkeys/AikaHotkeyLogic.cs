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

using System.Collections.Generic;
using OpenRA;
using OpenRA.Mods.Common.Lint;
using OpenRA.Widgets;

namespace OpenRA.Mods.Common.Widgets.Logic.Ingame
{
	[ChromeLogicArgsHotkeys("NextParallelWorld", "PreviousParallelWorld")]
	public class AikaHotkeyLogic : ChromeLogic
	{
		[ObjectCreator.UseCtor]
		public AikaHotkeyLogic(Widget widget, ModData modData, World world, Dictionary<string, MiniYaml> logicArgs)
		{
			var nextKey = new HotkeyReference();
			if (logicArgs.TryGetValue("NextParallelWorldKey", out var yaml))
				nextKey = modData.Hotkeys[yaml.Value];

			var prevKey = new HotkeyReference();
			if (logicArgs.TryGetValue("PreviousParallelWorldKey", out yaml))
				prevKey = modData.Hotkeys[yaml.Value];

			var keyhandler = widget.Get<LogicKeyListenerWidget>("WORLD_KEYHANDLER");
			keyhandler.AddHandler(e =>
			{
				if (e.Event == KeyInputEvent.Down)
				{
					var currentWorldIndex = Game.GetFrontendWorldIndex();
					if (nextKey.IsActivatedBy(e))
						currentWorldIndex++;
					else if (prevKey.IsActivatedBy(e))
						currentWorldIndex--;
					if (currentWorldIndex < 0)
						currentWorldIndex = Game.NumParallelWorlds - 1;
					else if (currentWorldIndex >= Game.NumParallelWorlds)
						currentWorldIndex = 0;
					Game.SetFrontendWorldIndex(currentWorldIndex);
				}

				return false;
			});
		}
	}
}
