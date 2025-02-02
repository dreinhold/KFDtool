﻿using KFDtool.Adapter.Protocol.Adapter;
using KFDtool.P25.ManualRekey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFDtool.P25.TransferConstructs
{
    public class Interact
    {
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static string ReadAdapterProtocolVersion(string port)
        {
            string version = string.Empty;

            if (port == string.Empty)
            {
                throw new ArgumentException("port empty");
            }

            AdapterProtocol ap = null;

            try
            {
                ap = new AdapterProtocol(port);

                ap.Open();

                ap.Clear();

                byte[] ver = ap.ReadAdapterProtocolVersion();

                version = string.Format("{0}.{1}.{2}", ver[0], ver[1], ver[2]);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (ap != null)
                    {
                        ap.Close();
                    }
                }
                catch (System.IO.IOException ex)
                {
                    Logger.Warn("could not close serial port: {0}", ex.Message);
                }
            }

            return version;
        }

        public static string ReadFirmwareVersion(string port)
        {
            string version = string.Empty;

            if (port == string.Empty)
            {
                throw new ArgumentException("port empty");
            }

            AdapterProtocol ap = null;

            try
            {
                ap = new AdapterProtocol(port);

                ap.Open();

                ap.Clear();

                byte[] ver = ap.ReadFirmwareVersion();

                version = string.Format("{0}.{1}.{2}", ver[0], ver[1], ver[2]);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (ap != null)
                    {
                        ap.Close();
                    }
                }
                catch (System.IO.IOException ex)
                {
                    Logger.Warn("could not close serial port: {0}", ex.Message);
                }
            }

            return version;
        }

        public static string ReadUniqueId(string port)
        {
            string uniqueId = string.Empty;

            if (port == string.Empty)
            {
                throw new ArgumentException("port empty");
            }

            AdapterProtocol ap = null;

            try
            {
                ap = new AdapterProtocol(port);

                ap.Open();

                ap.Clear();

                byte[] id = ap.ReadUniqueId();

                if (id.Length == 0)
                {
                    uniqueId = "NONE";
                }
                else
                {
                    uniqueId = BitConverter.ToString(id).Replace("-", string.Empty);

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (ap != null)
                    {
                        ap.Close();
                    }
                }
                catch (System.IO.IOException ex)
                {
                    Logger.Warn("could not close serial port: {0}", ex.Message);
                }
            }

            return uniqueId;
        }

        public static string ReadModel(string port)
        {
            string model = string.Empty;

            if (port == string.Empty)
            {
                throw new ArgumentException("port empty");
            }

            AdapterProtocol ap = null;

            try
            {
                ap = new AdapterProtocol(port);

                ap.Open();

                ap.Clear();

                byte mod = ap.ReadModelId();

                if (mod == 0x00)
                {
                    model = "NOT SET";
                }
                else if (mod == 0x01)
                {
                    model = "KFD100";
                }
                else
                {
                    model = "UNKNOWN";
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (ap != null)
                    {
                        ap.Close();
                    }
                }
                catch (System.IO.IOException ex)
                {
                    Logger.Warn("could not close serial port: {0}", ex.Message);
                }
            }

            return model;
        }

        public static string ReadHardwareRevision(string port)
        {
            string version = string.Empty;

            if (port == string.Empty)
            {
                throw new ArgumentException("port empty");
            }

            AdapterProtocol ap = null;

            try
            {
                ap = new AdapterProtocol(port);

                ap.Open();

                ap.Clear();

                byte[] ver = ap.ReadHardwareRevision();

                if (ver[0] == 0x00 && ver[1] == 0x00)
                {
                    version = "NOT SET";
                }
                else
                {
                    version = string.Format("{0}.{1}", ver[0], ver[1]);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (ap != null)
                    {
                        ap.Close();
                    }
                }
                catch (System.IO.IOException ex)
                {
                    Logger.Warn("could not close serial port: {0}", ex.Message);
                }
            }

            return version;
        }

        public static string ReadSerialNumber(string port)
        {
            string serialNumber = string.Empty;

            if (port == string.Empty)
            {
                throw new ArgumentException("port empty");
            }

            AdapterProtocol ap = null;

            try
            {
                ap = new AdapterProtocol(port);

                ap.Open();

                ap.Clear();

                byte[] ser = ap.ReadSerialNumber();

                if (ser.Length == 0)
                {
                    serialNumber = "NONE";
                }
                else
                {
                    serialNumber = Encoding.ASCII.GetString(ser);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (ap != null)
                    {
                        ap.Close();
                    }
                }
                catch (System.IO.IOException ex)
                {
                    Logger.Warn("could not close serial port: {0}", ex.Message);
                }
            }

            return serialNumber;
        }

        public static void EnterBslMode(string port)
        {
            if (port == string.Empty)
            {
                throw new ArgumentException("port empty");
            }

            AdapterProtocol ap = null;

            try
            {
                ap = new AdapterProtocol(port);

                ap.Open();

                ap.Clear();

                ap.EnterBslMode();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (ap != null)
                    {
                        ap.Close();
                    }
                }
                catch (System.IO.IOException ex)
                {
                    Logger.Warn("could not close serial port: {0}", ex.Message);
                }
            }
        }

        public static string SelfTest(string port)
        {
            string result = string.Empty;

            if (port == string.Empty)
            {
                throw new ArgumentException("port empty");
            }

            AdapterProtocol ap = null;

            try
            {
                ap = new AdapterProtocol(port);

                ap.Open();

                ap.Clear();

                byte res = ap.SelfTest();

                if (res == 0x01)
                {
                    result = string.Format("Data shorted to ground (0x{0:X2})", res);
                }
                else if (res == 0x02)
                {
                    result = string.Format("Sense shorted to ground (0x{0:X2})", res);
                }
                else if (res == 0x03)
                {
                    result = string.Format("Data shorted to power (0x{0:X2})", res);
                }
                else if (res == 0x04)
                {
                    result = string.Format("Sense shorted to power (0x{0:X2})", res);
                }
                else if (res == 0x05)
                {
                    result = string.Format("Data and Sense shorted (0x{0:X2})", res);
                }
                else if (res == 0x06)
                {
                    result = string.Format("Sense and Data shorted (0x{0:X2})", res);
                }
                else if (res != 0x00)
                {
                    result = string.Format("Unknown self test result (0x{0:X2})", res);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (ap != null)
                    {
                        ap.Close();
                    }
                }
                catch (System.IO.IOException ex)
                {
                    Logger.Warn("could not close serial port: {0}", ex.Message);
                }
            }

            return result;
        }

        public static void Keyload(string port, bool useActiveKeyset, int keysetId, int sln, int keyId, int algId, List<byte> key)
        {
            if (port == string.Empty)
            {
                throw new ArgumentException("port empty");
            }

            AdapterProtocol ap = null;

            try
            {
                ap = new AdapterProtocol(port);

                ap.Open();

                ap.Clear();

                ManualRekeyApplication mra = new ManualRekeyApplication(ap);

                mra.Keyload(useActiveKeyset, keysetId, sln, keyId, algId, key);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (ap != null)
                    {
                        ap.Close();
                    }
                }
                catch (System.IO.IOException ex)
                {
                    Logger.Warn("could not close serial port: {0}", ex.Message);
                }
            }
        }

        public static void EraseKey(string port, bool useActiveKeyset, int keysetId, int sln)
        {
            if (port == string.Empty)
            {
                throw new ArgumentException("port empty");
            }

            AdapterProtocol ap = null;

            try
            {
                ap = new AdapterProtocol(port);

                ap.Open();

                ap.Clear();

                ManualRekeyApplication mra = new ManualRekeyApplication(ap);

                mra.EraseKeys(useActiveKeyset, keysetId, sln);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (ap != null)
                    {
                        ap.Close();
                    }
                }
                catch (System.IO.IOException ex)
                {
                    Logger.Warn("could not close serial port: {0}", ex.Message);
                }
            }
        }

        public static void EraseAllKeys(string port)
        {
            if (port == string.Empty)
            {
                throw new ArgumentException("port empty");
            }

            AdapterProtocol ap = null;

            try
            {
                ap = new AdapterProtocol(port);

                ap.Open();

                ap.Clear();

                ManualRekeyApplication mra = new ManualRekeyApplication(ap);

                mra.EraseAllKeys();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (ap != null)
                    {
                        ap.Close();
                    }
                }
                catch (System.IO.IOException ex)
                {
                    Logger.Warn("could not close serial port: {0}", ex.Message);
                }
            }
        }

        public static List<RspKeyInfo> ViewKeyInfo(string port)
        {
            List<RspKeyInfo> result = new List<RspKeyInfo>();

            if (port == string.Empty)
            {
                throw new ArgumentException("port empty");
            }

            AdapterProtocol ap = null;

            try
            {
                ap = new AdapterProtocol(port);

                ap.Open();

                ap.Clear();

                ManualRekeyApplication mra = new ManualRekeyApplication(ap);

                result.AddRange(mra.ViewKeyInfo());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                try
                {
                    if (ap != null)
                    {
                        ap.Close();
                    }
                }
                catch (System.IO.IOException ex)
                {
                    Logger.Warn("could not close serial port: {0}", ex.Message);
                }
            }

            return result;
        }
    }
}
