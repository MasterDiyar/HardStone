using Godot;
using System;
using hardenedStone.scripts.entities;
using hardenedStone.scripts.Items;

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
			if (inventory.HotBar[i] == null) continue;
			var rect = _hotbar.GetNode<TextureRect>($"Slot{1 + i}");
			rect.Texture = inventory.HotBar[i].Texture;
			if (inventory.HotBar[i].Count > 0)
				rect.GetNode<Label>("Label").Text = inventory.HotBar[i].Count.ToString();
		}

		for (var i = 0; i < inventory.Backpack.Length; i++)
		{
			if (inventory.Backpack[i] == null) continue;
			var rect = _backpack.GetNode<TextureRect>($"Slot{6 + i}");
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
		pickedSprite = _backpack.GetNode<TextureRect>("Slot"+ (slot));
		_pickedSlot = slot;
		
		dragIcon = new TextureRect {
			Texture = pickedSprite.Texture,
			Scale = new Vector2(3.8f, 3.8f),
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
						_backpack.GetNode<TextureRect>($"Slot{6+newPos}").Texture = dragIcon.Texture;
						break;
					case > 786 and <= 923:
						_backpack.GetNode<TextureRect>($"Slot{11+newPos}").Texture = dragIcon.Texture;
						break;
					case > 925 and <= 1075:
						_hotbar.GetNode<TextureRect>($"Slot{1+newPos}").Texture = dragIcon.Texture;
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
		
		

		return true;
	}

	public TextureRect GetSlot(int slot) => slot switch
		{
			>= 0 and < 5 => _hotbar.GetNode<TextureRect>("Slot" + (slot + 1)),
			>= 5 and < 15 => _backpack.GetNode<TextureRect>("Slot" + (slot + 1)),
			>= 15 and < 18 => _suit.GetNode<TextureRect>("Slot" + (slot + 1)),
			_ => _backpack.GetNode<TextureRect>("Slot" + (slot + 1)),
		};

	public Item GetItem(int slot) => slot switch
	{
		>= 0 and < 5 => inventory.HotBar[slot],
		>= 5 and < 15 => inventory.HotBar[slot],
	};

}
