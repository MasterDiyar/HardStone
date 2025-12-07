using Godot;
using System;
using hardenedStone.scripts.entities;
using hardenedStone.scripts.Items;
using hardenedStone.scripts.Items.Suit.ChestPlate;
using hardenedStone.scripts.Items.Suit.Helmet;
using hardenedStone.scripts.Items.Suit.Pants;

public partial class InventoryUi : Control
{
	private int         _pickedSlot    = 0;
	
	public  bool        IsBackPackOpen = false,
				        IsDragging     = false;
	private float       Accel          = 300;
	
	private ItemList    _backpack,
						_hotbar,
						_suit;
	private TextureRect pickedSprite, 
 						dragIcon;
	[Export] 
	private Inventory   inventory;

	public override void _Ready()
	{
		_backpack = GetNode<ItemList>("BackPack");
		_hotbar   = GetNode<ItemList>("HotBar");
		_suit     = GetNode<ItemList>("Suit");
		_backpack.ItemClicked += BackpackOnItemClicked;
		_hotbar.ItemClicked += HotBarOnItemClicked;
		_suit.ItemClicked += SuitOnItemClicked;

		LoadTextures();
	}

	void LoadTextures()
	{
		for (var i = 0; i < inventory.HotBar.Length; i++)
		{
			var rect = _hotbar.GetNode<TextureRect>($"Slot{1 + i}");
			if (inventory.HotBar[i] == null) {
				rect.GetNode<Label>("Label").Text = "";
				continue;
			}
			
			rect.Texture = inventory.HotBar[i].Texture;
			if (inventory.HotBar[i].Count > 0)
				rect.GetNode<Label>("Label").Text = inventory.HotBar[i].Count.ToString();
		}

		for (var i = 0; i < inventory.Backpack.Length; i++)
		{
			var rect = _backpack.GetNode<TextureRect>($"Slot{6 + i}");
			if (inventory.Backpack[i] == null) {
				rect.GetNode<Label>("Label").Text = "";
				continue;
			}
			rect.Texture = inventory.Backpack[i].Texture;
			if (inventory.Backpack[i].Count > 0)
				rect.GetNode<Label>("Label").Text = inventory.Backpack[i].Count.ToString();
		}
		
		if (inventory.helmet != null)
			_suit.GetNode<TextureRect>("Slot16").Texture = inventory.helmet.Texture;
		if (inventory.chest != null)
			_suit.GetNode<TextureRect>("Slot17").Texture = inventory.chest.Texture;
		if (inventory.pants != null)
			_suit.GetNode<TextureRect>("Slot18").Texture = inventory.pants.Texture;
	}

	private void HotBarOnItemClicked(long index, Vector2 atPosition, long mouseButtonIndex) => Click((int)index+1);
	private void BackpackOnItemClicked(long index, Vector2 atPosition, long mouseButtonIndex) => Click((int)index+6);
	private void SuitOnItemClicked(long index, Vector2 atPosition, long mouseButtonIndex) => Click((int)index+16);
	

	private void Click(int slot)
	{
		if (dragIcon != null)return;
		pickedSprite = GetSlot(slot-1);
		_pickedSlot = slot-1;
		
		dragIcon = new TextureRect {
			Texture = pickedSprite.Texture,
			Scale = new Vector2(1.9f, 1.9f),
		};
		
		pickedSprite.Texture = null;
		IsDragging = true;
		AddChild(dragIcon);
	}

	public override void _Process(double delta)
	{
		if (IsBackPackOpen) 
			if (_backpack.Position.Y < 650)
				_backpack.Position += Vector2.Up * Accel * (float)delta;
			else
				_backpack.Position = new Vector2(626, 650);
		else
			if (_backpack.Position.Y > 1100)
				_backpack.Position += Vector2.Down * Accel * (float)delta;
			else
				_backpack.Position = new Vector2(626, 1100);
		Accel -= 100*(float)delta;

		if (Input.IsActionJustPressed("e")) {
			IsBackPackOpen = !IsBackPackOpen;
			Accel = 300;
		}

		if (IsDragging && dragIcon != null) 
			dragIcon.Position = GetLocalMousePosition()-Vector2.One*64;
		
		IsDragging = !Input.IsActionJustReleased("lm") ;

		if (!IsDragging && dragIcon != null)
		{
			var newPos = 0;
			if (dragIcon.Position.X+64 is > 632 and <= 1294)
			{
				newPos = (int)(dragIcon.Position.X + 64 - 632) / 128;
				switch (dragIcon.Position.Y+64) {
					case > 626 and <= 786:
						TryToPut(_pickedSlot ,5+newPos);
						break;
					case > 786 and <= 923:
						TryToPut(_pickedSlot ,10+newPos);
						break;
					case > 925 and <= 1075:
						TryToPut(_pickedSlot ,newPos);
						break;
					default:
						pickedSprite.Texture = dragIcon.Texture;
						break;
				}
			}else if (dragIcon.Position.X+64 is > 1316 and <= 1444) {
				switch (dragIcon.Position.Y + 64) {
					case >=656 and <= 784:
						TryToPut(_pickedSlot ,15);
						break;
					case >=788 and <= 916:
						TryToPut(_pickedSlot ,16);
						break;
					case > 920 and <= 1048:
						TryToPut(_pickedSlot ,17);
						break;
					default:
						pickedSprite.Texture = dragIcon.Texture;
						break;
				}
			}
			else
				pickedSprite.Texture = dragIcon.Texture;
			GD.Print("dragIconPos: ",dragIcon.Position, " newPos: ", newPos, pickedSprite);
			dragIcon.QueueFree();
			dragIcon = null;
			pickedSprite = null;
		}
	}

	public bool TryToPut(int from, int to)
	{
		var fromTextute = GetSlot(from);
		var toTextute   = GetSlot(to);
		var fromItem = GetItem(from);
		var toItem = GetItem(to);
		
		if (dragIcon.Texture != null && toTextute.Texture != null) {
			if (fromItem == null) return false;
			if (fromItem.ID == toItem.ID  ) {
				var total = fromItem.Count + toItem.Count;
				if (total > toItem.MaxCount) {
					fromItem.Count = toItem.MaxCount-toItem.Count;
					toItem.Count = toItem.MaxCount;
				} else {
					toItem.Count = total;
					fromItem = null;
				}
			} else {
				(fromTextute.Texture, toTextute.Texture) = (toTextute.Texture, dragIcon.Texture);
				(fromItem, toItem) = (toItem, fromItem);
			}
		} else if (dragIcon.Texture != null && toTextute.Texture == null) {
			(fromItem, toItem) = (toItem, fromItem);
			(fromTextute.Texture, toTextute.Texture) = (toTextute.Texture, dragIcon.Texture);
		}
		else
			GD.PrintErr("Index out of bounded array at: ", from, " or ", to);
		
		SetItem(to, toItem);
		SetItem(from, fromItem);
		
		LoadTextures();
		return true;
	}

	public TextureRect GetSlot(int slot) {
		GD.Print("getslot: ",slot);
		return slot switch
		{
			>= 0 and < 5 => _hotbar.GetNode<TextureRect>("Slot" + (slot + 1)),
			>= 5 and < 15 => _backpack.GetNode<TextureRect>("Slot" + (slot + 1)),
			>= 15 and < 18 => _suit.GetNode<TextureRect>("Slot" + (slot + 1)),
			_ => _backpack.GetNode<TextureRect>("Slot" + (slot + 1)),
		};
	}

	public Item GetItem(int slot) => slot switch
	{
		>= 0 and < 5 => inventory.HotBar[slot],
		>= 5 and < 15 => inventory.Backpack[slot-5],
		15 => inventory.helmet,
		16 => inventory.chest,
		17 => inventory.pants,
	};

	public void SetItem(int slot, Item item)
	{
		if(item != null)
		{
			GD.Print("setitem: ", slot, item.Name);
			switch (slot) {
				case >= 0 and < 5: inventory.HotBar[slot] = item; break;
		        case >= 5 and < 15: inventory.Backpack[slot-5] = item; break;
		        case 15: if (item is Helmet helmet)inventory.helmet = helmet; break;
		        case 16: if (item is ChestPlate chp)inventory.chest = chp; break;
		        case 17: if (item is Pants pts)inventory.pants = pts; break;
			}
		}else {
			GD.Print("setitem: ", slot, " null");
			switch (slot) {
				case >= 0 and < 5: inventory.HotBar[slot] = null; break;
				case >= 5 and < 15: inventory.Backpack[slot-5] = null; break;
				case 15: inventory.helmet = null; break;
				case 16: inventory.chest = null; break;
				case 17: inventory.pants = null; break;
			}
		}
		
	}
}
