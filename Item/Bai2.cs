using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Item
{
    internal class Bai2
    {
        static void Main(string[] args)
        {
        }
        public Item _item;
        public ItemManager _it;
        [SetUp]
        public void Setup()
        {
            _item = new Item();
        }

        [Test]
        [TestCase(1, "Nguyen")]
        [TestCase(2, "He")]
        [TestCase(3, "")]
        public void Test_Add_Item(int id, string name)
        {
            _it = new ItemManager(0, "A");

            _item.AddItemManager(_it);

            Assert.That(_item.ItemManagers.Contains(_it), Is.True);
        }
        [Test]
        public void Test_Update_Item()
        {
            _it = new ItemManager(0, "A");

            _item.AddItemManager(_it);

            string newName = "A1";

            _item.UpdateItemManager(0, newName);
            Assert.That(_item.ItemManagers.Contains(_it), Is.True);

        }
        [Test]
        public void Test_Delete_Item()
        {

            _it = new ItemManager(0, "A");

            _item.AddItemManager(_it);

            _item.DeleteItemManager(2);
            Assert.That(_item.ItemManagers.Contains(_it), Is.True);
        }

        public class ItemManager
        {
            public List<ItemManager> ItemManagers = new List<ItemManager>();
            public int ID { get; set; }
            public string Name { get; set; }
            public ItemManager(int _ID, string _Name)
            {
                ID = _ID;
                Name = _Name;
            }
        }
        public class Item
        {
            public List<ItemManager> ItemManagers = new List<ItemManager>();

            public void AddItemManager(ItemManager it)
            {
                ItemManagers.Add(it);
            }

            public void UpdateItemManager(int id, string newName)
            {
                var it = ItemManagers.SingleOrDefault(x => x.ID == id);
                if (it != null)
                {
                    it.Name = newName;
                }
            }

            public void DeleteItemManager(int id)
            {
                var it = ItemManagers.FirstOrDefault(x => x.ID == id);
                if (it != null)
                {
                    ItemManagers.Remove(it);
                }
            }
        }

    }
}
