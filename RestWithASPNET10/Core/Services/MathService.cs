using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class MathService
    {
        public decimal Sum(decimal firstNumber, decimal secondNumber) => firstNumber + secondNumber;

        public decimal Sub(decimal firstNumber, decimal secondNumber) => firstNumber - secondNumber;

        public decimal Mult(decimal firstNumber, decimal secondNumber) => firstNumber * secondNumber;

        public double Sqrt(double number) => Math.Sqrt(number);

        public decimal Div(decimal firstNumber, decimal secondNumber)
        {
            if (secondNumber == 0) throw new DivideByZeroException("Cannot divide by zero.");
            return firstNumber / secondNumber;
        }

        public decimal Mean(params decimal[] numbers)
        {
            if (numbers.Length == 0) return 0;
            decimal sum = 0;
            foreach (var number in numbers)
            {
                sum += number;
            }
            return sum / numbers.Length;
        }
    }
}
