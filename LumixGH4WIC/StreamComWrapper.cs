
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace LumixGH4WIC
{
    public class StreamComWrapper : IStream
    {
        Stream stream;
        public StreamComWrapper(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            this.stream = stream;
        }

        [SecurityCritical]
        public void Read(Byte[] buffer, Int32 bufferSize, IntPtr bytesReadPtr)
        {
            int red = stream.Read(buffer, 0, bufferSize);
            if (bytesReadPtr != IntPtr.Zero)
            {
                Marshal.WriteInt32(bytesReadPtr, red);
            }
        }

        [SecurityCritical]
        public void Seek(Int64 offset, Int32 origin, IntPtr newPositionPtr)
        {
            long position = stream.Seek(offset,(SeekOrigin)origin);

            if (newPositionPtr != IntPtr.Zero)
            {
                Marshal.WriteInt64(newPositionPtr, position);
            }
        }

        public void SetSize(Int64 libNewSize)
        {
            stream.SetLength(libNewSize);
        }

        public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG streamStats, int grfStatFlag)
        {
            streamStats = new System.Runtime.InteropServices.ComTypes.STATSTG();
            streamStats.type = 2;
            streamStats.cbSize = stream.Length;

            // Return access information in grfMode.
            streamStats.grfMode = 0; // default value for each flag will be false
            if (stream.CanRead && stream.CanWrite)
                streamStats.grfMode |= 22;
            else if (stream.CanRead)
                streamStats.grfMode |= 0;
            else if (stream.CanWrite)
                streamStats.grfMode |= 1;
            else
                throw new IOException();
        }

        [SecurityCritical]
        public void Write(Byte[] buffer, Int32 bufferSize, IntPtr bytesWrittenPtr)
        {
            stream.Write(buffer, 0, bufferSize);
            if (bytesWrittenPtr != IntPtr.Zero)
                Marshal.WriteInt32(bytesWrittenPtr, bufferSize);
        }

        public void Clone(out IStream streamCopy)
        {
            streamCopy = null;
            throw new NotSupportedException();
        }

        public void CopyTo(IStream targetStream, Int64 bufferSize, IntPtr buffer, IntPtr bytesWrittenPtr)
        {
            throw new NotSupportedException();
        }

        public void Commit(Int32 flags)
        {
            throw new NotSupportedException();
        }

        public void LockRegion(Int64 offset, Int64 byteCount, Int32 lockType)
        {
            throw new NotSupportedException();
        }

        public void Revert()
        {
            throw new NotSupportedException();
        }

        public void UnlockRegion(Int64 offset, Int64 byteCount, Int32 lockType)
        {
            throw new NotSupportedException();
        }
    }
}