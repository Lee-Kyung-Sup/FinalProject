using System.Collections.Generic;

public class UserManager : SingletoneBase<UserManager>
{
    public List<ItemData> InventoryItems = new ();

    public void LoadItem()
    {
        InventoryItems.Clear();
        
        // 테스트 아이템
        int[] itemList = new[]
        {
            10001001,
            10001003,
            10001004,
            10002001,
            10002002,
            20001003,
            20001004,
            20002001,
            70001002,
            70002001
        };
        
        
        for (int i = 0; i < itemList.Length; i++)
        {
            int id = itemList[i];
            ItemData item = Database.Item.Get(id);
            
            InventoryItems.Add(item);
        }
    }
}