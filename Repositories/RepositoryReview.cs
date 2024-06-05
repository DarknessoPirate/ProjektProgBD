using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.ApplicationServices;
using ProjektProgBD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektProgBD.Repositories
{
    public static class RepositoryReview
    {
        public static bool AddReviewToDb(Review review)
        {
            bool state = false;

            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();

            if (!db.Reviews.Any(r => r.GameId == review.GameId && r.UserId == review.UserId))
            {
                if (review != null)
                {
                    db.Reviews.Add(review);
                    state = true;
                    db.SaveChanges();
                }
            }       
            return state;
        }

        public static bool ModifyReviewInDb(Review newReview, int reviewIdToModify)
        {
            bool state = false;

            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();
            var reviewToModify = db.Reviews.SingleOrDefault(r => r.Id == reviewIdToModify);
            if (reviewToModify != null)
            {
                reviewToModify.Content = newReview.Content;
                reviewToModify.Score = newReview.Score;
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

        public static ObservableCollection<Review> GetAllReviews()
        {
            var list = new ObservableCollection<Review>();
            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();

            var reviews = db.Reviews.Include(g => g.User).Include(g => g.Game).ToList();
            foreach (var review in reviews)
            {
                list.Add(review);
            }
            return list;
        }
    }
}
