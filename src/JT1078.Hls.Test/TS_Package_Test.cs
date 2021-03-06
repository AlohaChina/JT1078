﻿using JT1078.Hls.Descriptors;
using JT1078.Hls.Enums;
using JT1078.Hls.MessagePack;
using JT1078.Protocol;
using JT1078.Protocol.Extensions;
using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace JT1078.Hls.Test
{
    /// <summary>
    /// 使用doc/video/demo0.ts
    /// </summary>
    public class TS_Package_Test
    {
        [Fact]
        public void ToBufferTest1()
        {
            //47 41 00 30 07 50 00 00 7B 0C 7E 00 00 00 01 E0 00 00 80 C0 0A 31 00 09 08 97 11 00 07 D8 61 00 00 00 01 09 F0 00 00 00 01 67 64 00 1F AC D9 40 88 1E 68 40 00 00 03 01 80 00 00 57 83 C6 0C 65 80 00 00 00 01 68 EB E3 CB 22 C0 00 00 01 06 05 FF FF AB DC 45 E9 BD E6 D9 48 B7 96 2C D8 20 D9 23 EE EF 78 32 36 34 20 2D 20 63 6F 72 65 20 31 35 38 20 72 32 39 38 34 20 33 37 35 39 66 63 62 20 2D 20 48 2E 32 36 34 2F 4D 50 45 47 2D 34 20 41 56 43 20 63 6F 64 65 63 20 2D 20 43 6F 70 79 6C 65 66 74 20 32 30 30 33 2D 32 30 31 39 20 2D 20 68 74 74 70 3A 2F 2F 77 77 77 2E

            //47 41 00 30 07 50 00 00 7B 0C 7E 00 00 00 01 E0 00 00 80 C0 0A 31 00 09 08 97 11 00 07 D8 61 00 00 00 01 09 F0 00 00 00 01 67 64 00 1F AC D9 40 88 1E 68 40 00 00 03 01 80 00 00 57 83 C6 0C 65 80 00 00 00 01 68 EB E3 CB 22 C0 00 00 01 06 05 FF FF AB DC 45 E9 BD E6 D9 48 B7 96 2C D8 20 D9 23 EE EF 78 32 36 34 20 2D 20 63 6F 72 65 20 31 35 38 20 72 32 39 38 34 20 33 37 35 39 66 63 62 20 2D 20 48 2E 32 36 34 2F 4D 50 45 47 2D 34 20 41 56 43 20 63 6F 64 65 63 20 2D 20 43 6F 70 79 6C 65 66 74 20 32 30 30 33 2D 32 30 31 39 20 2D 20 68 74 74 70 3A 2F 2F 77 77 77 2E
            //47 41 00 30 07 50 00 00 7B 0C 7E 00 00 00 01 E0 00 00 80 C0 0A 00 00 02 04 4B 00 00 01 EC 30 00 00 00 01 09 FF 000000016764001FACD940881E6840000003018000005783C60C65800000000168EBE3CB22C00000010605FFFFABDC45E9BDE6D948B7962CD820D923EEEF78323634202D20636F7265203135382072323938342033373539666362202D20482E3236342F4D5045472D342041564320636F646563202D20436F70796C65667420323030332D32303139202D20687474703A2F2F7777772E

            //47 
            //41 00 
            //30 
            //07 
            //50 
            //00 00 7B 0C 7E 
            //00 
            //00 00 01 
            //E0 
            //00 00 
            //80 
            //C0 
            //0A 
            //31 00 09 08 97 
            //11 00 07 D8 61 
            //00 00 00 01 
            //09 
            //F0

            //00 00 00 01 67 64 00 1F AC D9 40 88 1E 68 40 00 00 03 01 80 00 00 57 83 C6 0C 65 80 00 00 00 01 68 EB E3 CB 22 C0 00 00 01 06 05 FF FF AB DC 45 E9 BD E6 D9 48 B7 96 2C D8 20 D9 23 EE EF 78 32 36 34 20 2D 20 63 6F 72 65 20 31 35 38 20 72 32 39 38 34 20 33 37 35 39 66 63 62 20 2D 20 48 2E 32 36 34 2F 4D 50 45 47 2D 34 20 41 56 43 20 63 6F 64 65 63 20 2D 20 43 6F 70 79 6C 65 66 74 20 32 30 30 33 2D 32 30 31 39 20 2D 20 68 74 74 70 3A 2F 2F 77 77 77 2E 
            TS_Package package = new TS_Package();
            package.Header = new TS_Header();
            package.Header.PID = 0x100;
            package.Header.AdaptationFieldControl = AdaptationFieldControl.同时带有自适应域和有效负载;
            package.Header.ContinuityCounter = 0;
            package.Header.PackageType = PackageType.Data_Start;
            package.Header.PayloadUnitStartIndicator = 1;
            package.Header.Adaptation = new TS_AdaptationInfo();
            package.Header.Adaptation.Timestamp = 18900000;
            package.Header.Adaptation.PCRIncluded =  PCRInclude.包含;
            package.Payload = new PES_Package();
            package.Payload.PTS = 132171;
            package.Payload.DTS = 126000;
            package.Payload.PTS_DTS_Flag =  PTS_DTS_Flags.all;
            package.Payload.Payload = new ES_Package();
            package.Payload.Payload.NALUs = new List<byte[]>();
            package.Payload.Payload.NALUs.Add("00 00 00 01 67 64 00 1F AC D9 40 88 1E 68 40 00 00 03 01 80 00 00 57 83 C6 0C 65 80 00 00 00 01 68 EB E3 CB 22 C0 00 00 01 06 05 FF FF AB DC 45 E9 BD E6 D9 48 B7 96 2C D8 20 D9 23 EE EF 78 32 36 34 20 2D 20 63 6F 72 65 20 31 35 38 20 72 32 39 38 34 20 33 37 35 39 66 63 62 20 2D 20 48 2E 32 36 34 2F 4D 50 45 47 2D 34 20 41 56 43 20 63 6F 64 65 63 20 2D 20 43 6F 70 79 6C 65 66 74 20 32 30 30 33 2D 32 30 31 39 20 2D 20 68 74 74 70 3A 2F 2F 77 77 77 2E".ToHexBytes());
            TSMessagePackWriter writer = new TSMessagePackWriter(new byte[188]);
            package.ToBuffer(ref writer);
            var patData = writer.FlushAndGetArray().ToHexString();
        }

        [Fact]
        public void ToBufferTest2()
        {
            FileStream fileStream = null;
            try
            {
                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_1.ts");
                File.Delete(filepath);
                var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_1.txt"));
                fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                TSEncoder tSEncoder = new TSEncoder();
                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    var bytes = data[6].ToHexBytes();
                    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                    JT1078Package fullpackage = JT1078Serializer.Merge(package);
                    if (fullpackage != null)
                    {
                        var sdt = tSEncoder.CreateSDT(fullpackage);
                        string sdtHEX = sdt.ToHexString();
                        fileStream.Write(sdt);
                        var pat = tSEncoder.CreatePAT(fullpackage);
                        string patHEX = pat.ToHexString();
                        fileStream.Write(pat);
                        var pmt = tSEncoder.CreatePMT(fullpackage);
                        fileStream.Write(pmt);
                        var pes = tSEncoder.CreatePES(fullpackage);
                        fileStream.Write(pes);
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Throws<Exception>(() => { });
            }
            finally
            {
                fileStream?.Close();
                fileStream?.Dispose();
            }
        }

        /// <summary>
        /// 可以用ffplay播放的JT1078_3.ts
        /// </summary>
        [Fact]
        public void ToBufferTest3()
        {
            FileStream fileStream = null;
            try
            {
                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_3.ts");
                File.Delete(filepath);
                var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_3.txt"));

                bool isNeedFirstHeadler = true;
                TSEncoder tSEncoder = new TSEncoder();
                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    var bytes = data[6].ToHexBytes();
                    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                    JT1078Package fullpackage = JT1078Serializer.Merge(package);
                    if (fullpackage != null)
                    {
                        if (isNeedFirstHeadler)
                        {
                            var sdt = tSEncoder.CreateSDT(fullpackage);
                            string sdtHEX = sdt.ToHexString();
                            fileStream.Write(sdt);
                            var pat = tSEncoder.CreatePAT(fullpackage);
                            string patHEX = pat.ToHexString();
                            fileStream.Write(pat);
                            var pmt = tSEncoder.CreatePMT(fullpackage);
                            fileStream.Write(pmt);
                            var pes = tSEncoder.CreatePES(fullpackage, 18888);
                            fileStream.Write(pes);
                            isNeedFirstHeadler = false;
                        }
                        else
                        {
                            fileStream.Write(tSEncoder.CreatePES(fullpackage, 18888));
                        }              
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Throws<Exception>(() => { });
            }
            finally
            {
                fileStream?.Close();
                fileStream?.Dispose();
            }
        }
        /// <summary>
        /// 生成m3u8索引文件
        /// </summary>
        [Fact]
        public void Test4()
        {
            try
            {
                int file_count = 10;
                int file_max_second = 10;               
                int first_serialno = 0;
                double file_real_second = 10;

                var lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_3.txt")); 
                bool isNeedFirstHeadler = true;
                TSEncoder tSEncoder = new TSEncoder();
                
                ulong init_seconds = 0;
                int duration = 0;
                int temp_seconds = 0;

                var m3u8Filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", "JT1078_HLS.m3u8");
                AppendM3U8Start(m3u8Filepath, file_max_second, first_serialno);
                byte[] fileData = new byte[1888888888];
                int fileIndex = 0;
                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    var bytes = data[6].ToHexBytes();
                    JT1078Package package = JT1078Serializer.Deserialize(bytes);
                    JT1078Package fullpackage = JT1078Serializer.Merge(package);
                    if (fullpackage != null)
                    {
                        if (temp_seconds / 1000>= file_max_second) {                
                            file_real_second = temp_seconds / 1000.0;//秒
                           //生成一个文件
                            var file_name = $"JT1078_{first_serialno}.ts";
                            var ts_filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", file_name);
                            CreateTsFile(ts_filepath, fileData);
                            //更新m3u8文件
                            AppendTSFile(m3u8Filepath, file_real_second, file_name);
                            //删除最早一个文件
                            var del_file_name = $"JT1078_{first_serialno- file_count}.ts";
                            var del_ts_filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H264", del_file_name);
                            DeleteTsFile(del_ts_filepath);
                            fileData = new byte[1888888888];
                            temp_seconds = 0;
                            first_serialno = first_serialno + 1;
                        }

                        if (init_seconds == 0)
                        {
                            init_seconds = fullpackage.Timestamp;
                        }
                        else {
                            duration =(int)( fullpackage.Timestamp - init_seconds);
                            init_seconds = fullpackage.Timestamp;
                            temp_seconds = Convert.ToInt32(temp_seconds) + duration;
                        }

                        if (isNeedFirstHeadler)
                        {
                            var sdt = tSEncoder.CreateSDT(fullpackage);
                            string sdtHEX = sdt.ToHexString();
                            sdt.CopyTo(fileData, fileIndex);
                            fileIndex = sdt.Length;
                            var pat = tSEncoder.CreatePAT(fullpackage);
                            string patHEX = pat.ToHexString();
                            pat.CopyTo(fileData, fileIndex);
                            fileIndex = fileIndex + pat.Length;
                            var pmt = tSEncoder.CreatePMT(fullpackage);
                            pmt.CopyTo(fileData, fileIndex);
                            fileIndex = fileIndex + pmt.Length;
                            var pes = tSEncoder.CreatePES(fullpackage, 18888);
                            pes.CopyTo(fileData, fileIndex);
                            fileIndex = fileIndex + pes.Length;
                            isNeedFirstHeadler = false;
                        }
                        else
                        {
                            var pes = tSEncoder.CreatePES(fullpackage, 18888);
                            pes.CopyTo(fileData, fileIndex);
                            fileIndex = fileIndex + pes.Length;
                        }
                    }
                }
                AppendM3U8End(m3u8Filepath);
            }
            catch (Exception ex)
            {
                Assert.Throws<Exception>(() => { });
            }
        }

        private void DeleteTsFile(string tsFilepath) {
            if (File.Exists(tsFilepath)) File.Delete(tsFilepath);
        }
        private void CreateTsFile(string tsFilepath, byte[] data) {
            using (var fileStream = new FileStream(tsFilepath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fileStream.Write(data);
            }
        }

        private void AppendTSFile(string filepath,double tsRealSecond, string tsName) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"#EXTINF:{tsRealSecond},");//extra info，分片TS的信息，如时长，带宽等
            sb.AppendLine($"{tsName}");//文件名
            using (StreamWriter sw = new StreamWriter(filepath))
            {
                sw.WriteLine(sb);
            }
        }

        private void AppendM3U8Start(string filepath,int fileMaxSecond,int firstTSSerialno) {
            if(File.Exists(filepath)) File.Delete(filepath);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("#EXTM3U");//开始
            sb.AppendLine("#EXT-X-VERSION:3");//版本号
            sb.AppendLine("#EXT-X-ALLOW-CACHE:NO");//是否允许cache    

            sb.AppendLine($"#EXT-X-TARGETDURATION:{fileMaxSecond}");//每个分片TS的最大的时长  
            sb.AppendLine($"#EXT-X-MEDIA-SEQUENCE:{firstTSSerialno}");//第一个TS分片的序列号  
            using (StreamWriter sw = new StreamWriter(filepath))
            {
                sw.WriteLine(sb);
            }
        }
        /// <summary>
        /// 添加结束标识
        /// </summary>
        /// <param name="filepath"></param>
        private void AppendM3U8End(string filepath) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("#EXT-X-ENDLIST"); //m3u8文件结束符 表示视频已经结束 有这个标志同时也说明当前流是一个非直播流
                                             //#EXT-X-PLAYLIST-TYPE:VOD/Live   //VOD表示当前视频流不是一个直播流，而是点播流(也就是视频的全部ts文件已经生成)
            using (StreamWriter sw = new StreamWriter(filepath))
            {
                sw.WriteLine(sb);
            }
        }

        /// <summary>
        ///         
        /// PTS[32..30]                                3              bslbf
        /// marker_bit                                 1              bslbf
        /// PTS[29..15]                                15             bslbf
        /// marker_bit                                 1              bslbf
        /// PTS[14..0]                                 15             bslbf
        /// marker_bit                                 1              bslbf
        /// '0001'                                     4              bslbf
        /// DTS[32..30]                                3              bslbf
        /// marker_bit                                 1              bslbf
        /// DTS[29..15]                                15             bslbf
        /// marker_bit                                 1              bslbf
        /// DTS[14..0]                                 15             bslbf
        /// marker_bit                                 1              bslbf
        /// 
        /// </summary>
        [Fact]
        public void PTSTest()
        {
            //pts
            //31 00 09 08 97 
            //'0011'
            long ptsvalue = 132171;
            var str = Convert.ToString(ptsvalue, 2).PadLeft(40, '0');
            str = str.Insert(str.Length, "1");
            str = str.Insert(str.Length - 16, "1");
            str = str.Insert(str.Length - 32, "1");
            str = str.Insert(str.Length - 36, "0011");
            str = str.Substring(str.Length - 40, 40);
            var pts = Convert.ToInt64(str, 2);
            //210453989527
        }

        [Fact]
        public void DTSTest1()
        {
            //dts
            //11 00 07 D8 61 
            long value = 126000;
            var str = Convert.ToString(value, 2).PadLeft(40, '0');
            str = str.Insert(str.Length, "1");
            str = str.Insert(str.Length - 16, "1");
            str = str.Insert(str.Length - 32, "1");
            str = str.Insert(str.Length - 36, "0001");
            str = str.Substring(str.Length - 40, 40);
            var dts = Convert.ToInt64(str, 2);
        }
    }
}
