using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLib
{
    ///<summary> TypeUtils specifies attributes of data types that, for some reason, 
    /// don't seem to be implemented in .NET.</summary>
    public static class TypeUtils
    {
        public static bool IsIntegral(Type typ)
        {
            return IsSigned(typ) || IsUnsigned(typ);
        }

        public static bool IsIntegral(object ob)
        {
            return IsSigned(ob) || IsUnsigned(ob);
        }

        /// <summary>
        /// Returns true if a value of this type can be assigned to a variable of type Int32
        /// </summary>
        /// <param name="typ">Type of value</param>
        /// <returns>True if value can be assigned, false if not</returns>
        public static bool Is32BitIntegral(Type typ)
        {
            return (typ == typeof(SByte) || typ == typeof(Int16) || typ == typeof(Int32) ||
                typ == typeof(Byte) || typ == typeof(UInt16));
        }

        /// <summary>
        /// Returns true if the specified data value can be assigned to a variable of type Int32
        /// </summary>
        /// <param name="ob">Data value</param>
        /// <returns>True if value can be assigned, false if not</returns>
        public static bool Is32BitIntegral(object ob)
        {
            return (ob is SByte || ob is Int16 || ob is Int32 || ob is Byte || ob is UInt16);
        }

        public static bool IsSigned(Type typ)
        {
            return (typ == typeof(SByte) || typ == typeof(Int16) ||
                typ == typeof(Int32) || typ == typeof(Int64));
        }

        public static bool IsSigned(object ob)
        {
            return (ob is SByte || ob is Int16 || ob is Int32 || ob is Int64);
        }

        public static bool IsUnsigned(Type typ)
        {
            return (typ == typeof(Byte) || typ == typeof(UInt16) ||
                typ == typeof(UInt32) || typ == typeof(UInt64));
        }

        public static bool IsUnsigned(object ob)
        {
            return (ob is Byte || ob is UInt16 || ob is UInt32 || ob is UInt64);
        }

        /// <summary>
        /// Converts an integral value in an Object variable to Int32, if it can
        /// </summary>
        /// <param name="inValue">Value of type Object to convert</param>
        /// <param name="converted">Output parameter: true if it successfully converted, false otherwise</param>
        /// <returns>Value of type Int32 containing the converted value, if it successfully converted; otherwise 0.</returns>
        /// This handles a poorly designed aspect of .NET: if you compare two untyped variables with IComparable.CompareTo(), and they are different 
        /// types, it will throw an exception - even if they are both integral types. For example, say you've got two variables, val1 and val2, 
        /// both of type IComparable. If you set them to values of the same type, and compare them, as in this code, it works fine:
        /// <example>
        ///             short s1 = 1;
        ///             short s2 = 2;
        ///             IComparable val1 = s1;
        ///             IComparable val2 = s2;
        ///             int result = val1.CompareTo(val2);
        /// </example>
        /// But if you change that first line to "long s1 = 1;" and run it, it will throw an exception - even though it has no problem comparing
        /// two integral values.
        /// This was causing problems in our database code in the DBKey (now DBValue) class, where a table key may be type Int32 in the database (and in the 
        /// generated DataSet code), but a foreign key linked to it is type Int16. So on 8/22/13 I created ConvertToInt32().
        /// Note that this does not handle types that go beyond the integral range of the Int32; namely UInt32, Int64, and UInt64. I added 
        /// ConvertToInt64() (on 8/11/14) for that.
        public static Int32 ConvertToInt32(object inValue, out bool converted)
        {
            converted = true; // initialize
            if (inValue is Int32)
                return (Int32)inValue;
            else if (inValue is Int16)
                return Convert.ToInt32((Int16)inValue);
            else if (inValue is UInt16)
                return Convert.ToInt32((UInt16)inValue);
            else if (inValue is Byte)
                return Convert.ToInt32((Byte)inValue);
            else if (inValue is SByte)
                return Convert.ToInt32((SByte)inValue);
            else
            {
                converted = false;
                return 0;
            }
        }

        /// <summary>
        /// Converts an integral value in an Object variable to Int64, if it can
        /// </summary>
        /// <param name="inValue">Value of type Object to convert</param>
        /// <param name="converted">Output parameter: true if it successfully converted, false otherwise</param>
        /// <returns>Value of type Int64 containing the converted value, if it successfully converted; otherwise 0.</returns>
        /// This handles a poorly designed aspect of .NET: if you compare two untyped variables with IComparable.CompareTo(), and they are different 
        /// types, it will throw an exception - even if they are both integral types. For example, say you've got two variables, val1 and val2, 
        /// both of type IComparable. If you set them to values of the same type, and compare them, as in this code, it works fine:
        /// <example>
        ///             short s1 = 1;
        ///             short s2 = 2;
        ///             IComparable val1 = s1;
        ///             IComparable val2 = s2;
        ///             int result = val1.CompareTo(val2);
        /// </example>
        /// But if you change that first line to "long s1 = 1;" and run it, it will throw an exception - even though it has no problem comparing
        /// two integral values.
        /// This was causing problems in our database code in the DBKey (now DBValue) class, where a table key may be type Int32 in the database (and in the 
        /// generated DataSet code), but a foreign key linked to it is type Int16. So on 8/22/13 I created ConvertToInt32().
        /// Then on 8/11/14 I added ConvertToInt64(), as by then we had switched over to the 64-bit Windows 7 operating system, and we were encountering 
        /// a situation where the DBKey (now DBValue) constructor (public DBKey(object, EColumnType)) was passing an Int64 to ConvertToInt32. Since SQL Server's largest 
        /// integral type is Bigint - signed 64-bit - and it doesn't have unsigned int data types, converting to an Int64 will work for all DB integral types.
        public static Int64 ConvertToInt64(object inValue, out bool converted)
        {
            converted = true; // initialize
            if (inValue is Int64)
                return (Int64)inValue;
            else if (inValue is Int32)
                return Convert.ToInt64((Int32)inValue);
            else if (inValue is UInt32)
                return Convert.ToInt64((UInt32)inValue);
            else if (inValue is Int16)
                return Convert.ToInt64((Int16)inValue);
            else if (inValue is UInt16)
                return Convert.ToInt64((UInt16)inValue);
            else if (inValue is Byte)
                return Convert.ToInt64((Byte)inValue);
            else if (inValue is SByte)
                return Convert.ToInt64((SByte)inValue);
            else
            {
                converted = false;
                return 0;
            }
        }

        /** <summary>  ToInt casts an Object reference to type int.</summary>
         * I need this because, if Object is a short int, I can't cast it to 
         * an int -- (int)ob throws an exception. Good one, Microsoft.
         */
        public static int ToInt(Object ob)
        {
            if (ob is int || ob is Enum)
                return (int)ob;
            else if (ob is short)
                return (short)ob;
            else if (ob is byte)
                return (byte)ob;
            else throw (new Exception(
                "Utils.ToInt attempted to cast an unsupported data type"));
        }

    }
}
