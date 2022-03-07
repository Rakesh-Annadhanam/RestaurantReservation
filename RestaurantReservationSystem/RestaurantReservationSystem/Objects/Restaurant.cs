using System;
using System.Collections.Generic;

namespace RestaurantReservationSystem.Objects
{
    public class Restaurant
    {
        public Restaurant()
        {
            reservations = new List<Reservation>();
            InitializeTables();
        }
        private List<Reservation> reservations;
        private List<Table> tables;

        private void InitializeTables()
        {
            tables = new List<Table>();
            for(int i = 0; i < 1; i++)
            {
                tables.Add(new Table(4));
            }
        }


        private const string OpeningTime = "9AM";
        private const string ClosingTime = "9PM";

        private int reservationMaxId = 0;
        private DateTime? CalculateEndTime(DateTime startTime, Period period)
        {
            DateTime? endtime = null;
            switch (period)
            {
                case Period.halfHour:
                    endtime = startTime.AddMinutes(30);
                    break;
                case Period.OneHalfHour:
                    endtime = startTime.AddMinutes(90);
                    break;
                case Period.OneHour:
                    endtime = startTime.AddHours(1);
                    break;
                case Period.TwoHours:
                    endtime = startTime.AddHours(2);
                    break;
            }
            return endtime;

        }

        public void AddReservation(DateTime starttime,int people,string name,Period period)
        {
            try
            {
                if(starttime.Hour < 9)
                {
                    Console.WriteLine("Please select a start time greater than 9AM");
                    return;
                }

                var endtime = (DateTime)CalculateEndTime(starttime, period);
                if(endtime.Hour > 21)
                {
                    Console.WriteLine("Please select a end time less than 9PM");
                    return;
                }

                var tables = GetTables(starttime, endtime,people);
                var reservation = new Reservation(people, starttime, endtime, name);
                reservation.AssignTables(tables);
                AssignReservationtoTables(tables, reservation);
                reservationMaxId++;
                reservation.AssignId(reservationMaxId);
                reservations.Add(reservation);
                Console.WriteLine($"Your reservation is successful. Here is Id - {reservation.Id}");

            }
            catch(Exception ex)
            {
                Console.WriteLine("Sorry no tables are available at that time");
            }
        }

        private void AssignReservationtoTables(List<Table> tables, Reservation reservation)
        {
            foreach(var table in tables)
            {
                table.AddReservation(reservation);
            }
        }

        private List<Table> GetTables(DateTime startTime, DateTime endTime,int persons)
        {
            int accountedPersons = 0;
            List<Table> reservedTables = new List<Table>();
            foreach (var table in tables)
            {
                if (table.CheckAvailability(startTime,endTime))
                {
                    accountedPersons += table.capacity;
                }
                reservedTables.Add(table);
                if (accountedPersons >= persons)
                {
                    break; 
                }
            }
            if(accountedPersons >= persons)
            {
                return reservedTables;
            }
            else
            {
                throw new Exception("Not Available");
            }
        }
        public void FindReservationById(int id)
        {
            var reservation =  reservations.Find(x => x.Id == id);
            if(reservation == null)
                Console.WriteLine("Sorry Can't find the reservation");
            else
            {
                Console.WriteLine($"Here is reservation for id {id}");
                Console.WriteLine(reservation.ToString());
            }
        }

        
    }
}
