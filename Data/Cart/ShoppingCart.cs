using System;
using System.Collections.Generic;
using System.Linq;
using eTickets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    { 
        public AppDbContext _context { get; set; }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context =  context;
        }

        // Configure Shopping Cart Session
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services
                .GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            
            var context = services.GetService<AppDbContext>();

            // Check if have CartId Session, generate if not found
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context){ ShoppingCartId = cartId };
        }


        public void AddItemToCart(Movie movie)
        {
            var ShoppingCartItem = _context.ShoppingCartItems
                .FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if(ShoppingCartItem == null)
            {
                ShoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };

                // Add shopping cart to Database
                _context.ShoppingCartItems.Add(ShoppingCartItem);

            }
            else // Increase amount in Shopping Cart
            {
                ShoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }


        public void RemoveItemFromCart(Movie movie)
        {
            // Check if shopping cart item is in DB
            var ShoppingCartItem = _context.ShoppingCartItems
                .FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if(ShoppingCartItem != null)
            {       
                // Reduce items found by one
                if(ShoppingCartItem.Amount > 1)
                {
                    ShoppingCartItem.Amount--;
                }
                else
                {
                    // one found, remove from DB
                    _context.ShoppingCartItems.Remove(ShoppingCartItem);
                }

            }
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = 
                _context.ShoppingCartItems.Where( 
                    n => n.ShoppingCartId == ShoppingCartId
                ).Include(n => n.Movie).ToList());
        }

        public double GetShoppingCartTotal() => _context.ShoppingCartItems
            .Where( n => n.ShoppingCartId == ShoppingCartId)
            .Select( n => n.Movie.Price * n.Amount).Sum();
    }
}