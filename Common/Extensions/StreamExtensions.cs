// --------------------------------------------------------------------------------
// tailor-made software GmbH
// --------------------------------------------------------------------------------
// project:      Common
// filename:     StreamExtensions.cs
// --------------------------------------------------------------------------------
// 
// Created:      2014-02-15   12:35
// 
// Last changed: 2014-02-15   12:37
//               Torsten Vogt (tvo)
// --------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace TMS.Common.Extensions
{
    /// <summary>
    /// </summary>
    public static class StreamExtensions
    {
        private const string CXmlString = @"(<\?xml).+(\?>)";


        /// <summary>
        ///     Returns true if the data in the stream is empty or data is only the XML - definition.
        /// </summary>
        /// <param name="stream">The stream which holds the data which will be analyzed.</param>
        /// <returns>True, if the data of the stream is empty or only the head definition, else false.</returns>
        public static bool IsEmptyXmlStream(this Stream stream)
        {
            var ret = true;
            var pos = 0L;

            if (stream.CanSeek)
            {
                pos = stream.Position;
                stream.Position = 0;
            }

            var bytes = new byte[4096];
            int numBytes;
            var sb = new StringBuilder();

            while ((numBytes = stream.Read(bytes, 0, 4096)) > 0)
            {
                var ms = new MemoryStream();
                ms.Write(bytes, 0, numBytes);
                ms.Flush();
                ms.Position = 0;
                sb.Append(ms.ReadToEnd().TrimClean());

                if (!string.IsNullOrEmpty(Regex.Replace(sb.ToString(), CXmlString, "").TrimClean()))
                    ret = false;

                break;
            }

            if (stream.CanSeek)
                stream.Position = pos;

            return ret;
        }

        /// <summary>
        ///     Write data from a stream to disk without closing the stream
        /// </summary>
        /// <param name="stream">Stream which will be written to disk.</param>
        /// <param name="fileName"></param>
        public static void WriteToFile(this Stream stream, string fileName)
        {
            WriteToFile(stream, fileName, 4096, null);
        }

        /// <summary>
        ///     Write data from a stream to disk without closing the stream
        /// </summary>
        /// <param name="stream">Stream which will be written to disk.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="encoding">The encoding.</param>
        public static void WriteToFile(this Stream stream, string fileName, Encoding encoding)
        {
            WriteToFile(stream, fileName, 4096, encoding);
        }

        /// <summary>
        ///     Write data from a stream to disk without closing the stream
        /// </summary>
        /// <param name="stream">Stream which will be written to disk.</param>
        /// <param name="fileName"></param>
        /// <param name="bufferSize"></param>
        public static void WriteToFile(this Stream stream, string fileName, int bufferSize)
        {
            WriteToFile(stream, fileName, bufferSize, null);
        }

        /// <summary>
        ///     Write data from a stream to disk without closing the stream
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="encoding">The encoding.</param>
        public static void WriteToFile(this Stream stream, string fileName, int bufferSize, Encoding encoding)
        {
            using (var w = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                WriteTo(stream, w, 4096, true, encoding);
        }

        /// <summary>
        ///     Writes data from one stream to another stream using default buffersize of 4K
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="outputStream"></param>
        public static void WriteTo(this Stream stream, Stream outputStream)
        {
            WriteTo(stream, outputStream, 4096, true, null);
        }

        /// <summary>
        ///     Writes to.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="outputStream">The output stream.</param>
        /// <param name="encoding">The encoding.</param>
        public static void WriteTo(this Stream stream, Stream outputStream, Encoding encoding)
        {
            WriteTo(stream, outputStream, 4096, true, encoding);
        }

        /// <summary>
        ///     Writes data from one stream to another stream using a user defined buffersize
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="outputStream"></param>
        /// <param name="bufferSize"></param>
        public static void WriteTo(this Stream stream, Stream outputStream, int bufferSize)
        {
            WriteTo(stream, outputStream, bufferSize, true, null);
        }

        /// <summary>
        ///     Writes to.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="outputStream">The output stream.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="encoding">The encoding.</param>
        public static void WriteTo(this Stream stream, Stream outputStream, int bufferSize, Encoding encoding)
        {
            WriteTo(stream, outputStream, bufferSize, true, encoding);
        }

        /// <summary>
        ///     Writes to.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="outputStream">The output stream.</param>
        /// <param name="startFromBegin">
        ///     if set to <c>true</c> reads data from start of stream. If set to <c>false</c> read data
        ///     from actual position.
        /// </param>
        public static void WriteTo(this Stream stream, Stream outputStream, bool startFromBegin)
        {
            WriteTo(stream, outputStream, 4096, startFromBegin, null);
        }

        /// <summary>
        ///     Writes to.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="outputStream">The output stream.</param>
        /// <param name="startFromBegin">if set to <c>true</c> [start from begin].</param>
        /// <param name="encoding">The encoding.</param>
        public static void WriteTo(this Stream stream, Stream outputStream, bool startFromBegin, Encoding encoding)
        {
            WriteTo(stream, outputStream, 4096, startFromBegin, encoding);
        }

        /// <summary>
        ///     Writes to.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="outputStream">The output stream.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="startFromBegin">
        ///     if set to <c>true</c> reads data from start of stream. If set to <c>false</c> read data
        ///     from actual position.
        /// </param>
        public static void WriteTo(this Stream stream, Stream outputStream, int bufferSize, bool startFromBegin)
        {
            WriteTo(stream, outputStream, bufferSize, startFromBegin, null);
        }

        /// <summary>
        ///     Writes to.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="outputStream">The output stream.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="startFromBegin">if set to <c>true</c> [start from begin].</param>
        /// <param name="encoding">The encoding.</param>
        public static void WriteTo(this Stream stream, Stream outputStream, int bufferSize, bool startFromBegin,
            Encoding encoding)
        {
            if (stream.CanSeek && startFromBegin)
                stream.Position = 0;

            var writer = (encoding == null) ? new StreamWriter(outputStream) : new StreamWriter(outputStream, encoding);
            var buffer = new char[bufferSize];
            var reader = new StreamReader(stream);
            int numBytes;

            while ((numBytes = reader.Read(buffer, 0, bufferSize)) > 0)
                writer.Write(buffer, 0, numBytes);

            writer.Flush();
        }

        /// <summary>
        ///     Writes data from one stream to another stream using default buffersize of 4K
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="outputStream"></param>
        public static void WriteToWithoutXmlIdentifier(this Stream stream, Stream outputStream)
        {
            WriteToWithoutXmlIdentifier(stream, outputStream, 4096, true);
        }

        /// <summary>
        ///     Writes data from one stream to another stream using a user defined buffersize
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="outputStream"></param>
        /// <param name="bufferSize"></param>
        public static void WriteToWithoutXmlIdentifier(this Stream stream, Stream outputStream, int bufferSize)
        {
            WriteToWithoutXmlIdentifier(stream, outputStream, bufferSize, true);
        }

        /// <summary>
        ///     Writes to.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="outputStream">The output stream.</param>
        /// <param name="startFromBegin">
        ///     if set to <c>true</c> reads data from start of stream. If set to <c>false</c> read data
        ///     from actual position.
        /// </param>
        public static void WriteToWithoutXmlIdentifier(this Stream stream, Stream outputStream, bool startFromBegin)
        {
            WriteToWithoutXmlIdentifier(stream, outputStream, 4096, startFromBegin);
        }

        /// <summary>
        ///     Writes to.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="outputStream">The output stream.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="startFromBegin">
        ///     if set to <c>true</c> reads data from start of stream. If set to <c>false</c> read data
        ///     from actual position.
        /// </param>
        public static void WriteToWithoutXmlIdentifier(this Stream stream, Stream outputStream, int bufferSize,
            bool startFromBegin)
        {
            if (stream.CanSeek && startFromBegin)
                stream.Position = 0;

            var reader = new StreamReader(stream);
            var writer = new StreamWriter(outputStream);

            while (true)
            {
                var line = reader.ReadLine();

                if (line == null)
                    break;

                var newLine = Regex.Replace(line, CXmlString, "").TrimClean();

                if (!string.IsNullOrEmpty(newLine))
                    writer.WriteLine(newLine);
            }

            writer.Flush();
        }

        /// <summary>
        ///     Finds the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        public static int Find(this Stream stream, string search)
        {
            return Find(stream, search, (4096 < (search.Length + 1)) ? search.Length + 1 : 4096, true);
        }

        /// <summary>
        ///     Finds the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="search">The search.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <returns></returns>
        public static int Find(this Stream stream, string search, int bufferSize)
        {
            return Find(stream, search, bufferSize, true);
        }

        /// <summary>
        ///     Finds the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="search">The search.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="startFromBegin">
        ///     if set to <c>true</c> reads data from start of stream. If set to <c>false</c> read data
        ///     from actual position.
        /// </param>
        /// <returns>Position of searchstring.</returns>
        public static int Find(this Stream stream, string search, int bufferSize, bool startFromBegin)
        {
            if (bufferSize < (search.Length + 1))
                throw new ArgumentOutOfRangeException("bufferSize",
                    "The defined bufferSize mast be greater then length of searchstring");

            if (stream.CanSeek && startFromBegin)
                stream.Position = 0;

            var ret = -1;
            var pos = 0;
            var chars = new char[bufferSize];
            var reader = new StreamReader(stream);
            var searchBuffer = string.Empty;

            // read first block
            int numBytes;
            while ((numBytes = reader.Read(chars, 0, bufferSize)) > 0)
            {
                searchBuffer += new string(chars, 0, numBytes);

                var posActual = searchBuffer.IndexOf(search);

                if (posActual > -1)
                {
                    ret = pos + posActual;
                    break;
                }

                searchBuffer.Remove(0, searchBuffer.Length - search.Length);

                pos += posActual;
            }

            return ret;
        }

        /// <summary>
        ///     Replaces the text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        public static T ReplaceText<T>(this Stream stream, char oldValue, char newValue)
            where T : Stream, new()
        {
            return stream.ReplaceText<T>(oldValue, newValue, false, 4096);
        }

        /// <summary>
        ///     Replaces the text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="isGZipStream">if set to <c>true</c> [is G zip stream].</param>
        /// <returns></returns>
        public static T ReplaceText<T>(this Stream stream, char oldValue, char newValue, bool isGZipStream)
            where T : Stream, new()
        {
            return stream.ReplaceText<T>(oldValue, newValue, isGZipStream, 4096);
        }

        /// <summary>
        ///     Replaces the text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="isGZipStream">if set to <c>true</c> [is G zip stream].</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <returns></returns>
        public static T ReplaceText<T>(this Stream stream, char oldValue, char newValue, bool isGZipStream,
            int bufferSize)
            where T : Stream, new()
        {
            return stream.ReplaceText<T>(oldValue.ToString(), newValue.ToString(), isGZipStream, bufferSize);
        }

        /// <summary>
        ///     Replaces the text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        public static T ReplaceText<T>(this Stream stream, string oldValue, string newValue)
            where T : Stream, new()
        {
            return stream.ReplaceText<T>(oldValue, newValue, false, 4096);
        }

        /// <summary>
        ///     Replaces the text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="isGZipStream">if set to <c>true</c> [is G zip stream].</param>
        /// <returns></returns>
        public static T ReplaceText<T>(this Stream stream, string oldValue, string newValue, bool isGZipStream)
            where T : Stream, new()
        {
            return stream.ReplaceText<T>(oldValue, newValue, isGZipStream, 4096);
        }

        /// <summary>
        ///     Replaces the text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="isGZipStream">if set to <c>true</c> [is G zip stream].</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <returns></returns>
        public static T ReplaceText<T>(this Stream stream, string oldValue, string newValue, bool isGZipStream,
            int bufferSize)
            where T : Stream, new()
        {
            var values = new Dictionary<string, string> {{oldValue, newValue}};

            return stream.ReplaceTextInternal<T>(values, bufferSize, false, isGZipStream);
        }

        /// <summary>
        ///     Replaces the text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static T ReplaceText<T>(this Stream stream, Dictionary<string, string> values)
            where T : Stream, new()
        {
            return stream.ReplaceTextInternal<T>(values, 4096, false, false);
        }

        /// <summary>
        ///     Replaces the text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="values">The values.</param>
        /// <param name="isGZipStream">if set to <c>true</c> [is G zip stream].</param>
        /// <returns></returns>
        public static T ReplaceText<T>(this Stream stream, Dictionary<string, string> values, bool isGZipStream)
            where T : Stream, new()
        {
            return stream.ReplaceTextInternal<T>(values, 4096, false, isGZipStream);
        }

        /// <summary>
        ///     Replaces the text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="values">The values.</param>
        /// <param name="isGZipStream">if set to <c>true</c> [is G zip stream].</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <returns></returns>
        public static T ReplaceText<T>(this Stream stream, Dictionary<string, string> values, bool isGZipStream,
            int bufferSize)
            where T : Stream, new()
        {
            return stream.ReplaceTextInternal<T>(values, bufferSize, false, isGZipStream);
        }

        /// <summary>
        ///     Replaces the text by regex.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        public static T ReplaceTextByRegex<T>(this Stream stream, string expression, string newValue)
            where T : Stream, new()
        {
            return stream.ReplaceTextByRegex<T>(expression, newValue, false, 4096);
        }

        /// <summary>
        ///     Replaces the text by regex.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="isGZipStream">if set to <c>true</c> [is G zip stream].</param>
        /// <returns></returns>
        public static T ReplaceTextByRegex<T>(this Stream stream, string expression, string newValue, bool isGZipStream)
            where T : Stream, new()
        {
            return stream.ReplaceTextByRegex<T>(expression, newValue, isGZipStream, 4096);
        }

        /// <summary>
        ///     Replaces the text by regex.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="isGZipStream">if set to <c>true</c> [is G zip stream].</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <returns></returns>
        public static T ReplaceTextByRegex<T>(this Stream stream, string expression, string newValue, bool isGZipStream,
            int bufferSize)
            where T : Stream, new()
        {
            var values = new Dictionary<string, string> {{expression, newValue}};

            return stream.ReplaceTextInternal<T>(values, bufferSize, true, isGZipStream);
        }

        /// <summary>
        ///     Replaces the text by regex.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static T ReplaceTextByRegex<T>(this Stream stream, Dictionary<string, string> values)
            where T : Stream, new()
        {
            return stream.ReplaceTextInternal<T>(values, 4096, true, false);
        }

        /// <summary>
        ///     Replaces the text by regex.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="values">The values.</param>
        /// <param name="isGZipStream">if set to <c>true</c> [is G zip stream].</param>
        /// <returns></returns>
        public static T ReplaceTextByRegex<T>(this Stream stream, Dictionary<string, string> values, bool isGZipStream)
            where T : Stream, new()
        {
            return stream.ReplaceTextInternal<T>(values, 4096, true, isGZipStream);
        }

        /// <summary>
        ///     Replaces the text by regex.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="values">The values.</param>
        /// <param name="isGZipStream">if set to <c>true</c> [is G zip stream].</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <returns></returns>
        public static T ReplaceTextByRegex<T>(this Stream stream, Dictionary<string, string> values, bool isGZipStream,
            int bufferSize)
            where T : Stream, new()
        {
            return stream.ReplaceTextInternal<T>(values, bufferSize, true, isGZipStream);
        }

        /// <summary>
        ///     Replaces the text internal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="values">The values.</param>
        /// <param name="isGZipStream">if set to <c>true</c> [is G zip stream].</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="isRegex">if set to <c>true</c> [is regex].</param>
        /// <returns></returns>
        private static T ReplaceTextInternal<T>(this Stream stream, Dictionary<string, string> values, int bufferSize,
            bool isRegex, bool isGZipStream)
            where T : Stream, new()
        {
            Stream ret = null;

            if (stream.CanRead)
            {
                var buffer = new char[bufferSize];
                var oldPosition = stream.Position;

                if (isGZipStream)
                    ret = new GZipStream((Stream) Activator.CreateInstance(typeof (T)), CompressionMode.Compress, true);
                else
                    ret = (Stream) Activator.CreateInstance(typeof (T));

                using (var gzs = isGZipStream ? new GZipStream(stream, CompressionMode.Decompress, true) : null)
                {
                    var reader = new StreamReader(isGZipStream ? gzs : stream);

                    if (stream.CanSeek)
                        stream.Position = 0;

                    var sb = new StringBuilder();

                    while (true)
                    {
                        if (sb.Length < bufferSize)
                        {
                            var readLength = reader.Read(buffer, 0, bufferSize);

                            if (readLength == 0)
                            {
                                ret.Write(sb.ToString());
                                ret.Flush();
                                break;
                            }

                            sb.Append(buffer.Take(readLength).ToArray());
                        }

                        if (isRegex)
                        {
                            var s = sb.ToString();

                            foreach (var kv in values)
                                s = Regex.Replace(s, kv.Key, kv.Value);

                            sb = new StringBuilder(s);
                        }
                        else
                        {
                            foreach (var kv in values)
                                sb.Replace(kv.Key, kv.Value);
                        }

                        var cutCount = sb.Length - bufferSize + 1;

                        if (cutCount < 0)
                            cutCount = 0;

                        ret.Write(sb.ToString(0, cutCount));
                        sb.Remove(0, cutCount);
                    }
                }

                if (ret.CanSeek)
                    ret.Position = oldPosition;
            }

            if (isGZipStream)
            {
                var retBase = ((GZipStream) ret);

                if (retBase != null)
                {
                    var retStream = retBase.BaseStream;

                    ret.Dispose();
                    ret = retStream;
                }
            }

            return (T) ret;
        }

        /// <summary>
        ///     Replaces data in Stream matches the regular expression regexReplacement.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="regexReplacement">Regular expression which will be used for replacements.</param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static Stream ReplaceRegex(this Stream stream, string regexReplacement, string newValue)
        {
            Stream ret = null;

            if (stream.CanRead)
            {
                ret = (Stream) Activator.CreateInstance(stream.GetType());

                if (stream.CanSeek)
                    stream.Position = 0;

                var writer = new StreamWriter(ret);

                writer.Write(stream.ReadToEndReplaceRegex(regexReplacement, newValue));
                writer.Flush();
            }

            return ret;
        }

        /// <summary>
        ///     Reads to end replace regex.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="regexReplacement">The regex replacement.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        public static string ReadToEndReplaceRegex(this Stream stream, string regexReplacement, string newValue)
        {
            var ret = string.Empty;

            if (stream.CanRead)
                ret = Regex.Replace(stream.ReadToEnd(), regexReplacement, newValue);

            return ret;
        }

        /// <summary>
        ///     Reads to end.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static string ReadToEnd(this Stream stream)
        {
            return stream.ReadToEnd(false);
        }

        /// <summary>
        ///     Reads to end.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="isGZipStream">if set to <c>true</c> [is G zip stream].</param>
        /// <returns></returns>
        public static string ReadToEnd(this Stream stream, bool isGZipStream)
        {
            var ret = string.Empty;

            if (stream.CanRead)
            {
                var oldPosition = stream.CanSeek ? stream.Position : 0;

                var reader =
                    new StreamReader(isGZipStream ? new GZipStream(stream, CompressionMode.Decompress, true) : stream);

                ret = reader.ReadToEnd();

                if (stream.CanSeek)
                    stream.Position = oldPosition;
            }

            return ret;
        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ReadLine(this Stream stream)
        {
            var ret = string.Empty;

            if (stream.CanRead)
            {
                var oldPosition = 0L;

                if (stream.CanSeek)
                {
                    oldPosition = stream.Position;
                    stream.Position = 0;
                }

                var reader = new StreamReader(stream);

                ret = reader.ReadLine();

                if (stream.CanSeek)
                    stream.Position = oldPosition;
            }

            return ret;
        }

        /// <summary>
        ///     Writes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="value">The value.</param>
        public static void Write(this Stream stream, string value)
        {
            if (!stream.CanWrite) return;

            var writer = new StreamWriter(stream);

            writer.Write(value);
            writer.Flush();
        }

        /// <summary>
        ///     Writes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="value">The value.</param>
        /// <param name="encoding">The encoding.</param>
        public static void Write(this Stream stream, string value, Encoding encoding)
        {
            if (!stream.CanWrite) return;

            var writer = new StreamWriter(stream, encoding);

            writer.Write(value);
            writer.Flush();
        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        public static void WriteToEnd(this Stream stream, string value)
        {
            if (stream.CanSeek)
                stream.Seek(0, SeekOrigin.End);

            stream.Write(value);
        }

        //#region Compress / Decompress GZip
        //public static Stream CompressGZip(this Stream stream)
        //{
        //    return new GZipStream(stream, CompressionMode.Compress);
        //}
        //public static Stream DecompressGZip(this Stream stream)
        //{
        //    return new GZipStream(stream, CompressionMode.Decompress);
        //}
        //#endregion

        //#region Compress / Decompress Deflate
        //public static Stream CompressDeflate(this Stream stream)
        //{
        //    return new DeflateStream(stream, CompressionMode.Compress);
        //}
        //public static Stream DecompressDeflate(this Stream stream)
        //{
        //    return new DeflateStream(stream, CompressionMode.Decompress);
        //}
        //#endregion
    }
}