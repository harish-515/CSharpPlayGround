using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpPlayGrond
{
    public class TempTracker
    {
        // Fill in the TempTracker class methods below
        private Dictionary<int, int> Temps;

        private int MaxValue;
        private int MinValue;
        private int SumValue;
        private int ModeValue;
        private double MeanValue;

        public TempTracker()
        {
            this.Temps = new Dictionary<int, int>();
            this.SumValue = 0;
            this.MinValue = 111;
            this.MaxValue = -1;
            this.ModeValue = 0;
            this.MeanValue = 0;
        }

        // Records a new temperature
        public void Insert(int temp)
        {
            if (this.Temps.Keys.Contains(temp))
            {
                this.Temps[temp]++;
            }
            else
            {
                this.Temps.Add(temp, 1);
            }

            this.SumValue += temp;
            this.MinValue = this.MinValue > temp ? temp : this.MinValue;
            this.MaxValue = this.MaxValue < temp ? temp : this.MaxValue;
            this.ModeValue = this.Temps.OrderByDescending(key => key.Value).First().Key;
            var occurances = this.Temps.Select((k, v) => v).ToList();
            this.MeanValue = 0; //(double)this.SumValue / occurances;
        }

        // Returns the highest temp we've seen so far
        public int? GetMax()
        {
            if (Temps.Count == 0)
            {
                return null;
            }
            return this.MaxValue;
        }

        // Returns the lowest temp we've seen so far
        public int? GetMin()
        {
            if (Temps.Count == 0)
            {
                return null;
            }
            return this.MinValue;
        }

        // Returns the mean of all temps we've seen so far
        public double? GetMean()
        {
            if (Temps.Count == 0)
            {
                return null;
            }
            return this.MeanValue;
        }

        // Return a mode of all temps we've seen so far
        public int? GetMode()
        {
            if (Temps.Count == 0)
            {
                return null;
            }
            return this.ModeValue;
        }
    }
}