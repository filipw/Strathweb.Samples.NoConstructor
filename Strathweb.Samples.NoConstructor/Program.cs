using System;
using System.Runtime.CompilerServices;

namespace Strathweb.Samples.NoConstructor
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new TaxCalculator("valid_license");
            Console.WriteLine(calculator.CalculateVAT(11.99));

            var calculator2 = RuntimeHelpers.GetUninitializedObject(typeof(TaxCalculator)) as TaxCalculator;
            Console.WriteLine(calculator2.CalculateVAT(11.99));

            var dog = RuntimeHelpers.GetUninitializedObject(typeof(Dog)) as Dog;
            Console.WriteLine(dog.Bark());
        }
    }

    public class Dog
    {
        private Dog() { }

        public static Dog CreateDog()
        {
            // some validation on how Dog must be created
            return new Dog();
        }

        public string Bark()
        {
            return "woof!";
        }
    }

    public class TaxCalculator
    {
        public TaxCalculator(string license)
        {
            ValidateLicense(license);
        }

        private void ValidateLicense(string license)
        {
            if (string.IsNullOrEmpty(license))
            {
                throw new ArgumentException("Your license is invalid, you stupid developer", nameof(license));
            }
        }

        public double CalculateVAT(double price)
        {
            return Math.Round(price * 0.22, 2);
        }
    }
}
