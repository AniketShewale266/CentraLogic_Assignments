public class Item
{
    public int itemID;
    public string itemName;
    public double itemPrice;
    public int itemQuantity;

    public Item(int itemID, string itemName, double itemPrice, int itemQuantity)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemPrice = itemPrice;
        this.itemQuantity = itemQuantity;
    }

}

class Inventory
{
    List<Item> myItems;
    public Inventory() 
    { 
        myItems = new List<Item>();
    }

    public void addItem(Item item)
    {
        myItems.Add(item);
    }

    public void displayItems()
    {
        foreach (var item in myItems)
        {
            Console.WriteLine($"ID: {item.itemID}, Name: {item.itemName}, Price: {item.itemPrice}, Quantity: {item.itemQuantity}");
        }
    }

    public Item findItemById(int id)
    {
        foreach (Item item in myItems)
        {
            if (item.itemID == id)
            {
                return item;
            }
        }
        return null;
    }

    public void UpdateItem(int id, string name, double price, int quantity)
    {
        Item itemToUpdate = findItemById(id);
        if (itemToUpdate != null)
        {
            itemToUpdate.itemName = name;
            itemToUpdate.itemPrice = price;
            itemToUpdate.itemQuantity = quantity;
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }

    public void DeleteItem(int id)
    {
        Item itemToDelete = findItemById(id);
        if (itemToDelete != null)
        {
            myItems.Remove(itemToDelete);
            Console.WriteLine("*** Item deleted successfully. ***");
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }

    public int GenerateNextItemId()
    {
        Random random = new Random();
        return random.Next(1000, 9999);
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Inventory Management System");
        Inventory inventory = new Inventory();
        int ch;
        do
        {
            Console.WriteLine("1. Add a new item");
            Console.WriteLine("2. Display all items");
            Console.WriteLine("3. Find an item by ID");
            Console.WriteLine("4. Update an item's information");
            Console.WriteLine("5. Delete an item");
            Console.WriteLine("6. Exit");

            Console.Write("Enter your Choice: ");
            ch = Convert.ToInt32(Console.ReadLine());
            if (ch == 6)
            {
                break;
            }
            switch (ch)
            {
                case 1:
                    // For Adding
                    int newId = inventory.GenerateNextItemId();
                    Console.Write("Enter item name: ");
                    string newName = Console.ReadLine();
                    Console.Write("Enter item price: ");
                    double newPrice = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Enter item quantity: ");
                    int newQuantity = Convert.ToInt32(Console.ReadLine());
                    Item newItem = new Item(newId, newName, newPrice, newQuantity);
                    inventory.addItem(newItem);

                    Console.WriteLine("*** Item Successfully Added ***");
                    Console.WriteLine("----------------------------");
                    break;
                case 2:
                    // For Reading
                    inventory.displayItems();
                    Console.WriteLine("----------------------------");
                    break;
                case 3:
                    // Finding an item by ID
                    Console.Write("Enter item ID: ");
                    int searchId = Convert.ToInt32(Console.ReadLine());
                  
                    Item foundItem = inventory.findItemById(searchId);
                    if (foundItem != null)
                    {
                            Console.WriteLine($"ID: {foundItem.itemID}, Name: {foundItem.itemName}, Price: {foundItem.itemPrice}, Quantity: {foundItem.itemQuantity}");
                    }
                    else
                    {
                            Console.WriteLine("Item not found.");
                    }
                    Console.WriteLine("----------------------------");
                    break;
                   
                case 4:
                    // Updating an item's information
                    Console.Write("Enter item ID to update: ");
                    int updateId = Convert.ToInt32(Console.ReadLine());
                    
                    Item itemToUpdate = inventory.findItemById(updateId);
                    if (itemToUpdate != null)
                    {
                            Console.Write("Enter new item name: ");
                            string updatedName = Console.ReadLine();
                            Console.Write("Enter new item price: ");
                            double updatedPrice = Convert.ToDouble(Console.ReadLine());
                           
                            Console.Write("Enter new item quantity: ");
                            int updatedQuantity = Convert.ToInt32(Console.ReadLine()); 
                           
                            inventory.UpdateItem(updateId, updatedName, updatedPrice, updatedQuantity);
                            Console.WriteLine("*** Item updated successfully. ***");     
                    }
                    else
                    {
                            Console.WriteLine("Item not found.");
                    }
                    Console.WriteLine("----------------------------");
                    break;

                case 5:
                    // Deleting an item
                    Console.Write("Enter item ID to delete: ");
                    int deleteId = Convert.ToInt32(Console.ReadLine());
                    inventory.DeleteItem(deleteId);
                    Console.WriteLine("----------------------------");
                    break;

                default:
                    Console.WriteLine("Invalid Choice! Please try again...");
                    Console.WriteLine("----------------------------");
                    break;
            }
        } while (ch != 6);
    }
}