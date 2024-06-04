using Microsoft.Extensions.DependencyInjection;
using ProjektProgBD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgBD.Repositories
{
    public static class RepositoryReview
    {
        public static bool AddReviewToDb(Review review)
        {
            bool state = false;

            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();
            
            if (review != null)
            {
                db.Reviews.Add(review);
                state = true;
                db.SaveChanges();
            }
            
            return state;
        }

        public static bool DeleteReviewFromDb(int id)
        {
            bool state = false;

            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();
            
            var reviewToRemove = db.Reviews.SingleOrDefault(r => r.Id == id);
            if (reviewToRemove != null)
            {
                db.Reviews.Remove(reviewToRemove);
                state = true;
                db.SaveChanges();
            }
            
            return state;
        }

        public static ObservableCollection<Review> GetUserReviewsFromDb(int userId)
        {
            var list = new ObservableCollection<Review>();
            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();
            
            var reviews = db.Users.Where(u => u.Id == userId).SelectMany(u => u.Reviews).ToList();
            foreach (var review in reviews)
            {
                list.Add(review);
            }
            return list;
        }
    }
}
