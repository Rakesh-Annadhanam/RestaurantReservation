using System;
using System.Collections.Generic;

namespace RestaurantReservationSystem.Objects
{
    public class Restaurant
    {
        #region private fields
        private int reservationMaxId = 0;

        private const string OpeningTime = "9AM";

        private const string ClosingTime = "9PM";

        private List<Reservation> Reservations;

        private List<Table> Tables;
        #endregion

        public Restaurant()
        {
            Reservations = new List<Reservation>();
            InitializeTables();
        }

        #region private methods

        private void InitializeTables()
        {
            Tables = new List<Table>();
            for (int i = 0; i < 1; i++)
            {
                Tables.Add(new Table(4));
            }
        }

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

        private void AssignReservationtoTables(List<Table> tables, Reservation reservation)
        {
            foreach(var table in tables)
            {
                table.AddReservation(reservation);
            }
        }

        private List<Table> GetTablesToReserved(DateTime startTime, DateTime endTime,int persons)
        {
            int accountedPersons = 0;
            List<Table> reservedTables = new List<Table>();
            foreach (var table in Tables)
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

        #endregion

        #region public methods

        public void AddReservation(DateTime starttime, int people, string name, Period period)
        {
            try
            {
                if (starttime.Hour < 9)
                {
                    Console.WriteLine("Please select a start time greater than 9AM");
                    return;
                }

                var endtime = (DateTime)CalculateEndTime(starttime, period);
                if (endtime.Hour > 21)
                {
                    Console.WriteLine("Please select a end time less than 9PM");
                    return;
                }

                var tablesToBeReserved = GetTablesToReserved(starttime, endtime, people);
                var currentReservation = new Reservation(people, starttime, endtime, name);
                currentReservation.AssignTables(tablesToBeReserved);
                AssignReservationtoTables(tablesToBeReserved, currentReservation);
                reservationMaxId++;
                currentReservation.AssignId(reservationMaxId);
                Reservations.Add(currentReservation);
                Console.WriteLine($"Your reservation is successful. Here is Id - {currentReservation.Id}");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Sorry no tables are available at that time");
            }
        }

        public void FindReservationById(int id)
        {
            var reservation =  Reservations.Find(x => x.Id == id);
            if(reservation == null)
                Console.WriteLine("Sorry Can't find the reservation");
            else
            {
                Console.WriteLine($"Here is reservation for id {id}");
                Console.WriteLine(reservation.ToString());
            }
        }

        public void CancelReservationById(int id)
        {
            var reservation = Reservations.Find(x => x.Id == id);
            if (reservation == null)
                Console.WriteLine("Sorry Can't find the reservation");
            else
            {
                Reservations.Remove(reservation);
                foreach(var table in reservation.Tables)
                {
                    table.RemoveReservation(reservation);
                }
                Console.WriteLine($"Reservation with Id {id} is cancelled");

            }
        }

        #endregion 
    }
}
