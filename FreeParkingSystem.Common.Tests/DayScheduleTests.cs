using System;
using System.Collections.Generic;
using FreeParkingSystem.Common.Models;
using Xunit;

namespace FreeParkingSystem.Common.Tests
{
    public class DayScheduleTests
    {
        [Fact]
        public void Is_Open_Now_Returns_True_If_It_Is_Open_24_Hour()
        {
            // arrange  

            // act  
            var daySched = new DaySchedule(DateTime.Now.DayOfWeek);

            // assert  

            Assert.Equal(true, daySched.Is24HourOpen);
            Assert.Equal(true, daySched.IsOpenNow());
        }

        [Fact]
        public void Is_Open_Now_Returns_False_If_Wrong_Day()
        {
            // arrange  

            // act  
            var daySched = new DaySchedule(DateTime.Now.AddDays(1).DayOfWeek);

            // assert  

            Assert.Equal(true, daySched.Is24HourOpen);
            Assert.Equal(false, daySched.IsOpenNow());
        }

        [Fact]
        public void Is_Open_Now_Returns_True_Because_Of_The_Open_Hour_Range()
        {
            // arrange  
            var openHourList = new List<(TimeSpan, TimeSpan)>();
            openHourList.Add((DateTimeOffset.Now.AddHours(-1).TimeOfDay, DateTimeOffset.Now.AddHours(5).TimeOfDay));
            // act  
            var daySched = new DaySchedule(DateTime.Now.DayOfWeek, openHourList);

            // assert  

            Assert.Equal(false, daySched.Is24HourOpen);
            Assert.Equal(true, daySched.IsOpenNow());
        }

        [Fact]
        public void Is_Open_Now_Returns_False_Because_Of_The_Open_Hour_Range()
        {
            // arrange  
            var openHourList = new List<(TimeSpan, TimeSpan)>();
            openHourList.Add((DateTimeOffset.Now.AddHours(1).TimeOfDay, DateTimeOffset.Now.AddHours(2).TimeOfDay));
            // act  
            var daySched = new DaySchedule(DateTime.Now.DayOfWeek, openHourList);

            // assert  

            Assert.Equal(false, daySched.Is24HourOpen);
            Assert.Equal(false, daySched.IsOpenNow());
        }

        [Fact]
        public void Add_Open_Hours_Throws_ExceptionsBecauseOfInvalidTimeRange()
        {
            // arrange  
            var openHourList = new List<(TimeSpan, TimeSpan)>();
            openHourList.Add((new TimeSpan(0, 2, 0, 0), new TimeSpan(0, 8, 0, 0)));

            // act  
            var daySched = new DaySchedule(DateTime.Now.DayOfWeek, openHourList);
            // assert  
            Assert.Throws<ArgumentException>(() =>
            {
                daySched.AddOpenHours(new TimeSpan(0, 23, 0, 0), new TimeSpan(0, 10, 0, 0));
            });
        }

        [Fact]
        public void Add_Open_Hours_Does_Not_Add_Time_Collapsed_Open_Hours()
        {
            // arrange  
            var openHourList = new List<(TimeSpan, TimeSpan)>();
            openHourList.Add((new TimeSpan(0, 2, 0, 0), new TimeSpan(0, 12, 0, 0)));
            var openHourListCount = openHourList.Count;
            // act  
            var daySched = new DaySchedule(DateTime.Now.DayOfWeek, openHourList);

            daySched.AddOpenHours(new TimeSpan(0, 3, 0, 0), new TimeSpan(0, 10, 0, 0));
            // assert  
            Assert.Equal(openHourListCount, daySched.OpenHoursList.Count);
        }
        [Fact]
        public void Add_Open_Hours()
        {
            // arrange  
            var openHourList = new List<(TimeSpan, TimeSpan)>();
            openHourList.Add((new TimeSpan(0, 2, 0, 0), new TimeSpan(0, 4, 0, 0)));
            // act  
            var daySched = new DaySchedule(DateTime.Now.DayOfWeek, openHourList);

            daySched.AddOpenHours(new TimeSpan(0, 5, 0, 0), new TimeSpan(0, 10, 0, 0));
            // assert  
            Assert.Equal(2, daySched.OpenHoursList.Count);
        }

        [Fact]
        public void Add_Open_Hours_Updates_End_Time_Instead_Of_Adding_A_New_One()
        {
            // arrange  
            var openHourList = new List<(TimeSpan, TimeSpan)>();
            openHourList.Add((new TimeSpan(0, 2, 0, 0), new TimeSpan(0, 12, 0, 0)));
            var openHourListCount = openHourList.Count;
            var endTime = new TimeSpan(0, 15, 0, 0);
            // act  
            var daySched = new DaySchedule(DateTime.Now.DayOfWeek, openHourList);

            daySched.AddOpenHours(new TimeSpan(0, 3, 0, 0), endTime);
            // assert  
            Assert.Equal(openHourListCount, daySched.OpenHoursList.Count);
            Assert.Equal(endTime.TotalMilliseconds, daySched.OpenHoursList[0].End.TotalMilliseconds);
        }

        [Fact]
        public void Add_Open_Hours_Updates_Start_Time_Instead_Of_Adding_A_New_One()
        {
            // arrange  
            var openHourList = new List<(TimeSpan, TimeSpan)>();
            openHourList.Add((new TimeSpan(0, 2, 0, 0), new TimeSpan(0, 12, 0, 0)));
            var openHourListCount = openHourList.Count;
            var startTime = new TimeSpan(0, 1, 0, 0);
            var endTime = new TimeSpan(0, 12, 0, 0);
            // act  
            var daySched = new DaySchedule(DateTime.Now.DayOfWeek, openHourList);

            daySched.AddOpenHours(startTime, endTime);
            // assert  
            Assert.Equal(openHourListCount, daySched.OpenHoursList.Count);
            Assert.Equal(startTime.TotalMilliseconds, daySched.OpenHoursList[0].Start.TotalMilliseconds);
        }

        [Fact]
        public void Add_Open_Hours_Start_Time_Equals_End_Time()
        {
            // arrange  
            var openHourList = new List<(TimeSpan, TimeSpan)>();
            openHourList.Add((new TimeSpan(0, 2, 0, 0), new TimeSpan(0, 6, 0, 0)));
            var openHourListCount = openHourList.Count;
            var startTime = new TimeSpan(0, 6, 0, 0);
            var endTime = new TimeSpan(0, 12, 0, 0);
            // act  
            var daySched = new DaySchedule(DateTime.Now.DayOfWeek, openHourList);

            daySched.AddOpenHours(startTime, endTime);
            // assert  
            Assert.Equal(openHourListCount, daySched.OpenHoursList.Count);
            Assert.Equal(endTime.TotalMilliseconds, daySched.OpenHoursList[0].End.TotalMilliseconds);
        }

        [Fact]
        public void Add_Open_Hours_End_Time_Equals_Start_Time()
        {
            // arrange  
            var openHourList = new List<(TimeSpan, TimeSpan)>();
            openHourList.Add((new TimeSpan(0, 2, 0, 0), new TimeSpan(0, 6, 0, 0)));
            var openHourListCount = openHourList.Count;
            var startTime = new TimeSpan(0, 0, 0, 0);
            var endTime = new TimeSpan(0, 2, 0, 0);
            // act  
            var daySched = new DaySchedule(DateTime.Now.DayOfWeek, openHourList);

            daySched.AddOpenHours(startTime, endTime);
            // assert  
            Assert.Equal(openHourListCount, daySched.OpenHoursList.Count);
            Assert.Equal(startTime.TotalMilliseconds, daySched.OpenHoursList[0].Start.TotalMilliseconds);
        }
    }
}
