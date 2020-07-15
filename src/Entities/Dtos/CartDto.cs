using System.Collections.Generic;

namespace Entities.Dtos
{
    public class CartDto
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value.ToLower(); }
        }

        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    }
}