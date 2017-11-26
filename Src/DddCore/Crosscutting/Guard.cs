using System;

namespace DddCore.Crosscutting
{
    public static class Guard
    {
        public static void NotNull(object instance, string message)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(message);
            }
        }

        public static void NotNullOrWhiteSpace(string instance, string message)
        {
            if (String.IsNullOrWhiteSpace(instance))
            {
                throw new ArgumentNullException(message);
            }
        }

        public static void Greater(int value, int than, string name)
        {
            if (value <= than)
            {
                throw new ArgumentException($"'{name}' must be integer greater than {than}.");
            }
        }

        public static void True(bool value, string message)
        {
            if (!value)
            {
                throw new ArgumentException(message);
            }
        }
    }
}