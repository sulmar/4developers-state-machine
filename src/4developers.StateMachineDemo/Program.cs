using Stateless;
using Stateless.Graph;
using System;

namespace _4developers.StateMachineDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello 4developers 2020!");

            LampProxy lamp = new LampProxy();

            Console.WriteLine(lamp.Graph);

            Dump(lamp.State);

            lamp.Push();

            Dump(lamp.State);

            lamp.Push();

            Dump(lamp.State);

            lamp.Push();

            Dump(lamp.State);

            lamp.Push();

            Dump(lamp.State);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }


        static ConsoleColor[] colors = { ConsoleColor.Green, ConsoleColor.Red, ConsoleColor.Yellow };

        private static void Dump(LampState state)
        {
            Console.ForegroundColor = colors[(int)state];
            Console.WriteLine(state);
            Console.ResetColor();
        }
    }

   // dotnet add package Stateless


    public class LampProxy : Lamp
    {
      
        public override LampState State => machine.State;

        private StateMachine<LampState, LampTrigger> machine;


        public string Graph => Stateless.Graph.UmlDotGraph.Format(machine.GetInfo());

        public LampProxy()
        {
            // State = LampState.Off;

            machine = new StateMachine<LampState, LampTrigger>(LampState.Off);

            machine.Configure(LampState.Off)
                .Permit(LampTrigger.Push, LampState.On);

            machine.Configure(LampState.On)
                .Permit(LampTrigger.Push, LampState.Blinking)
                .OnEntry(() => Console.WriteLine("Send sms "), "Send sms");

            machine.Configure(LampState.Blinking)
                .Permit(LampTrigger.Push, LampState.Off);

        }

        public override void Push() => machine.Fire(LampTrigger.Push);
    }

    public class Lamp
    {
        public virtual LampState State { get; set; }

        public virtual void Push()
        {
        }
    }

    public enum LampState
    {
        On,
        Off,
        Blinking
    }

    public enum LampTrigger
    {
        Push
    }
}
