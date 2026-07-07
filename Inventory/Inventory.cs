namespace SharpSurv;

public class Inventory
{
    public InventorySlot[] slots = new InventorySlot[9];
    public int holdingSlot = 0;

    public Inventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new InventorySlot();
        }
    }

    public void Add(TileId tileId, int amount)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].empty && slots[i].tileId == tileId)
            {
                slots[i].amount += amount;
                return;
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].empty)
            {
                slots[i].tileId = tileId;
                slots[i].amount = amount;
                slots[i].empty = false;
                return;
            }
        }
    }

    public void Remove(TileId tileId, int amount)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].empty && slots[i].tileId == tileId && slots[i].amount >= amount)
            {
                slots[i].amount -= amount;

                if (slots[i].amount <= 0)
                {
                    slots[i].empty = true;
                    slots[i].amount = 0;
                }

                return;
            }
        }
    }

    public InventorySlot Get(int index)
    {
        return slots[index];
    }
}