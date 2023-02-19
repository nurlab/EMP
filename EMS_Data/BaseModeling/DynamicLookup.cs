using EMS.Core.Interfaces;
using EMS.Data.DbModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Data.BaseModeling
{
    public class DynamicLookup : ILookupEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
     }
}
