using System;
using congestion.calculator;
using congestion.calculator.Interfaces;
public class CongestionTaxCalculator
{
    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */

    public int GetTax(IVehicle vehicle, DateTime[] dates)
    {
       // DateTime intervalStart = dates[0];
        int totalFee = 0;
        //supposing the dates array is sorted the time difference between 2 elements
        //should not be greater than 60 minutes
        for(int i=0;  i < dates.Length;i++)
        {
      
            int nextFee = GetTollFee(dates[i], vehicle);
            int tempFee = 0;
            long diffInMillies = 0;
            if (i > 0)
            {
                tempFee = GetTollFee(dates[i - 1], vehicle);

                diffInMillies = dates[i].Millisecond - dates[i - 1].Millisecond;
            }
          
            long minutes = diffInMillies / 1000 / 60;

            if (minutes>0 && minutes <= 60 )
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }

        /*the problem with this block is it only checks the diff between the first 2 elements,
        but any 2 elements could be within 60 minutes difference*/

        //foreach (DateTime date in dates)
        //{
        //    int nextFee = GetTollFee(date, vehicle);
        //    int tempFee = GetTollFee(intervalStart, vehicle);

        //    long diffInMillies = date.Millisecond - intervalStart.Millisecond;
        //    long minutes = diffInMillies / 1000 / 60;

        //    if (minutes <= 60)
        //    {
        //        if (totalFee > 0) totalFee -= tempFee;
        //        if (nextFee >= tempFee) tempFee = nextFee;
        //        totalFee += tempFee;
        //    }
        //    else
        //    {
        //        totalFee += nextFee;
        //    }

        //}
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }


    public int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (IsTollFreeDate(date) ||vehicle.IsTollFree()) return 0;

        int hour = date.Hour;
        int minute = date.Minute;
        

        if ((hour == 8 && minute >= 30) || (hour > 8 && hour <= 14))
            {
            return 8;
        }
        else if ((hour==18 && minute>=30) || (hour>18) ||(hour<=5))
        {
            return 0;
        }
        else
        {
            switch (hour)
            {
                case 6:
                    if (minute >= 0 && minute <= 29)
                        return 8;
                    if (minute >= 30 && minute <= 59)
                        return 13;
                    break;

                case 7:
                    return 18;
                 

                case 8:
                    return 13;
                  

                case 15:
                    if (minute >= 0 && minute <= 29)
                        return 13;

                    if (minute >=30 )
                        return 18;
                    break;

                case 16:
                    return 18;

                case 17:
                    return 13;

                case 18:
                    if(minute >= 0 && minute <= 29)
                        return 8;
                    break;
              
            }

            return 0;
        }
  
    }

    private Boolean IsTollFreeDate(DateTime date)
    {
        //The more efficient way is to write a little holliday calculator or use exisiting reliable libraries
        DateTime[] Hollidays = new DateTime[17];
        Hollidays[0] = new DateTime(2013, 1, 1);
        Hollidays[1] = new DateTime(2013, 3, 28);
        Hollidays[2] = new DateTime(2013, 3, 29);
        Hollidays[3] = new DateTime(2013, 4, 1);
        Hollidays[4] = new DateTime(2013, 4, 30);
        Hollidays[5] = new DateTime(2013, 5, 1);
        Hollidays[6] = new DateTime(2013, 5, 8);
        Hollidays[7] = new DateTime(2013, 5, 9);
        Hollidays[8] = new DateTime(2013, 6, 5);
        Hollidays[9] = new DateTime(2013, 6, 6);
        Hollidays[10] = new DateTime(2013, 6, 21);
        Hollidays[11] = new DateTime(2013, 7, 1);
        Hollidays[12] = new DateTime(2013, 11, 1);
        Hollidays[13] = new DateTime(2013, 12, 24);
        Hollidays[14] = new DateTime(2013, 12, 25);
        Hollidays[15] = new DateTime(2013, 12, 26);
        Hollidays[16] = new DateTime(2013, 12, 31);

        int year = date.Year;
        int month = date.Month;
        int day = date.Day; 
        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (Array.Find(Hollidays, element => element.Equals(date.Date)) != null)
            return true;
        else return false;

        //if (year == 2013)
        //{
        //    if (month == 1 && day == 1 ||
        //        month == 3 && (day == 28 || day == 29) ||
        //        month == 4 && (day == 1 || day == 30) ||
        //        month == 5 && (day == 1 || day == 8 || day == 9) ||
        //        month == 6 && (day == 5 || day == 6 || day == 21) ||
        //        month == 7 ||
        //        month == 11 && day == 1 ||
        //        month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
        //    {
        //        return true;
        //    }
        //}
        //return false;
    }

}