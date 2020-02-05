using System;
using System.Collections.Generic;

namespace TinyCrm.Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VatNumber { get; set; }

        /// <summary>/
        /// 
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<Order> Orders { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<ContactPerson> Contacts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Customer()
        {
            Created = DateTimeOffset.Now;
            Orders = new List<Order>();
            Contacts = new List<ContactPerson>();
        }
    }
}
