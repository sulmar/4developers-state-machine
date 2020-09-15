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

            Lamp lamp = new Lamp();

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

    public class Lamp
    {
        // public LampState State { get; set; }

        public LampState State => machine.State;

        private StateMachine<LampState, LampTrigger> machine;

        public string Graph => Stateless.Graph.UmlDotGraph.Format(machine.GetInfo());


        public Lamp()
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

        public void Push() => machine.Fire(LampTrigger.Push);


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
