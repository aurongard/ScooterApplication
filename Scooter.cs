//Filname Scooter.cs
//Written by Tom Williams
//Written on 11/03/23

namespace Scooter
{
    using System;
    using System.Collections;
    using static System.Console;

    public class Scooter
    {
        private String app; //name of the app used
        private String model; //model of the scooter
        private int year; //year of the scooter
        private double hours; //hours the scooter has been ridden
        private double hoursNextSoftwareUpdate; //hours until the next software update
        private double charge; // charge level of the battery
        private double wph; //watts per hour of the scooter
        private const double BatteryCapacity = 250.0; //max battery capacity
        private const int HoursBetweenUpdates = 48; //how many hours before next update
        private bool scooterLock;

        public Scooter() //default constructor
        {
            app = "Spin";
            model = "SegwayES4";
            year = 2021;
            hours = 0.0;
            wph = 20.0;
            charge = 250.0;
            hoursNextSoftwareUpdate = 48;
            scooterLock = true;
        }
        public Scooter(String App, String Model, int y, double Wph) //constructor with additional input
        {
            app = App;
            model = Model;
            year = y;
            wph = Wph;
            hours = 0.0;
            charge = 250.0;
            hoursNextSoftwareUpdate = 48;
            scooterLock = true;
        }

        //Getters and Setters
        public String App
        {
            get { return app; }
            set { app = value; }
        }
        public String Model
        {
            get { return model; }
            set { model = value; }
        }
        public int Year
        {
            get { return year; }
            set { year = value; }
        }
        public double Hours
        {
            get { return hours; }
            set { hours = value; }
        }
        public double HoursNextSoftwareUpdate
        {
            get { return hoursNextSoftwareUpdate; }
            set { hoursNextSoftwareUpdate = value; }
        }
        public double Charge
        {
            get { return charge; }
            set { charge = value; }
        }
        public double Wph
        {
            get { return wph; }
            set { wph = value; }
        }

        //unlocks scooter
        public void UnlockScooter()
        {
            scooterLock = true;
            WriteLine("{0} {1} {2} unlocked", App, Model, Year);
        }

        //locks scooter
        public void LockScooter()
        {
            scooterLock = false;
            WriteLine("{0} {1} {2} locked", App, Model, Year);
        }

        //checks status of scooter's lock
        public bool IsUnlocked ()
        {
            return scooterLock;
        }

        //method to charge the battery
        public void ChargeBattery(double chargeAmt)
        {
            if (scooterLock == false) //checks if scooter is locked
            {
                if (chargeAmt < 0.0) // if charge amount is negative it gives a warning
                {
                    WriteLine("{0} {1} {2} watts cannot be a negative number - Charge in Battery after being plugged in: {3}", App, Model, Year, Charge);
                }
                else
                {
                    Charge += chargeAmt; //applies the charge amount to the total battery charge.

                    if (Charge > 250.0) //gives warning if battery is overcharged after charging.
                    {
                        Charge = 250.0; //sets charge to max without being over.
                        WriteLine("{0} {1} {2} battery overcharged - Charge in Batter after being plugged in: {3}", App, Model, Year, Charge);
                    }
                    else
                    {
                        WriteLine("{0} {1} {2} added charge {3} - Charge in Batter after being plugged in: {4}", App, Model, Year, chargeAmt, Charge); //if everything is nominal.
                    }
                }
            }
            else
            {
                WriteLine("{0} {1} {2} must be locked to charge battery", App, Model, Year);
            }
        }

        //method to add ridden hours to the scooter and update the battery and ride hours as necessary
        public void Ride(double rideHours)
        {
            double wattsUsed;
            double tempBatt;
            double cutRide;

            if (scooterLock == true) //checks to make sure that scooter is unlocked
            {
                if (rideHours < 0.0) //if ride hours is negative, gives this error and does not update battery charge.
                {
                    WriteLine("{0} {1} {2} watts cannot ride negative hours.", App, Model, Year);
                }
                else
                {
                    wattsUsed = rideHours * Wph; //gets the number of battery power needed for total ride.
                    tempBatt = Charge - wattsUsed; //creates temp variable for how much charge was used

                    if (tempBatt < 0.0) //checks if charge goes into the negatives
                    {
                        scooterLock = false;
                        cutRide = (wattsUsed + tempBatt) / Wph; //calculates the number of hours were ridden before the battery ran out.
                        Charge = 0.0; //sets battery to empty.
                        Hours += cutRide; //adds to hours ridden.
                        WriteLine("{0} {1} {2} ran out of battery after riding {3} hours.", App, Model, Year, cutRide);
                    }
                    else //if scooter is ridden and doesn't run out of battery.
                    {
                        Hours += rideHours; //adds to hours ridden
                        Charge = tempBatt; //updates the battery charge
                        WriteLine("{0} {1} {2} rode {3} hours.", App, Model, Year, rideHours);
                    }
                }
            }
            else
            {
                WriteLine("{0} {1} {2} must be UNLOCKED to ride.", App, Model, Year);
            }
        }

        //method to mark next inspection.
        public void Inspect()
        {
            if (scooterLock == false) 
            {
                double inspection = hours + 48;
                hoursNextSoftwareUpdate = inspection;
                WriteLine("{0} {1} {2} has been updated, next update is: {3}", App, Model, Year, hoursNextSoftwareUpdate);
            }
            else
            {
                WriteLine("{0} {1} {2} must be locked to inspect software.", App, Model, Year);
            }
            
        }

        //method to check for update
        public void CheckforUpdate()
        {
            if (scooterLock == false)
            {
                if (hours >= hoursNextSoftwareUpdate) //if hours ridden are equal to or greater than update marker - need to update
                {
                    WriteLine("{0} {1} {2} - It is time to update software.", App, Model, Year);
                }
                else if (hours < hoursNextSoftwareUpdate) //if hours ridden are less than the update marker - no need to update
                {
                    WriteLine("{0} {1} {2} - Scooter is OK, no need to update software.", App, Model, Year);
                }
            }
            else
            {
                WriteLine("{0} {1} {2} must be locked to check for updates.", App, Model, Year);
            }
        }

        //method to calculate remaining time scooter can be ridden
        private double CalcRange()
        {
            double range = charge / wph;
            return range;
        }

        //method to calculate how much the battery needs to charge
        private double ChargeNeededToFullyChargeBattery()
        {
            double chargeNeeded = BatteryCapacity - charge;
            return chargeNeeded;
        }

        //method string override to return, but not print scooter details
        public override String ToString ()
        {
            String ScooterDetail = App + " " + Model + " " + Year + " Hours: " + hours.ToString("N") + " Battery charge: " + charge.ToString("N");
            return ScooterDetail;
        }

        public void SimulateMultiUserRides(int numberOfUsers)
        {
            double maxTime = CalcRange() * 60; //calls the maximum time the scooter can be ridden and converts it to minutes
            double userTime; //used to convert the random number of minutes back to hours.
            double tempBattUsage; //calculates battery used on the trip
            double wattsUsed; //calculates the watts used
            double cutRide = 0.0; //variable for time ridden when battery dies
            int userRide; //variable for the number of minutes a user rides the scooter for.
            int userTotRide = 0; //holds total user time ridden.
            int maxRide = Convert.ToInt32 (maxTime); //converts maxTime into an int for the random number generator
            int userRiders = 0; //tracks how many riders actually rode the scooter before battery died or no more users
            int i; //index for "for loops" to print user times

            ArrayList riderNum = new ArrayList(); //holds the rider number
            ArrayList riderTimes = new ArrayList(); //holds the rider times

            Random random = new Random();
            if (charge > 0.0)
            {
                while ((maxTime > 0.0) || (numberOfUsers > 0))
                {
                    ++userRiders; //increments the number of riders
                    riderNum.Add(userRiders); //adds rider number to list of riders

                    userRide = random.Next(1, maxRide); //generates number of minutes the ride was for current rider
                    userTime = userRide / 60.0; //converts the minutes back into hours.

                    //riderTimes.Add(userRide); //adds the time ridden for individual rider

                    tempBattUsage = charge - (wph * userTime);

                    if (tempBattUsage < 0.0)
                    {
                        wattsUsed = userTime * wph;
                        cutRide = ((wattsUsed + tempBattUsage) / wph) * 60; //calculates in minutes how long the scooter was ridden before batter dies
                        userTotRide += Convert.ToInt32(cutRide);
                        maxTime -= cutRide; //sets time to 0.0
                    }
                    else
                    {
                        maxTime -= userRide; //reduces time before battery dies
                        userTotRide += userRide;
                    }

                    UnlockScooter(); //unlocks scooter to be ridden.

                    Ride(userTime); //enters in the ride time the user rides the scooter.

                    if (charge == 0.0)
                    {
                        userRide = Convert.ToInt32(cutRide);
                        riderTimes.Add(userRide); //adds the time ridden for individual rider
                        LockScooter(); //locks the scooter.

                        WriteLine();
                        WriteLine("Riders before battery reached 0.0:");
                        WriteLine("--------------------------------------------");

                        for (i = 0; i < riderNum.Count; i++)
                        {
                            WriteLine("Rider #" + riderNum[i] + " rode for " + riderTimes[i] + " minutes.");
                        }
                        WriteLine("--------------------------------------------");
                        WriteLine("Total Riders of " + userRiders + " rode for " + userTotRide + " minutes.");
                        break;
                    }
                    else
                    {
                        riderTimes.Add(userRide); //adds the time ridden for individual rider
                    }

                    LockScooter(); //locks the scooter.
                    --numberOfUsers; //reduces the number of people in que to ride.

                }
            }
            else
            {
                WriteLine("Scooter battery needs to be charged.");
            }
        }
    }
}