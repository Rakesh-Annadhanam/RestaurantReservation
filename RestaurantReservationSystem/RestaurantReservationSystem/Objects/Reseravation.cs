using System;
using System.Collections.Generic;

namespace RestaurantReservationSystem.Objects
{
    public class Reservation
    {
        public Reservation(int count,DateTime startTime,DateTime endtme,string name)
        {
            Count = count;
            StartTime = startTime;
            EndTime = endtme;  
            Name = name;
        }

       

        public int Id;
        public int Count;
        public DateTime StartTime;
        public Period Period;
        public string Name;

        public DateTime EndTime { get; private set; }
        public List<Table> Tables { get; set; }

        public void AssignTables(List<Table> tables)
        {
            this.Tables = tables;
        }

        public void AssignId(int id) {
            this.Id = id;
        }

        public override string ToString()
        {
            return $"Id - {Id}\n" +
                   $"Name - {Name}\n" +
                $"Count - {Count}\n" +
                $"Time - {StartTime.ToString()}";
        }

    }
}
