using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pombot_UI.RobotLibrary
{
    sealed class RenkoCurve : StrategyCurve
    {
        protected sealed override void BuildCurve()
        {
            if (strategy.Temp - brickFinal > renkoPeriod) //complete period for ascending curve
            {
                if (maxCurve) //ascending curve
                {
                    brickInitial += renkoPeriod;
                    brickFinal = brickInitial + renkoPeriod;

                    periodsPrices.Enqueue(Convert.ToDecimal(brickFinal));
                    maxPeriods.Enqueue(Convert.ToDecimal(renkoPeriod));
                    minPeriods.Enqueue(0);

                    if (historyComplete) BuildProcess();
                }
                else if (!maxCurve) //reversion of descending curve point
                {
                    if (strategy.Temp - brickInitial > renkoPeriod)
                    {
                        maxCurve = true;
                        brickFinal = brickInitial + renkoPeriod;

                        periodsPrices.Enqueue(Convert.ToDecimal(brickFinal));
                        maxPeriods.Enqueue(Convert.ToDecimal(2 * renkoPeriod));
                        minPeriods.Enqueue(0);

                        if (historyComplete) BuildProcess();
                    }
                }
            }
            if (strategy.Temp - brickFinal < -renkoPeriod) //complete period for descending curve
            {
                if (maxCurve) //reversion point of ascending curve
                {
                    if (strategy.Temp - brickInitial < -renkoPeriod)
                    {
                        maxCurve = false;
                        brickFinal = brickInitial - renkoPeriod;

                        periodsPrices.Enqueue(Convert.ToDecimal(brickFinal));
                        minPeriods.Enqueue(Convert.ToDecimal(2 * renkoPeriod));
                        maxPeriods.Enqueue(0);

                        if (historyComplete) BuildProcess();
                    }
                }
                else if (!maxCurve) //descending curve
                {
                    brickInitial -= renkoPeriod;
                    brickFinal = brickInitial - renkoPeriod;

                    periodsPrices.Enqueue(Convert.ToDecimal(brickFinal));
                    minPeriods.Enqueue(Convert.ToDecimal(renkoPeriod));
                    maxPeriods.Enqueue(0);

                    if (historyComplete) BuildProcess();
                }
            }

            CheckPeriodsCount();
        }
    }
}
