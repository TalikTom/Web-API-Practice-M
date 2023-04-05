using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Common
{
    public class Paging
    {

        private int _maxItemsPerPage = 10;

        private int _itemsPerPage = 3;

        public int? Page { get; set; } = 1;



        public int ItemsPerPage
        {
            get => _itemsPerPage;
            set => _itemsPerPage = value > _maxItemsPerPage ? _maxItemsPerPage : value;
        }

        
    }
}
