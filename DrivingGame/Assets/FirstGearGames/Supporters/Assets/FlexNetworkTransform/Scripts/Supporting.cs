using Mirror;
using System;
using UnityEngine;

namespace FirstGearGames.Mirrors.Assets.FlexNetworkTransforms
{
    [System.Serializable, System.Flags]
    public enum Axes : int
    {
        X = 1,
        Y = 2,
        Z = 4
    }

    /// <summary>
    /// Transform properties which need to be synchronized.
    /// </summary>
    [System.Flags]
    public enum SyncProperties : byte
    {
        None = 0,
        //Position included.
        Position = 1,
        //Rotation included.
        Rotation = 2,
        //Scale included.
        Scale = 4,
        //Indicates packet is sequenced, generally for UDP.
        Sequenced = 8,
        //Indicates transform did not move.
        Settled = 16
    }

    /// <summary>
    /// Using strongly typed for performance.
    /// </summary>
    public static class EnumContains
    {
        /// <summary>
        /// Returns if a SyncProperties Whole contains Part.
        /// </summary>
        /// <param name="whole"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        public static bool SyncPropertiesContains(SyncProperties whole, SyncProperties part)
        {
            return (whole & part) == part;
        }

        /// <summary>
        /// Returns if a Axess Whole contains Part.
        /// </summary>
        /// <param name="whole"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        public static bool AxesContains(Axes whole, Axes part)
        {
            return (whole & part) == part;
        }
    }


    /// <summary>
    /// Container holding latest transform values.
    /// </summary>
    [System.Serializable]
    public class TransformSyncData
    {
        public TransformSyncData() { }
        public TransformSyncData(byte syncProperties, double sequenceId, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            SyncProperties = syncProperties;
            SequenceId = sequenceId;
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public byte SyncProperties;
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
        public double SequenceId = -1d;

        public float TransitionRate;
    }

    public static class FlexNetworkTransformSerializers
    {
        /// <summary>
        /// Writes TransformSyncData into a writer.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="syncData"></param>
        public static void WriteTransformSyncData(this NetworkWriter writer, TransformSyncData syncData)
        {
            SyncProperties sp = (SyncProperties)syncData.SyncProperties;
            writer.WriteByte(syncData.SyncProperties);

            if (EnumContains.SyncPropertiesContains(sp, SyncProperties.Sequenced))
                writer.WriteDouble(syncData.SequenceId);

            if (EnumContains.SyncPropertiesContains(sp, SyncProperties.Position))
                writer.WriteVector3(syncData.Position);
            if (EnumContains.SyncPropertiesContains(sp, SyncProperties.Rotation))
            {
                sbyte[] rot = new sbyte[4]
                {
                    Convert.ToSByte(syncData.Rotation.x * 100),
                    Convert.ToSByte(syncData.Rotation.y * 100),
                    Convert.ToSByte(syncData.Rotation.z * 100),
                    Convert.ToSByte(syncData.Rotation.w * 100)
                };

                byte[] bytes = new byte[4];
                Buffer.BlockCopy(rot, 0, bytes, 0, 4);
                writer.WriteBytes(bytes, 0, 4);
                //writer.WriteQuaternion(syncData.Rotation);
            }
            if (EnumContains.SyncPropertiesContains(sp, SyncProperties.Scale))
                writer.WriteVector3(syncData.Scale);
        }

        /// <summary>
        /// Converts reader data into a new TransformSyncData.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static TransformSyncData ReadTransformSyncData(this NetworkReader reader)
        {
            SyncProperties sp = (SyncProperties)reader.ReadByte();

            TransformSyncData syncData = new TransformSyncData();
            syncData.SyncProperties = (byte)sp;

            //SequenceId.
            if (EnumContains.SyncPropertiesContains(sp, SyncProperties.Sequenced))
                syncData.SequenceId = reader.ReadDouble();
            //Position.
            if (EnumContains.SyncPropertiesContains(sp, SyncProperties.Position))
                syncData.Position = reader.ReadVector3();
            //Rotation.
            if (EnumContains.SyncPropertiesContains(sp, SyncProperties.Rotation))
            {
                byte[] bytes = reader.ReadBytes(4);
                sbyte[] rot = new sbyte[4];
                Buffer.BlockCopy(bytes, 0, rot, 0, 4);
                syncData.Rotation = new Quaternion(rot[0] / 100f, rot[1] / 100f, rot[2] / 100f, rot[3] / 100f);
            }
            //scale.
            if (EnumContains.SyncPropertiesContains(sp, SyncProperties.Scale))
                syncData.Scale = reader.ReadVector3();

            return syncData;
        }


    }


}