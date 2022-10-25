using Lopushokpplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Lopushokpplication.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private List<TableItem> _tableItems = new();
        private List<string> _sortBoxItems = new();
        private List<string> _filtherBoxItems = new();
        private string _search = null!;
        private string _selectedFilther = null!;
        private string _selectedSort=null!;
        public List<TableItem> TableItems 
        { 
            get => _tableItems; 
            set
            {
                _tableItems = value;
                OnPropertyChanged(nameof(TableItems));
            }
        }
        public List<string> SortBoxItems { get => _sortBoxItems; set => _sortBoxItems = value; }
        public List<string> FiltherBoxItems { get => _filtherBoxItems; set => _filtherBoxItems = value; }
        public string Search
        { 
            get => _search; 
            set
            {
                _search = value;
                SearchTable(value);
                OnPropertyChanged(nameof(Search));
            } 
        }
        public string SelectedFilther 
        {
            get => _selectedFilther; 
            set
            {
                _selectedFilther = value;
                FiltherTable(value);
                OnPropertyChanged(nameof(SelectedFilther));
            }
        }
        public string SelectedSort 
        { 
            get => _selectedSort; 
            set
            {
                _selectedSort = value;
                SortTable(TableItems,value);
                OnPropertyChanged(nameof(SelectedSort));
            }
        }

        public MainWindowViewModel()
        {
            TableItems=GetTableItems();
            SortFiltherItemsUpload();
        }

        #region Заполнение таблицы
        private List<TableItem> GetTableItems()
        {
            using(LopushokAxyonovContext db = new())
            {
                List<TableItem> localItems = new();
                var productMaterials = db.Products
                    .Include(pt => pt.ProductType)
                    .ToList();

                foreach (var item in productMaterials)
                {
                    TableItem tableItem = new TableItem
                    {
                        Title=item.Title,
                        Type=item.ProductType.Title,
                        Article=item.ArticleNumber,
                        Materials=GetMaterialsForProduct(item.Id),
                        //ProductImage=GetImage(item.Image),
                        Cost=item.MinCostForAgent
                    };
                    localItems.Add(tableItem);
                }
                return localItems;
            }
        }
        private Bitmap GetImage(string path)
        {
            if (path=="")
            {
                path = @"\products\picture.png";
            }
            return new Bitmap("." + path);
        }
        private string GetMaterialsForProduct(int productid)
        {
            using (LopushokAxyonovContext db = new LopushokAxyonovContext())
            {
                StringBuilder stringBuilder = new StringBuilder();
                var materials = db.ProductMaterials.Include(m => m.Material)
                    .Where(p=>p.ProductId==productid)
                    .ToList();

                if (materials.Count() == 0)
                {
                    stringBuilder.Append("Отсутствуют");
                    return stringBuilder.ToString();
                }
                foreach (var material in materials)
                {
                    stringBuilder.Append(material.Material.Title+", ");
                }
                stringBuilder.Remove(stringBuilder.Length - 2, 2);
                return stringBuilder.ToString();
            }
        }
        #endregion
        #region фильтр, сортировка, поиск
        private void SortFiltherItemsUpload()
        {
            SortBoxItems.Add("Без сортировки");
            SortBoxItems.Add("По возрастанию");
            SortBoxItems.Add("По убыванию");
            using(LopushokAxyonovContext db = new LopushokAxyonovContext())
            {
                FiltherBoxItems.Add("Без фильтра");
                foreach (var item in db.ProductTypes.ToList())
                {
                    FiltherBoxItems.Add(item.Title);
                }
            }
        }

        private void SortTable(List<TableItem> itemList,string value)
        {
            if (value=="Без сортировки")
            {
                SelectedSort = "";
                TableItems = GetTableItems();
            }
            else if(value=="По убыванию")
            {
                TableItems = itemList.OrderByDescending(p => p.Title).ToList();
            }
            else if (value=="По возрастанию")
            {
                TableItems = itemList.OrderBy(p => p.Title).ToList();
            }
        }
        private void FiltherTable(string value)
        {
            if (value == "Без фильтра")
            {
                SelectedFilther = "";
                TableItems = GetTableItems();
            }
            else
            {
                TableItems = GetTableItems().Where(p=>p.Type==value).ToList();
            }
            SortTable(TableItems,SelectedSort);
        }
        private void SearchTable(string value)
        {
            if (value=="")
            {
                TableItems= GetTableItems();
            }
            TableItems = TableItems.Where(p => p.Title.ToLower().Contains(value.ToLower())).ToList();
            SortTable(TableItems, SelectedSort);
        }
        #endregion
    }

    public class TableItem
    {
        public string Title { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Article { get; set; } = null!;
        public string Materials { get; set; } = null!;
        public Bitmap ProductImage { get; set; } = null!;
        public decimal Cost { get; set; }
    }
}
