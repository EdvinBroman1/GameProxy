using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ProxyTibia
{
    class Memory
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr pHandle, IntPtr Address, byte[] Buffer, int Size, IntPtr NumberofBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, Int32 nSize, out IntPtr lpNumberOfBytesWritten);


        private static UInt32 ReadInt32(UInt32 memory_adr)
        {
            byte[] buffer = new byte[sizeof(int)];
            IntPtr NumberOfBytesRead = IntPtr.Zero;

            ReadProcessMemory(Tibia.Client.Handle, (IntPtr)memory_adr, buffer, 4, NumberOfBytesRead);
            return BitConverter.ToUInt32(buffer, 0);
        }

        public static void WriteToPointer(UInt32 StartAddress, IEnumerable<UInt32> offsets)
        {
            UInt32 FinalAddress = StartAddress;

            foreach(var offset in offsets)
            {
                FinalAddress =  ReadInt32(FinalAddress);
                FinalAddress = offset;
            }
        }
    }
}
