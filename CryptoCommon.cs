public class CryptoCommon
{
  public string AESKey{get;set;}
  public string AESVector{get;set;}
  public string DESKey{get;set;}
  
  public CryptoCommon()
  {
    this.AESKey="BD8EA36DEE6D48CCB279376AC9C3CBF9";
    this.AESVector="93605B4AD6468C3E";
    this.DESKey="ehaiserv";//8位
  }
  public string AESDecrypt(string Data)
        {
            return this.AESDecrypt(Data, this.AESKEY, this.AESVECTOR);
        }

        public string AESDecrypt(string Data, string Key)
        {
            return this.AESDecrypt(Data, Key, this.AESVECTOR);
        }

        public string AESDecrypt(string Data, string Key, string Vector)
        {
            string str;
            Rijndael rijndael = Rijndael.Create();
            try
            {
                byte[] numArray = Convert.FromBase64String(Data);
                byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(Key);
                byte[] bytes1 = Encoding.GetEncoding("utf-8").GetBytes(Vector);
                MemoryStream memoryStream = new MemoryStream(numArray);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(bytes, bytes1), CryptoStreamMode.Read);
                MemoryStream memoryStream1 = new MemoryStream();
                byte[] numArray1 = new byte[1025];
                int i = 0;
                for (i = cryptoStream.Read(numArray1, 0, checked((int)numArray1.Length)); i > 0; i = cryptoStream.Read(numArray1, 0, checked((int)numArray1.Length)))
                {
                    memoryStream1.Write(numArray1, 0, i);
                }
                str = Encoding.GetEncoding("utf-8").GetString(memoryStream1.ToArray());
            }
            catch (Exception exception)
            {
                ProjectData.SetProjectError(exception);
                str = "";
                ProjectData.ClearProjectError();
            }
            return str;
        }

        public string AESEncrypt(string Data)
        {
            return this.AESEncrypt(Data, this.AESKEY, this.AESVECTOR);
        }

        public string AESEncrypt(string Data, string Key)
        {
            return this.AESEncrypt(Data, Key, this.AESVECTOR);
        }

        public string AESEncrypt(string Data, string Key, string Vector)
        {
            string base64String;
            Rijndael rijndael = Rijndael.Create();
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(Data);
            byte[] numArray = Encoding.GetEncoding("utf-8").GetBytes(Key);
            byte[] bytes1 = Encoding.GetEncoding("utf-8").GetBytes(Vector);
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(numArray, bytes1), CryptoStreamMode.Write);
                cryptoStream.Write(bytes, 0, checked((int)bytes.Length));
                cryptoStream.FlushFinalBlock();
                base64String = Convert.ToBase64String(memoryStream.GetBuffer(), 0, Convert.ToInt32(memoryStream.Length));
            }
            catch (Exception exception)
            {
                ProjectData.SetProjectError(exception);
                base64String = "";
                ProjectData.ClearProjectError();
            }
            return base64String;
        }

public static string DESDecrypt(string str, string key)
        {
            try
            {
                str = str.Replace('*', '+');
                byte[] rgbKey = System.Text.Encoding.UTF8.GetBytes(key);
                byte[] rgbIV = rgbKey;
                byte[] inputByteArray = Convert.FromBase64String(str);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, des.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return System.Text.Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return "";
            }
        }
        public static string DESEncrypt(string str, string key)
        {
            try
            {
                byte[] rgbKey = System.Text.Encoding.UTF8.GetBytes(key);
                byte[] rgbIV = rgbKey;
                byte[] inputByteArray = System.Text.Encoding.UTF8.GetBytes(str);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                System.IO.MemoryStream mStream = new System.IO.MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return "";
            }
        }
  
}
