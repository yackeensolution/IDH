﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.SalesReps
{
    public class GetSalesRepListRequest:BaseRequest
    {
        public Guid UserID { get; set; }

    }
}
