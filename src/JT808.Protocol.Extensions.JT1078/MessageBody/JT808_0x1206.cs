﻿using JT808.Protocol.Formatters;
using JT808.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Extensions.JT1078.MessageBody
{
    /// <summary>
    /// 文件上传完成通知
    /// </summary>
    public class JT808_0x1206 : JT808Bodies, IJT808MessagePackFormatter<JT808_0x1206>
    {
        public override string Description => "文件上传完成通知";
        public override ushort MsgId => 0x1206;
        /// <summary>
        /// 流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public byte Result{ get; set; }
        public JT808_0x1206 Deserialize(ref JT808MessagePackReader reader, IJT808Config config)
        {
            JT808_0x1206 jT808_0x1206 = new JT808_0x1206();
            jT808_0x1206.MsgNum = reader.ReadUInt16();
            jT808_0x1206.Result = reader.ReadByte();
            return jT808_0x1206;
        }

        public void Serialize(ref JT808MessagePackWriter writer, JT808_0x1206 value, IJT808Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            writer.WriteByte(value.Result);
        }
    }
}
