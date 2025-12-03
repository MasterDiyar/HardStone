import json

def load_items(path):
    try:
        with open(path, "r", encoding="utf-8") as f:
            return json.load(f)
    except FileNotFoundError:
        return []

def save_items(path, items):
    with open(path, "w", encoding="utf-8") as f:
        json.dump(items, f, indent=4, ensure_ascii=False)

def add_item(items_list, id, type, name, texture, max_count, description):
    new_item = {
        "id": id,
        "type": type,
        "name": name,
        "texture": texture,
        "max_count": max_count,
        "description": description
    }
    items_list.append(new_item)

# Использование
path = "scripts/Items/items.json"

items = load_items(path)

string = ""
item_info = []

while string != "exit":
	string = input()
	if len(item_info) == 4:
		item_info.append(int(string))
	else:
		item_info.append(string)
	if len(item_info) == 6:
		add_item(
			items,
			item_info[0],
			item_info[1],
			item_info[2],
			item_info[3],
			item_info[4],
			item_info[5],
		)
		save_items(path, items)
		item_info = []
		
	
