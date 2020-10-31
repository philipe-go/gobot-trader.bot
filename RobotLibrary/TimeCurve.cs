using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pombot_UI.RobotLibrary
{
    sealed class TimeCurve : StrategyCurve
    {
        protected sealed override void BuildCurve()
        {
            brickFinal = strategy.Temp;

            if (tempTime >= Convert.ToInt32(renkoPeriod))
            {
                periodsPrices.Enqueue(Convert.ToDecimal(strategy.Temp));

                if (periodsPrices.Count() > 1)
                {
                    if ((periodsPrices.Last() - periodsPrices.ElementAt(periodsPrices.Count() - 2) >= 0))
                    {
                        maxPeriods.Enqueue(Convert.ToDecimal((periodsPrices.Last() - periodsPrices.ElementAt(periodsPrices.Count() - 2))));
                        minPeriods.Enqueue(0);
                    }
                    else
                    {
                        maxPeriods.Enqueue(0);
                        minPeriods.Enqueue(Math.Abs(Convert.ToDecimal((periodsPrices.Last() - periodsPrices.ElementAt(periodsPrices.Count() - 2)))));
                    }
                }


                if (historyComplete) BuildProcess();
                tempTime = 0;
            }

            tempTime = (strategy.Now.Second + Convert.ToInt32(tempTime / 60) * 60) - tempTime < 0 ? (strategy.Now.Second + Convert.ToInt32(tempTime / 60) * 60) + 60 : (strategy.Now.Second + Convert.ToInt32(tempTime / 60) * 60);

            CheckPeriodsCount();
        }
    }
}
