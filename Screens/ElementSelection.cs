using System;
using System.Linq;
using System.Runtime.InteropServices;
using GeonBit.UI;
using GeonBit.UI.Entities;
using TheWiseOneQuest.Components;
using TheWiseOneQuest.Utils;
using Core = TheWiseOneQuest.TheWiseOneQuest;
namespace TheWiseOneQuest.Screens;

class ElementSelection : Menu
{
	public ElementSelection()
	{
		// Creates the dropdown and adds elements as the dropdown items
		DropDown elementDropdown = new();
		// Get the elements from the Element Enum
		Element[] elements = Enum.GetValues<Element>();
		foreach (var element in elements)
		{
			elementDropdown.AddItem(element.ToString());
		};


		Button confirmElement = new("Confirm Element")
		{   
			Locked = true,
			OnClick = (e) =>
			{
				if (elementDropdown.SelectedValue == null) return;
				Core.battleHandler.PlayerElement = elements.Where(e => e.ToString() == elementDropdown.SelectedValue).First();
				Core.battleHandler.StartBattle();
				RemoveFromParent();
			}
		};

		elementDropdown.OnValueChange = (e) => {
			confirmElement.Locked = false;
		};
		AddChild(elementDropdown);
		AddChild(confirmElement);
	}
}