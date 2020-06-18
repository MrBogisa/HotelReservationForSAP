using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationForSAP.Tests
{
    class BookingAlgorithm
    {
        public static List<(int, int, string)> Algorithm(int size, List<(int, int)> data)
        {
            const int MAXDAYS = 365;
            const int MAXSIZE = 1000;
            int? startDate;
            int? endDate;
            var booking = new List<(int, int, string)>();
            bool isFirstIteration = true;
            if (size < 1 || size > MAXSIZE)
            {
                //I assume that if size parametar is invalid it returns an empty list and bookings cannot be made
                return new List<(int, int, string)>();
            }

            List<Room> hotelRoom = new List<Room>(size);
            for (int i = 0; i < size; i++)
            {
                hotelRoom.Add(new Room());
            }
            foreach (var item in data)
            {
                startDate = item.Item1;
                endDate = item.Item2;
                if (startDate.Value < 0 || startDate.Value > MAXDAYS || endDate.Value < 0 || endDate.Value > MAXDAYS)
                {
                    booking.Add((startDate.Value, endDate.Value, "Decline"));
                }
                else if (isFirstIteration)
                {
                    for (int i = startDate.Value; i <= endDate.Value; i++)
                    {
                        hotelRoom[0].isDayTaken[i] = true;
                    }
                    booking.Add((startDate.Value, endDate.Value, "Accept"));
                    isFirstIteration = false;
                }
                else
                {
                    int startDateToBeforeCounter = 0;
                    int endDateToAfterCounter = 0;
                    int sumOfStartAndEnd;
                    int mostOptimalRange = 367;
                    int hotelRoomNumber = 0;
                    bool isThereATrue = false;
                    bool isRoomFound = false;

                    for (int i = 0; i < hotelRoom.Count; i++)
                    {
                        for (int j = startDate.Value; j <= endDate.Value; j++)
                        {
                            if (hotelRoom[i].isDayTaken[j])
                            {
                                isThereATrue = true;
                                break;
                            }
                        }
                        if (!isThereATrue)
                        {
                            for (int k = startDate.Value - 1; k >= 0; k--)
                            {
                                if (!hotelRoom[i].isDayTaken[k])
                                {
                                    startDateToBeforeCounter++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int k = endDate.Value - 1; k < hotelRoom[i].isDayTaken.Length; k++)
                            {
                                if (!hotelRoom[i].isDayTaken[k])
                                {
                                    endDateToAfterCounter++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            isRoomFound = true;
                            sumOfStartAndEnd = startDateToBeforeCounter + endDateToAfterCounter;
                            if (sumOfStartAndEnd <= mostOptimalRange)
                            {
                                mostOptimalRange = sumOfStartAndEnd;
                                hotelRoomNumber = i;
                            }
                        }
                        isThereATrue = false;
                    }
                    if (isRoomFound)
                    {
                        booking.Add((startDate.Value, endDate.Value, "Accept"));
                        Console.WriteLine("Stavljamo ovo u sobu: {0} ", hotelRoomNumber + 1);
                        for (int l = startDate.Value; l <= endDate.Value; l++)
                        {
                            hotelRoom[hotelRoomNumber].isDayTaken[l] = true;
                        }
                    }
                    else
                    {
                        booking.Add((startDate.Value, endDate.Value, "Decline"));
                    }
                }
            }
            return booking;
        }
    }
}
