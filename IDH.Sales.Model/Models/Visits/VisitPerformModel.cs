using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDH.Sales.Model.Models.Visits
{
    public class EndVisitRequest : BaseRequest
    {
        [Required]
        public Guid VisitID { get; set; }

        [Required]
        public decimal StartLong { get; set; }

        [Required]
        public decimal StartLat { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public decimal EndLong { get; set; }

        [Required]
        public decimal EndLat { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string StartAddress { get; set; }

        [Required]
        public string EndAddress { get; set; }

        [Required]
        public int FeedbackID { get; set; }

        public string Comment { get; set; }
    }
}