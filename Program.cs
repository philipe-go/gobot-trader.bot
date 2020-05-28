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
            //TO BE TESTED SECOND RSI.initialPer = Brick.final;
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

            Console.WriteLine($"...Current Measure: {temp.ToString("0.00")}");
        }

        private static void OnDisconnected(object sender, DdeDisconnectedEventArgs args)
        {
            Console.WriteLine($"Disconnected = IsServerInitiated = {args.IsServerInitiated.ToString()} = IsDisposed= {args.IsDisposed.ToString()}");
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
        //internal static double initialPer;
        private static Queue<double> periods = new Queue<double>(); //periods to be a 5R = 2 pts
        private static Queue<double> maxPeriods = new Queue<double>();
        private static Queue<double> minPeriods = new Queue<double>();
        #endregion

        #region Strategy Variables
        private static double lowMean = 0;
        private static double highMean = 0;
        private static double rsiMean = 0;
        private static double plot3Mean = 0;
        private static bool firstPass = true;
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

                    RSI.maxPeriods.Enqueue(Program.renkoPeriod);
                    RSI.minPeriods.Enqueue(0);
                    periods.Enqueue(Brick.final);
                    if (RSI.periods.Count() > 2) RSI.periods.Dequeue();
                    if (RSI.minPeriods.Count() > Program.historySize) RSI.minPeriods.Dequeue();
                    if (RSI.maxPeriods.Count() > Program.historySize) RSI.maxPeriods.Dequeue();

                    //POSSIBLE NEW solution
                    //periods.Enqueue(Brick.final);
                    //if (periods.Count() > Program.historySize + 1) periods.Dequeue();
                    if (historyComplete) Strategy();
                }
                else if (!RSI.maxCurve) //reversion of descending curve point
                {
                    if (Program.temp - Brick.initial > Program.renkoPeriod)
                    {
                        RSI.maxCurve = true;
                        Brick.final = Brick.initial + Program.renkoPeriod;

                        RSI.maxPeriods.Enqueue(2 * Program.renkoPeriod);
                        RSI.minPeriods.Enqueue(0);
                        periods.Enqueue(Brick.final);
                        if (RSI.periods.Count() > 2) RSI.periods.Dequeue();
                        if (RSI.minPeriods.Count() > Program.historySize) RSI.minPeriods.Dequeue();
                        if (RSI.maxPeriods.Count() > Program.historySize) RSI.maxPeriods.Dequeue();

                        //POSSIBLE NEW solution
                        //periods.Enqueue(Brick.final);
                        //if (periods.Count() > Program.historySize + 1) periods.Dequeue();


                        //Console.WriteLine("===> Reverse Point from LOW to HIGH);
                        if (historyComplete) Strategy();
                    }
                }
                
            }
            if (Program.temp - Brick.final < -Program.renkoPeriod) //complete period for descending curve
            {
                if (RSI.maxCurve) //reversion point of ascending curve
                {
                    if (Program.temp - Brick.initial < -Program.renkoPeriod)
                    {
                        RSI.maxCurve = false;
                        Brick.final = Brick.initial - Program.renkoPeriod;

                        RSI.minPeriods.Enqueue(2 * Program.renkoPeriod);
                        RSI.maxPeriods.Enqueue(0);
                        periods.Enqueue(Brick.final);
                        if (RSI.periods.Count() > 2) RSI.periods.Dequeue();
                        if (RSI.minPeriods.Count() > Program.historySize) RSI.minPeriods.Dequeue();
                        if (RSI.maxPeriods.Count() > Program.historySize) RSI.maxPeriods.Dequeue();

                        //POSSIBLE NEW solution
                        //periods.Enqueue(Brick.final);
                        //if (periods.Count() > Program.historySize + 1) periods.Dequeue();


                        //Console.WriteLine("<=== Reverse Point from HIGH  to LOW);
                        if (historyComplete) Strategy();
                    }
                }
                else if (!RSI.maxCurve) //descending curve
                {
                    Brick.initial -= Program.renkoPeriod;
                    Brick.final = Brick.initial - Program.renkoPeriod;

                    RSI.minPeriods.Enqueue(Program.renkoPeriod);
                    RSI.maxPeriods.Enqueue(0);
                    periods.Enqueue(Brick.final);
                    if (RSI.periods.Count() > 2) RSI.periods.Dequeue();
                    if (RSI.minPeriods.Count() > Program.historySize) RSI.minPeriods.Dequeue();
                    if (RSI.maxPeriods.Count() > Program.historySize) RSI.maxPeriods.Dequeue();

                    //POSSIBLE NEW solution 
                    //periods.Enqueue(Brick.final);
                    //if (periods.Count() > Program.historySize + 1) periods.Dequeue();
                    if (historyComplete) Strategy();
                }
            }

            Console.WriteLine($"Calibration Load:  {maxPeriods.Count() * 100 / Program.historySize} %");
            //Console.WriteLine($"Calibration Load:  {periods.Count() * 100 / Program.historySize} %");

            //Strategy Call
            historyComplete = (maxPeriods.Count() == Program.historySize) ? true : false;
            //historyComplete = (periods.Count() == Program.historySize + 1) ? true : false;
            //if (historyComplete) Strategy();
        }

        private static void Strategy()
        {
            Console.WriteLine("=====Strategy====");

            if (firstPass)
            {
                highMean = maxPeriods.Sum() / Program.historySize; //can use maxPeriods.Average() from LinQ
                lowMean = minPeriods.Sum() / Program.historySize; //can use maxPeriods.Average() from LinQ
            }
            else
            {
                highMean = (highMean * (Program.historySize - 1)/ Program.historySize) + (RSI.periods.Last() - RSI.periods.First() > 0 ? RSI.periods.Last() - RSI.periods.First() : 0) / Program.historySize; //mean of MaxPeriods
                lowMean = (lowMean * (Program.historySize - 1)/ Program.historySize) + (RSI.periods.Last() - RSI.periods.First() < 0 ? Math.Abs(RSI.periods.Last() - RSI.periods.First()) : 0) / Program.historySize; //mean of MinPeriods 
            }

            //double diff = periods.First() - RSI.initialPer;
            //if (diff < 0)
            //{
            //    minPeriods.Enqueue(diff);
            //    maxPeriods.Enqueue(0);
            //}
            //else
            //{
            //    maxPeriods.Enqueue(diff);
            //    minPeriods.Enqueue(0);
            //}

            //for (int i = 1; i < periods.Count(); i++)
            //{
            //    diff = periods.ElementAt(i) - periods.ElementAt(i - 1);
            //    if (diff < 0)
            //    {
            //        minPeriods.Enqueue(diff);
            //        maxPeriods.Enqueue(0);
            //    }
            //    else
            //    {
            //        maxPeriods.Enqueue(diff);
            //        minPeriods.Enqueue(0);
            //    }
            //}
            //highMean = maxPeriods.Sum() / Program.historySize;
            //lowMean = minPeriods.Sum() / Program.historySize;
            //highMean = highMean * (Program.historySize - 1) + (periods.Last() - periods.ElementAt(Program.historySize - 1) > 0 ? periods.Last() - periods.ElementAt(Program.historySize - 1) : 0) / Program.historySize;
            //lowMean = lowMean * (Program.historySize - 1) + (periods.Last() - periods.ElementAt(Program.historySize - 1) < 0 ? periods.Last() - periods.ElementAt(Program.historySize - 1) : 0) / Program.historySize;


            rsiMean = (lowMean != 0) ? 100 - (100 / (1 + (highMean / lowMean))) : (highMean != 0) ? 100 : 50; //RSI index for the historySize (N periods)

            threePerMean.Enqueue(rsiMean);
            if (threePerMean.Count() > Program.plot3Size)
            {
                threePerMean.Dequeue();
                plot3Mean = threePerMean.Average();
                Console.WriteLine($"{DateTime.Now} - RSI20: {rsiMean.ToString("0.00")} --- Plot3: {plot3Mean.ToString("0.00")}\n");
            }

            firstPass = false;

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
