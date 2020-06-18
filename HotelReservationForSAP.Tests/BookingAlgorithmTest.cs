using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using Xunit;

namespace HotelReservationForSAP.Tests
{
    public class BookingAlgorithmTest
    {
        [Theory]
        [MemberData(nameof(InputData))]
        public void ReturnsCorrectBooking(int size, List<(int,int,string)> expected,List<(int, int)> inputData)
        {
            //Then
            List<(int, int, string)> actual = BookingAlgorithm.Algorithm(size,inputData);
            
            //Assert
            Assert.Equal(expected, actual);
        }
        public static List<object[]> InputData =>
        new List<object[]>
        {
            new object[] { 
                1,
                new List<(int, int, string)> {(-4,2,"Decline")},
                new List<(int, int)>{ (-4,2)}
            },
            new object[] { 
                1,
                new List<(int, int, string)> {(200,400,"Decline")},
                new List<(int, int)>{(200,400)}
            },
            new object[] { 
                3,
                new List<(int, int, string)> {(0,5,"Accept"),(7,13,"Accept"),(3,9,"Accept"),(5,7,"Accept"),(6,6,"Accept"),(0,4,"Accept")},
                new List<(int, int)> { (0, 5 ), (7, 13), (3, 9 ), (5, 7 ), (6, 6), (0, 4) } 
            },
            new object[] { 
                3,
                new List<(int, int, string)> {(1,3,"Accept"),(2,5,"Accept"),(1,9,"Accept"),(0,15,"Decline")},
                new List<(int, int)> { (1, 3), (2, 5), (1, 9), (0, 15) } 
            },
            new object[] { 
                3,
                new List<(int, int, string)> {(1,3,"Accept"),(0,15,"Accept"),(1,9,"Accept"),(2,5,"Decline"),(4,9,"Accept")},
                new List<(int, int)> { (1, 3), (0, 15), (1, 9), (2, 5), (4, 9) } 
            },
            new object[] { 
                2,
                new List<(int, int, string)> {(1,3,"Accept"),(0,4,"Accept"),(2,3,"Decline"),(5,5,"Accept"),(4,10,"Accept"),(10,10,"Accept"),(6,7,"Accept"),(8,10,"Decline"),(8,9,"Accept")},
                new List<(int, int)> { (1, 3), (0, 4), (2, 3), (5, 5), (4, 10), (10, 10), (6, 7), (8, 10), (8, 9) } 
            }
        };
    }
}
