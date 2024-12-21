﻿using E_Commerce_API.Model;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.DTOs
{
    public class WishlistItemDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
