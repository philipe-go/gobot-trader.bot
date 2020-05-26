using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDde.Client;

//=profitchart|cot!DOLM20.ULT
//DDE::{app}|{service}!{item}

namespace PombotTest
{
    class Program
    {
        internal static int historySize = 20;
        internal static int plot3Size = 3;
        internal static float renkoPeriod = 5; //for 5R graphs
        internal static float temp;
        internal static bool refreshtemp = false;

        static void Main(string[] args)
        {
            string app = "profitchart";
            string ticker = "DOLM20";
            string col = "ULT";
            string item = $"{ticker}.{col}";
            string service = "cot";

            DdeClient client = new DdeClient(app, service);
            renkoPeriod = (renkoPeriod / 2) - 0.5f;
            client.Disconnected += OnDisconnected;
            client.Connect();
            Console.WriteLine("Connected");


            Console.Write("\nEnter the reference value for the first measurement. \n\t-Enter the initial value of the previous Brick then press ENTER: ");
            Brick.initial = float.Parse(Console.ReadLine());
            //Brick.final = 0;
            Console.Write("\t-Enter the final value of the previous Brick then press ENTER: ");
            Brick.final = float.Parse(Console.ReadLine());
            temp = Brick.final;
            RSI.historyComplete = false;
            refreshtemp = false;
            RSI.maxCurve = Brick.initial < Brick.final ? true : false;


            client.StartAdvise(item, 1, true, 500);
            client.Advise += OnAdvise;

            //Brick.initial = float.Parse(client.Request(item, 500)) / 100;
            //Console.WriteLine($"Initial Request {ticker} - {Brick.initial.ToString("0.00")}");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine($"Initial Brick measurement: {Brick.initial.ToString("0.00")} to {Brick.final.ToString("0.00")}\n");
            Console.WriteLine("\nPress ENTER to exit...");
            Console.ReadKey();
        }

        /********************************/

        private static void OnAdvise(object sender, DdeAdviseEventArgs args)
        {
            ClearLine();
            if (refreshtemp) temp = float.Parse(args.Text) / 100;

            RSI.RSICurve();

            refreshtemp = true;

            Console.WriteLine($"Current Measure: {temp.ToString("0.00")}");
        }

        private static void OnDisconnected(object sender, DdeDisconnectedEventArgs args)
        {
            Console.WriteLine($"OnDisconnected = IsServerInitiated= {args.IsServerInitiated.ToString()} = IsDisposed= {args.IsDisposed.ToString()}");
        }

        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

    }//class Program

    internal abstract class Brick
    {
        internal static float initial, final;
    }//class Brick

    internal abstract class RSI
    {
        #region Curve Variables
        internal static bool maxCurve;
        internal static bool historyComplete;
        internal static bool manualEntry; //-->TO CHECK
        //internal static Queue<double> periods = new Queue<double>(); //periods to be a 5R = 2 pts
        private static Queue<double> maxPeriods = new Queue<double>();
        private static Queue<double> minPeriods = new Queue<double>();
        #endregion

        #region Strategy Variables
        private static double lowMean = 0;
        private static double highMean = 0;
        private static double rsUP = 0;
        private static double rsDown = 0;
        private static double rsiMean = 0;
        private static double plot3Mean = 0;
        private static Queue<double> threePerMean = new Queue<double>();
        #endregion

        //-->TO CHECK if Manual Entry will be implemented
        internal static void ManualEntry()
        {
            Stack<double> Temp = new Stack<double>(); //stack instance to handle backwards period insertion
        }

        internal static void RSICurve()
        {
            Program.ClearLine();
            if (Program.temp - Brick.final > Program.renkoPeriod) //complete period for ascending curve
            { 
                if (RSI.maxCurve) //ascending curve
                {
                    Brick.initial += Program.renkoPeriod;
                    Brick.final = Brick.initial + Program.renkoPeriod;
                    //RSI.periods.Enqueue(Brick.final);
                    RSI.maxPeriods.Enqueue(Program.renkoPeriod);
                    RSI.minPeriods.Enqueue(0);

                    //Console.WriteLine("Added to maxCurve: " + Brick.final.ToString("0.00"));
                    //Console.WriteLine("Added to minCurve: " + 0);
                }
                else if (!RSI.maxCurve) //reversion of descending curve point
                {
                    if (Program.temp - Brick.initial > Program.renkoPeriod)
                    {
                        RSI.maxCurve = true;
                        Brick.final = Brick.initial + Program.renkoPeriod;
                        //RSI.periods.Enqueue(Brick.final);
                        RSI.maxPeriods.Enqueue(2 * Program.renkoPeriod);
                        RSI.minPeriods.Enqueue(0);

                        //Console.WriteLine("===> Reverse Point Added to maxCurve: " + Brick.final.ToString("0.00"));
                        //Console.WriteLine("Added to minCurve: " + 0);
                    }
                }
            }
            if (Program.temp - Brick.final < -Program.renkoPeriod) //complete period for descending curve
            {
                if (RSI.maxCurve) //reversion point of ascending curve
                {
                    if (Program.temp - Brick.initial < -Program.renkoPeriod)
                    {
                        Brick.final = Brick.initial - Program.renkoPeriod;
                        RSI.maxCurve = false;
                        //RSI.periods.Enqueue(Brick.final);
                        RSI.minPeriods.Enqueue(2 * Program.renkoPeriod);
                        RSI.maxPeriods.Enqueue(0);

                        //Console.WriteLine("<=== Reverse Point Added to minCurve: " + Brick.final.ToString("0.00"));
                        //Console.WriteLine("Added to maxCurve: " + 0);
                    }
                }
                else if (!RSI.maxCurve) //descending curve
                {
                    Brick.initial -= Program.renkoPeriod;
                    Brick.final = Brick.initial - Program.renkoPeriod;
                    //RSI.periods.Enqueue(Brick.final);
                    RSI.minPeriods.Enqueue(Program.renkoPeriod);
                    RSI.maxPeriods.Enqueue(0);

                    //Console.WriteLine("Added to minCurve: " + Brick.final.ToString("0.00"));
                    //Console.WriteLine("Added to maxCurve: " + 0);
                }
            }

            Console.WriteLine($"Calibration Load:  {maxPeriods.Count() * 100 / Program.historySize} %");
            //Strategy Call
            historyComplete = (maxPeriods.Count() > Program.historySize) ? true : false;
            if (historyComplete) Strategy();
        }

        private static void Strategy()
        {
            highMean = (maxPeriods.Sum() - maxPeriods.ElementAt(Program.historySize)) / Program.historySize; //mean of MaxPeriods
            lowMean = (minPeriods.Sum() - minPeriods.ElementAt(Program.historySize)) / Program.historySize; //mean of MinPeriods 
            rsUP = (highMean * (Program.historySize - 1) + maxPeriods.ElementAt(Program.historySize)) / Program.historySize;
            rsDown = (lowMean * (Program.historySize - 1) + minPeriods.ElementAt(Program.historySize)) / Program.historySize;
            rsiMean = 100 - (100 / (1 + (rsUP / rsDown))); //RSI index for the historySize (N periods)
            RSI.maxPeriods.Dequeue();
            RSI.minPeriods.Dequeue();
            //RSI.periods.Dequeue();

            threePerMean.Enqueue(rsiMean);
            if (threePerMean.Count() > Program.plot3Size)
            {
                threePerMean.Dequeue();
                plot3Mean = threePerMean.Average();
                Console.WriteLine("=====Strategy====");
                Console.WriteLine($"RSI20: {rsiMean.ToString("0.00")} --- Plot3: {plot3Mean.ToString("0.00")}");
                Console.SetCursorPosition(0, Console.CursorTop - 3);
            }


            //         Inicio
            //                    Plot(RSI(20, 0));             //  IFR de 20 Períodos
            //         Plot2(50);                   //   Linha de 50% de IFR
            //         Plot3(Media(3, RSI(20, 0))); //    MMA de 3 Períodos 
            //         Inicio
            //Verifica se está vendido
            //          Se(IsSold) então
            //           Inicio
            //               Fecha a posição vendida com uma compra caso
            //               o IFR fique acima da MMA 3
            //                    Se(RSI(20, 0) > Media(3, RSI(20, 0)))  então
            //               BuyToCoverAtMarket;
            //         Fim
            //        Verifica se está comprado
            //       Senão Se(IsBought) então
            //        Inicio
            //               Fecha a posição comprada com uma venda caso
            //               o IFR fique abaixo da MMA 31
            //                    Se(RSI(20, 0) < Media(3, RSI(20, 0))) então
            //               SellToCoverAtMarket;
            //         Fim
            //        Verifica se deve abrir uma posição de compra ou venda
            //        utilizando o valor do IFR em relação à MMA 3 e considerando 50 % IFR
            //                  Senão Se(RSI(20, 0) > Media(3, RSI(20, 0))) e(RSI(20, 0) > (50)) então
            //                  BuyAtMarket
            //          Senão Se(RSI(20, 0) < Media(3, RSI(20, 0))) e(RSI(20, 0) < (50)) então
            //          SellShortAtMarket;
            //         Fim; 
            //         Fim;
        }//RSI.Strategy

    }//class RSI

}//namespace
