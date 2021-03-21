using System;
using System.Collections;
using System.Collections.Generic;

    public class Id64
    {
        public Id64()
        {
            this.value = 0;
        }
        public Id64(long value)
        {
            this.value = value;
        }
        public Id64(int left, int right)
        {
            this.value = MakeLong(left, right);
        }
        public long value
        {
            get; private set;
        }

        public int LeftValue()
        {
            return (int)(value >> 32);
        }
        public int RightValue()
        {
            return (int)(value & 0xffffffffL);
        }

        public static int LeftValue(long original)
        {
            return (int)(original >> 32);
        }

        public static int RightValue(long original)
        {
            return (int)(original & 0xffffffffL);
        }


        public static long MakeLong(int left, int right)
        {
            //implicit conversion of left to a long
            long res = left;

            //shift the bits creating an empty space on the right
            // ex: 0x0000CFFF becomes 0xCFFF0000
            res = (res << 32);

            //combine the bits on the right with the previous value
            // ex: 0xCFFF0000 | 0x0000ABCD becomes 0xCFFFABCD
            res = res | (long)(uint)right; //uint first to prevent loss of signed bit


            //return the combined result
            return res;
        }

    }

    public static class Uid
    {
        public static long lastTick = 0;
        public static object idGenLock = new Object();
        public static long GetNextId()
        {
            lock (idGenLock)
            {
                long tick = DateTime.UtcNow.Ticks;
                if (lastTick == tick)
                {
                    tick = lastTick + 1;
                }
                lastTick = tick;
                return tick;
            }
        }

        public static int GetNextId32()
        {
            return (int)((GetNextId() << 32) >> 32);
        }
    }


    public static class LoopId
    {
        static ushort counter = default(ushort);
        static object idGenLock = new Object();
        public static ushort minValue = ushort.MinValue;
        public static ushort maxValue = ushort.MaxValue;
        public static ushort NextUShortId()
        {


            lock (idGenLock)
            {
                counter = (ushort)(1 + ((counter + 1) % ushort.MaxValue));
            }

            return (ushort)(counter);
        }
    }


