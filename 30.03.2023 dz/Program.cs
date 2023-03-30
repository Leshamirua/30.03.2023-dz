using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30._03._2023_dz
{
    public interface IMediator
    {
        void Notify(object sender, string ev);
    }

    class Dispatcher : IMediator
    {
        private Plane plane;
        private Helicopter helicopter;
        private Biplane biplane;

        public Dispatcher(Plane plane, Helicopter helicopter, Biplane biplane)
        {
            this.plane = plane;
            this.plane.SetMediator(this);

            this.helicopter = helicopter;
            this.helicopter.SetMediator(this);

            this.biplane = biplane;
            this.biplane.SetMediator(this);
        }

        public void Notify(object sender, string ev)
        {
            if (ev == "Plane")
            {
                helicopter.FlightWait();
                biplane.FlightWait();
            }

            else if (ev == "Helicopter")
            {
                plane.FlightWait();
                biplane.FlightWait();
            }

            else if (ev == "Biplane")
            {
                plane.FlightWait();
                helicopter.FlightWait();
            }
        }
    }


    class FlyingMachine
    {
        protected IMediator med;

        public FlyingMachine(IMediator med = null)
        {
            this.med = med;
        }
        public void SetMediator(IMediator med)
        {
            this.med = med;
        }
    }

    class Plane : FlyingMachine
    {
        public void FlightStart()
        {
            Console.WriteLine("Plane is starting flyight");
            med.Notify(this, "Plane");
        }
        public void FlightWait()
        {
            Console.WriteLine("Plane is waiting for flyight");
        }
    }

    class Helicopter : FlyingMachine
    {
        public void FlightStart()
        {
            Console.WriteLine("Helicopter is starting flyight");
            med.Notify(this, "Helicopter");
        }
        public void FlightWait()
        {
            Console.WriteLine("Helicopter is waiting for flyight");
        }
    }

    class Biplane : FlyingMachine
    {
        public void FlightStart()
        {
            Console.WriteLine("Biplane is starting flyight");
            med.Notify(this, "Biplane");
        }
        public void FlightWait()
        {
            Console.WriteLine("Biplane is waiting for flyight");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Plane plane = new Plane();
            Helicopter helicopter = new Helicopter();
            Biplane biplane = new Biplane();

            new Dispatcher(plane, helicopter, biplane);

            Console.WriteLine("Client triggers Plane:");
            plane.FlightStart();

            Console.WriteLine();

            Console.WriteLine("Client triggers Biplane:");
            biplane.FlightStart();

        }
    }
}
