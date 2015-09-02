using com.azi.Image;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace LumixGH4WIC
{

    class WICReadOnlyStreamWrapper : Stream
    {
        IStream stream;
        System.Runtime.InteropServices.ComTypes.STATSTG stat;

        public WICReadOnlyStreamWrapper(IStream stream)
        {
            this.stream = stream;
            stream.Stat(out stat, 0);
        }
        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => false;

        public override long Length
        {
            get
            {
                return stat.cbSize;
            }
        }

        public override long Position
        {
            get
            {
                return Seek(0, SeekOrigin.Current);
            }

            set
            {
                Seek(value, SeekOrigin.Begin);
            }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            byte[] buf;
            if (offset == 0)
                buf = buffer;
            else
                buf = ArraysReuseManager.ReuseOrGetNew<byte>(count);

            IntPtr red = Marshal.AllocHGlobal(4);
            try
            {
                stream.Read(buf, count, red);
                var redint = Marshal.ReadInt32(red);
                if (offset != 0)
                {
                    Array.Copy(buf, 0, buffer, offset, redint);
                    ArraysReuseManager.Release(buf);
                }
                return redint;
            }
            finally
            {
                Marshal.Release(red);
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            IntPtr newptr = Marshal.AllocHGlobal(8);
            try
            {
                stream.Seek(offset, (int)origin, newptr);
                return Marshal.ReadInt64(newptr);
            }
            finally
            {
                Marshal.Release(newptr);
            }
        }

        public override void SetLength(long value)
        {
            stream.SetSize(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
