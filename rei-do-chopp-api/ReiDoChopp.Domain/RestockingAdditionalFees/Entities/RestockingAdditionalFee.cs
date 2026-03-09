using ReiDoChopp.Domain.Restockings.Entities;
using System;
using System.Collections.Generic;

namespace ReiDoChopp.Domain.RestockingAdditionalFees.Entities
{
    public class RestockingAdditionalFee
    {
        public int Id { get; protected set; }
        public virtual Restocking Restocking { get; protected set; }
        public int RestockingId { get; protected set; }
        public double Value { get; protected set; }
        public string Description { get; protected set; }
        
        protected RestockingAdditionalFee() {}

        public RestockingAdditionalFee(Restocking restocking, double value, string description)
        {
            SetRestocking(restocking);
            SetValue(value);
            SetDescription(description);
        }

        public virtual void SetRestocking(Restocking restocking)
        {
            Restocking = restocking;
            RestockingId = restocking.Id;
        }
        public virtual void SetValue(double value)
        {
            if (value == null)
                throw new ArgumentException("Required field: Value");
            Value = value;
        }
        public virtual void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Required field: Description");

            Description = description;
        }

    }
}
