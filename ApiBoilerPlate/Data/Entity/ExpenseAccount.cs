using System;

namespace ApiBoilerPlate.Data.Entity
{
    public class ExpenseAccount : EntityBase
    {
        public long Id { get; set; }
        public long PersonnelId { get; set; }
        public float MaxExpense { get; set; }
    }
}
