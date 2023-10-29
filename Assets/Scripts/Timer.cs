using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;


    public static class Timer
    {
        private static Stopwatch stopwatch;
        private static TimeSpan pausedTime;
        private static bool isRunning;

        static Timer()
        {
            stopwatch = new Stopwatch();
            pausedTime = TimeSpan.Zero;
            isRunning = false;
        }

        public static void Start()
        {
            if (!isRunning)
            {
                stopwatch.Start();
                isRunning = true;
            }
        }

        public static void Pause()
        {
            if (isRunning)
            {
                stopwatch.Stop();
                pausedTime += stopwatch.Elapsed;
                isRunning = false;
            }
        }

        public static void Reset()
        {
            stopwatch.Reset();
            pausedTime = TimeSpan.Zero;
            isRunning = false;
        }

    public static void Resume()
    {
        if (!isRunning)
        {
            stopwatch.Start();
            isRunning = true;
        }
    }

    public static String ElapsedTime
        {
            get
            {
                if (isRunning)
                {
                    TimeSpan insg = stopwatch.Elapsed + pausedTime;
                    return insg.ToString(@"hh\:mm\:ss");
            }
                else
                {
                    return pausedTime.ToString(@"hh\:mm\:ss"); 
                }
            }
        }
    }


