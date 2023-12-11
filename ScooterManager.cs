//Filname ScooterManager.cs
//Written by Tom Williams
//Written on 11/03/23

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scooter
{
    using static System.Console;
    internal class ScooterManager
    {
        static void Main(String[] args)
        {

            WriteLine("**Testing starts");
            WriteLine("----------------");

            //testing default constructor
            Scooter scoot = new Scooter();
            int errors = 0;

            Console.WriteLine("**Testing first constructor");

            //checks app name
            if (scoot.App.Equals("Spin"))
            {
                WriteLine(" + Default App property is working");
            }
            else
            {
                WriteLine(" - Default App property failed");
                ++errors;
            }

            //checks scooter model name
            if (scoot.Model.Equals("SegwayES4"))
            {
                WriteLine(" + Default Model property is working");
            }
            else
            {
                WriteLine(" - Default Model property failed");
                ++errors;
            }

            //checks year
            if (scoot.Year.Equals(2021))
            {
                WriteLine(" + Default Year property is working");
            }
            else
            {
                WriteLine(" - Default Year property failed");
                ++errors;
            }

            //checks hours
            if (scoot.Hours.Equals(0.0))
            {
                WriteLine(" + Default Hours property is working");
            }
            else
            {
                WriteLine(" - Default Hours property failed");
                ++errors;
            }

            //checks wph
            if (scoot.Wph.Equals(20.0))
            {
                WriteLine(" + Default WPH property is working");
            }
            else
            {
                WriteLine(" - Default WPH property failed");
                ++errors;
            }

            //checks charge
            if (scoot.Charge.Equals(250.0))
            {
                WriteLine(" + Default Charge property is working");
            }
            else
            {
                WriteLine(" - Default Charge property failed");
                ++errors;
            }

            //checks hoursNextSoftwareUpdate
            if (scoot.HoursNextSoftwareUpdate.Equals(48))
            {
                WriteLine(" + Default hoursNextSoftwareUpdate property is working");
            }
            else
            {
                WriteLine(" - Default hoursNextSoftwareUpdate property failed");
                ++errors;
            }

            WriteLine();

            //Test number 2 based off of baseline given by Professor to test the parameter input for the second constructor
            Scooter s = new Scooter("Bird", "Razor", 2019, 30.0);

            Console.WriteLine("**Testing second constructor");

            //Checks app name
            if (s.App.Equals("Bird"))
            {
                WriteLine(" + App property is working");
            }
            else
            {
                errors++;
            }

            //checks scooter model name
            if (s.Model.Equals("Razor"))
            {
                WriteLine(" + Model property is working");
            }
            else
            {
                ++errors;
            }

            //checks year
            if (s.Year.Equals(2019))
            {
                WriteLine(" + Year property is working");
            }
            else
            {
                ++errors;
            }

            //checks wph
            if (s.Wph.Equals(30.0))
            {
                WriteLine(" + WPH property is working");
            }
            else
            {
                ++errors;
            }

            WriteLine();

            Console.WriteLine("**Testing setters");
            //test setter for app

            scoot.App = "Wheelz";

            if (scoot.App.Equals("Wheelz"))
            {
                WriteLine(" + App setter property is working");
            }
            else
            {
                WriteLine(" - App setter property failed");
                ++errors;
            }

            s.Model = "Griffyn";

            //checks scooter model name
            if (s.Model.Equals("Griffyn"))
            {
                WriteLine(" + Model setter property is working");
            }
            else
            {
                ++errors;
            }

            WriteLine();
            WriteLine("**Testing Scooter Lock/Unlock methods");

            scoot.Charge = 200.0;

            WriteLine("**Scooter locked for testing.");
            scoot.LockScooter(); //lock the scooter to allow charging

            //tests if the lock scooter method works
            if (scoot.IsUnlocked() == false)
            {
                WriteLine(" + LockScooter property works");
                WriteLine(" + IsUnlocked property works");
            }
            else
            {
                ++errors;
            }

            WriteLine("**Scooter unlocked for testing.");
            scoot.UnlockScooter(); //unlock the scooter to allow charging
            //tests if the lock scooter method works
            if (scoot.IsUnlocked() == true)
            {
                WriteLine(" + UnlockScooter property works");
                WriteLine(" + IsUnlocked property works");
            }
            else
            {
                ++errors;
            }

            WriteLine();
            WriteLine("**Testing charge battery method");
            scoot.LockScooter();

            //checks batteryCharge method for nominal input
            scoot.ChargeBattery(10.1);
            if (scoot.Charge == 210.1)
            {
                WriteLine(" + ChargeBattery property works");
            }
            else
            {
                ++errors;
            }

            //checks batteryCharge method for negative input catch
            scoot.ChargeBattery(-1.0);
            if (scoot.Charge == 210.1)
            {
                WriteLine(" + ChargeBattery negative number catch property works");
            }
            else
            {
                ++errors;
            }

            //checks battryCharge method for over charged catch
            scoot.ChargeBattery(9000.1);
            if (scoot.Charge == 250.0)
            {
                WriteLine(" + ChargeBattery overcharge property works");
            }
            else
            {
                ++errors;
            }

            WriteLine();
            WriteLine("**Testing Ride method");

            //tests Ride method
            scoot.UnlockScooter();
            scoot.Hours = 0.0;
            scoot.Charge = 50;
            scoot.Wph = 25.0;

            scoot.Ride(3);

            //checks Ride methods run out of battery catch
            if ((scoot.Charge == 0.0) && (scoot.Hours == 2.0))
            {
                WriteLine(" + Ride battery empty catch property works");
            }
            else
            {
                ++errors;
            }

            //checks if scooter locks after running out of battery
            scoot.Ride(1);
            if (scoot.IsUnlocked() == false)
            {
                WriteLine(" + Run out of battery charge scooter lock property works.");
            }
            else
            {
                ++errors;
            }

            s.UnlockScooter();
            s.Hours = 10.0;
            s.Charge = 250.0;
            s.Wph = 25.0;

            s.Ride(-0.5);

            //checks Ride methods negative input catch
            if ((s.Charge == 250.0) && (s.Hours == 10.0))
            {
                WriteLine(" + Ride negative catch property works");
            }
            else
            {
                ++errors;
            }

            s.Ride(3.5);

            //checks Ride nominal ride
            if ((s.Charge == 162.5) && (s.Hours == 13.5))
            {
                WriteLine(" + Ride nominal addition property works");
            }
            else
            {
                ++errors;
            }

            WriteLine();
            WriteLine("**Testing Inspect and Update methods");

            //tests inspect and check for update methods
            s.LockScooter();
            s.Hours = 20.0;
            s.Inspect();
            s.CheckforUpdate();

            //checks that scooter inspection has updated hours
            if (s.HoursNextSoftwareUpdate == 68.0)
            {
                WriteLine(" + Inspection property works");
            }
            else
            {
                ++errors;
            }

            s.Hours = 69.0;
            s.CheckforUpdate();

            String details = s.ToString();

            WriteLine(details);

            if (details.Equals("Bird Griffyn 2019 Hours: 69.00 Battery charge: 162.50"))
            {
                WriteLine(" + ToString property works");
            }
            else
            {
                ++errors;
            }

            //tests simulation
            WriteLine();
            WriteLine("**Scooter Simulation");
            Scooter userScoot = new Scooter("Condor", "Spinster", 2021, 30.0);

            userScoot.SimulateMultiUserRides(5);//runs simulation

            //shows number of errors
            WriteLine();
            WriteLine("**Testing ends with " + errors + " errors");
            WriteLine("---------------------------------------------------------------");
            WriteLine();

            
        }
    }
}