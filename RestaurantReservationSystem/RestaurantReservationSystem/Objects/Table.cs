using System;
using System.Collections.Generic;

namespace RestaurantReservationSystem.Objects
{
    public class Table
    {
        public int capacity;
        public Table(int capacity)
        {
            this.capacity = capacity;
            reservations = new List<Reservation>();
        }

        private List<Reservation> reservations;

        public void AddReservation(Reservation reservation)
        {
            reservations.Add(reservation);
        }

        public bool CheckAvailability(DateTime startTime,DateTime endTime)
        {
            foreach(var reservation in reservations)
            {
                if ((startTime >= reservation.StartTime && startTime < reservation.EndTime)
                    ||(endTime > reservation.StartTime && endTime <= reservation.EndTime)
                    ||(reservation.StartTime>=startTime && reservation.EndTime <= endTime)
                    )
                    return false;
            }
            return true;
                
        }

        public void RemoveReservation(Reservation reservation)
        {
            reservations.Remove(reservation);
        }
    }
}
