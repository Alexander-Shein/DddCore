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
    }
}