using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EComproj.Models
{
    public enum ApprovalStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }

    public enum OrderStatus
    {
        Pending = 0,
        Confirmed = 1,
        Shipped = 2,
        Delivered = 3,
        Cancelled = 4
    }
}