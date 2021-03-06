// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: shared.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace OpenTibiaUnity.Protobuf.Shared
{

    /// <summary>Holder for reflection information generated from shared.proto</summary>
    public static partial class SharedReflection
    {

        #region Descriptor
        /// <summary>File descriptor for shared.proto</summary>
        public static pbr::FileDescriptor Descriptor {
            get { return descriptor; }
        }
        private static pbr::FileDescriptor descriptor;

        static SharedReflection() {
            byte[] descriptorData = global::System.Convert.FromBase64String(
                string.Concat(
                  "CgxzaGFyZWQucHJvdG8SHk9wZW5UaWJpYVVuaXR5LlByb3RvYnVmLlNoYXJl",
                  "ZCofCghIb29rVHlwZRIJCgVTb3V0aBAAEggKBEVhc3QQASruAgoMSXRlbUNh",
                  "dGVnb3J5EhAKDENhdGVnb3J5Tm9uZRAAEgoKBkFybW9ycxABEgsKB0FtdWxl",
                  "dHMQAhIJCgVCb290cxADEg4KCkNvbnRhaW5lcnMQBBIOCgpEZWNvcmF0aW9u",
                  "EAUSCAoERm9vZBAGEg8KC0hlbG1ldHNIYXRzEAcSCAoETGVncxAIEgoKBk90",
                  "aGVycxAJEgsKB1BvdGlvbnMQChIJCgVSaW5ncxALEgkKBVJ1bmVzEAwSCwoH",
                  "U2hpZWxkcxANEgkKBVRvb2xzEA4SDQoJVmFsdWFibGVzEA8SDgoKQW1tdW5p",
                  "dGlvbhAQEggKBEF4ZXMQERIJCgVDbHVicxASEhMKD0Rpc3RhbmNlV2VhcG9u",
                  "cxATEgoKBlN3b3JkcxAUEg0KCVdhbmRzUm9kcxAVEhIKDlByZW1pdW1TY3Jv",
                  "bGxzEBYSDgoKVGliaWFDb2lucxAXEhQKEENyZWF0dXJlUHJvZHVjdHMQGCpw",
                  "ChBQbGF5ZXJQcm9mZXNzaW9uEhIKDlByb2Zlc3Npb25Ob25lEAASCgoGS25p",
                  "Z2h0EAESCwoHUGFsYWRpbhACEgwKCFNvcmNlcmVyEAMSCQoFRHJ1aWQQBBIM",
                  "CghQcm9tb3RlZBAKEggKA0FueRD/ASpSCgxQbGF5ZXJBY3Rpb24SDgoKQWN0",
                  "aW9uTm9uZRAAEggKBExvb2sQARIHCgNVc2UQAhIICgRPcGVuEAMSFQoRQXV0",
                  "b3dhbGtIaWdobGlnaHQQBCo0Cg5GcmFtZUdyb3VwVHlwZRIICgRJZGxlEAAS",
                  "CwoHV2Fsa2luZxABEgsKB0luaXRpYWwQAio9ChFBbmltYXRpb25Mb29wVHlw",
                  "ZRIMCghJbmZpbml0ZRAAEgsKB0NvdW50ZWQQARINCghQaW5nUG9uZxD/AWIG",
                  "cHJvdG8z"));
            descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
                new pbr::FileDescriptor[] { },
                new pbr::GeneratedClrTypeInfo(new[] { typeof(global::OpenTibiaUnity.Protobuf.Shared.HookType), typeof(global::OpenTibiaUnity.Protobuf.Shared.ItemCategory), typeof(global::OpenTibiaUnity.Protobuf.Shared.PlayerProfession), typeof(global::OpenTibiaUnity.Protobuf.Shared.PlayerAction), typeof(global::OpenTibiaUnity.Protobuf.Shared.FrameGroupType), typeof(global::OpenTibiaUnity.Protobuf.Shared.AnimationLoopType), }, null));
        }
        #endregion

    }
    #region Enums
    public enum HookType
    {
        [pbr::OriginalName("South")] South = 0,
        [pbr::OriginalName("East")] East = 1,
    }

    public enum ItemCategory
    {
        [pbr::OriginalName("CategoryNone")] CategoryNone = 0,
        [pbr::OriginalName("Armors")] Armors = 1,
        [pbr::OriginalName("Amulets")] Amulets = 2,
        [pbr::OriginalName("Boots")] Boots = 3,
        [pbr::OriginalName("Containers")] Containers = 4,
        [pbr::OriginalName("Decoration")] Decoration = 5,
        [pbr::OriginalName("Food")] Food = 6,
        [pbr::OriginalName("HelmetsHats")] HelmetsHats = 7,
        [pbr::OriginalName("Legs")] Legs = 8,
        [pbr::OriginalName("Others")] Others = 9,
        [pbr::OriginalName("Potions")] Potions = 10,
        [pbr::OriginalName("Rings")] Rings = 11,
        [pbr::OriginalName("Runes")] Runes = 12,
        [pbr::OriginalName("Shields")] Shields = 13,
        [pbr::OriginalName("Tools")] Tools = 14,
        [pbr::OriginalName("Valuables")] Valuables = 15,
        [pbr::OriginalName("Ammunition")] Ammunition = 16,
        [pbr::OriginalName("Axes")] Axes = 17,
        [pbr::OriginalName("Clubs")] Clubs = 18,
        [pbr::OriginalName("DistanceWeapons")] DistanceWeapons = 19,
        [pbr::OriginalName("Swords")] Swords = 20,
        [pbr::OriginalName("WandsRods")] WandsRods = 21,
        [pbr::OriginalName("PremiumScrolls")] PremiumScrolls = 22,
        [pbr::OriginalName("TibiaCoins")] TibiaCoins = 23,
        [pbr::OriginalName("CreatureProducts")] CreatureProducts = 24,
    }

    public enum PlayerProfession
    {
        [pbr::OriginalName("ProfessionNone")] ProfessionNone = 0,
        [pbr::OriginalName("Knight")] Knight = 1,
        [pbr::OriginalName("Paladin")] Paladin = 2,
        [pbr::OriginalName("Sorcerer")] Sorcerer = 3,
        [pbr::OriginalName("Druid")] Druid = 4,
        [pbr::OriginalName("Promoted")] Promoted = 10,
        [pbr::OriginalName("Any")] Any = 255,
    }

    public enum PlayerAction
    {
        [pbr::OriginalName("ActionNone")] ActionNone = 0,
        [pbr::OriginalName("Look")] Look = 1,
        [pbr::OriginalName("Use")] Use = 2,
        [pbr::OriginalName("Open")] Open = 3,
        [pbr::OriginalName("AutowalkHighlight")] AutowalkHighlight = 4,
    }

    public enum FrameGroupType
    {
        [pbr::OriginalName("Idle")] Idle = 0,
        [pbr::OriginalName("Walking")] Walking = 1,
        /// <summary>
        /// introduced in tibia 11 
        /// </summary>
        [pbr::OriginalName("Initial")] Initial = 2,
    }

    public enum AnimationLoopType
    {
        [pbr::OriginalName("Infinite")] Infinite = 0,
        [pbr::OriginalName("Counted")] Counted = 1,
        [pbr::OriginalName("PingPong")] PingPong = 255,
    }

    #endregion

}

#endregion Designer generated code