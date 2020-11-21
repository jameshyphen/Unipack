using System;
using System.Collections.Generic;
using System.Text;
using Unipack.Models;
using Xunit;

namespace Unipack.Tests.Services
{
    public class TestVacationList
    {
        [Fact]
        public void Creating_VacationList_Date_Today()
        {
            var VacationList = new VacationList();

            Assert.True(VacationList.AddedOn.Date == DateTime.Today.Date);
        }

        [Fact]
        public void Creating_VacationList_Name_Correct()
        {
            var VacationList = new VacationTask("test");

            Assert.True(VacationList.Name.Equals("test"));
        }
    }
}
