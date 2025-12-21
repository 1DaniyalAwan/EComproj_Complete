using EComproj.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComproj.Tests
{
    [TestClass]
    public class ProductApprovalTests
    {
        [TestMethod]
        public void ApprovalStatus_Transitions_Work()
        {
            var p = new Product { ApprovalStatus = ApprovalStatus.Pending };
            p.ApprovalStatus = ApprovalStatus.Approved;
            Assert.AreEqual(ApprovalStatus.Approved, p.ApprovalStatus);
            p.ApprovalStatus = ApprovalStatus.Rejected;
            Assert.AreEqual(ApprovalStatus.Rejected, p.ApprovalStatus);
        }
    }
}
