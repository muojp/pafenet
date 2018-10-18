using System;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;

namespace pafenet
{
    public interface ISFDump
    {
        string IDm { get; }
        byte[][] Blocks { get; }
    }

    public struct SFRawBlock
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] data;
    }
    public struct SFDumpRaw
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] IDm;
        public int cnt;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public SFRawBlock[] blocks;

        public ISFDump ToISFDump()
        {
            return new SFDump(this);
        }

        [DllImport("pafe_sfreader")]
        static extern int DumpSFBytes(out SFDumpRaw dest, int systemCode, int serviceCode, int retry);

        public static ISFDump DumpISFBytes(int systemCode, int serviceCode, int retry = 4)
        {
            var r = DumpSFBytes(out var dest, systemCode, serviceCode, retry);
            if (r != 0)
            {
                return null;
            }
            return dest.ToISFDump();
        }

    }

    public class SFDump : ISFDump
    {
        public string IDm { get; private set; }
        public byte[][] Blocks { get; private set; }

        public SFDump(SFDumpRaw raw)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < raw.IDm.Length; i++)
            {
                builder.Append($"{raw.IDm[i]:X02}");
            }
            IDm = builder.ToString();
            Blocks = new byte[raw.cnt][];
            for (int i = 0; i < raw.cnt; i++)
            {
                var len = raw.blocks[i].data.Length;
                Blocks[i] = new byte[len];
                Array.Copy(raw.blocks[i].data, Blocks[i], len);
            }
        }
    }



}