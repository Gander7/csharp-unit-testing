using System;
using System.Collections.Generic;
using System.Text;

namespace TestNinja
{
    public class DemeritPointsCalculator
    {
        private const int SpeedLimit = 65;
        private const int MaxSpeed = 300;
        private const int MinSpeed = 0;

        public int CalculateDemeritPoints(int speed)
        {
            if (speed < MinSpeed || speed > MaxSpeed)
                throw new ArgumentOutOfRangeException();
            if (speed <= SpeedLimit)
                return 0;

            const int kmPerDemeritPoints = 5;
            var demeritPoints = (speed - SpeedLimit) / kmPerDemeritPoints;

            return demeritPoints;
        }
    }
}
