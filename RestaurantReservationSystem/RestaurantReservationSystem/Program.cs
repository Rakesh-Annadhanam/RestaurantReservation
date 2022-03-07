using System;
using RestaurantReservationSystem.Objects;

namespace RestaurantReservationSystem
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Initializing Restaurant");
            var restaurant = new Restaurant();

            restaurant.AddReservation(DateTime.Now,4,"Rakesh",Period.OneHour);
            restaurant.AddReservation(DateTime.Now,4, "Rabi", Period.OneHour);
            restaurant.FindReservationById(1);
            restaurant.FindReservationById(2);
        }
    }
}
