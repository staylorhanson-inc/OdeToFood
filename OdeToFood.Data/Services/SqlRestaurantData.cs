using OdeToFood.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;
        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }
        public void Add(Restaurant restaurant)
        {
            db.Restaurants.Add(restaurant);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
            db.SaveChanges();
        }

        public Restaurant Get(int id)
        {
            return (db.Restaurants.FirstOrDefault(r => r.Id == id));
        }

        public IEnumerable<Restaurant> GetAll()
        {
            //May use the fancy query syntax provided by links
            return from r in db.Restaurants
                   orderby r.Name
                   select r;
        }

        public void Update(Restaurant restaurant)
        {
            //This would not be the best approach in the scenario that you may have multiple users that may be selecting and updating database object property names
            //at the same time. The updates may clash with one another.. In this case you would want to implement a solution that utilizes optimistic concurrenc.
            //With optimistic concurrency you would check that the restaurant object had not changed since you read that restaurant object from the database.
            //It helps to prevent situations where one user overrites the changes made by another users. You can detect those kinds of changes. In order for oc to wor, a number of things have to be in place.
            //you have to walk up to the db context and designate that you have a new entry to track.

            var entry = db.Entry(restaurant); //this is making the dbContext aware of this object and that you want it to track any changes made to this object.
            entry.State = EntityState.Modified; // this lets dbContext know that you are giving it an object that already exists in the database, but I am giving it to the context in a modified state, and those modifications need to be persisted to the database.
            //so when I call SaveChanges, an update statement will be created for the restaurant change record and will make sure that the database data will mach the update changes made to the object.
            
            db.SaveChanges();

        }
    }
}
