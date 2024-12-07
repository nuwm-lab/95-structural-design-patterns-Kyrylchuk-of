using System;

namespace AnalogToDigitalAdapterPattern
{
    /// <summary>
    /// Інтерфейс цифрового сигналу.
    /// </summary>
    public interface IDigitalSignal
    {
        byte[] GetDigitalData();
    }

    /// <summary>
    /// Клас аналогового сигналу.
    /// </summary>
    public class AnalogSignal
    {
        private readonly double[] _signalData;

        public AnalogSignal(double[] signalData)
        {
            _signalData = signalData;
        }

        /// <summary>
        /// Метод отримання даних аналогового сигналу.
        /// </summary>
        /// <returns>Масив чисел із значеннями аналогового сигналу.</returns>
        public double[] GetAnalogData()
        {
            return _signalData;
        }
    }

    /// <summary>
    /// Адаптер для перетворення аналогового сигналу на цифровий.
    /// </summary>
    public class AnalogToDigitalAdapter : IDigitalSignal
    {
        private readonly AnalogSignal _analogSignal;

        public AnalogToDigitalAdapter(AnalogSignal analogSignal)
        {
            _analogSignal = analogSignal ?? throw new ArgumentNullException(nameof(analogSignal));
        }

        /// <summary>
        /// Метод перетворення аналогового сигналу в цифровий.
        /// </summary>
        /// <returns>Масив байтів цифрового сигналу.</returns>
        public byte[] GetDigitalData()
        {
            double[] analogData = _analogSignal.GetAnalogData();
            byte[] digitalData = new byte[analogData.Length];

            for (int i = 0; i < analogData.Length; i++)
            {
                // Перетворення значень в діапазон [0, 255] (імітуючи цифровий сигнал)
                digitalData[i] = (byte)Math.Clamp((analogData[i] * 255), 0, 255);
            }

            return digitalData;
        }
    }

    /// <summary>
    /// Головний клас програми.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            // Створення аналогового сигналу
            double[] analogData = { 0.1, 0.5, 0.8, 1.0, 0.3, 0.0, 1.2 };
            var analogSignal = new AnalogSignal(analogData);

            Console.WriteLine("Аналоговий сигнал:");
            foreach (var value in analogSignal.GetAnalogData())
            {
                Console.Write($"{value:F2} ");
            }

            // Використання адаптера для перетворення сигналу
            var adapter = new AnalogToDigitalAdapter(analogSignal);
            byte[] digitalData = adapter.GetDigitalData();

            Console.WriteLine("\n\nЦифровий сигнал:");
            foreach (var value in digitalData)
            {
                Console.Write($"{value} ");
            }
        }
    }
}
