using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RovaPOS.Manager
{
    public class ImagenSQLite
    {
        Server server = new Server();
        public System.Drawing.Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            System.Drawing.Bitmap bitmap;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new System.Drawing.Bitmap(outStream);
            }
            return bitmap;
        }
        public  System.Drawing.Bitmap BitmapSourceToBitmap2(BitmapSource srs)
        {
            int width = srs.PixelWidth;
            int height = srs.PixelHeight;
            int stride = width * ((srs.Format.BitsPerPixel + 7) / 8);
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(height * stride);
                srs.CopyPixels(new Int32Rect(0, 0, width, height), ptr, height * stride, stride);
                using (var btm = new System.Drawing.Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format1bppIndexed, ptr))
                {
                    // Clone the bitmap so that we can dispose it and
                    // release the unmanaged memory at ptr
                    return new System.Drawing.Bitmap(btm);
                }
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptr);
            }
        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();
            BitmapImage retval;

            try
            {
                retval = (BitmapImage)Imaging.CreateBitmapSourceFromHBitmap(
                             hBitmap,
                             IntPtr.Zero,
                             Int32Rect.Empty,
                             BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(hBitmap);
            }

            return retval;
        }
        public Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }
        public byte[] ImageToByte(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();
                return imageBytes;
            }
        }
        public byte[] ImageBitmapToByte(BitmapImage image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                //image.sa(ms, format);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                image = new BitmapImage();
                img.Save(ms, format);
                byte[] imageBytes = ms.ToArray();
                return imageBytes;
            }
        }
        public  BitmapImage BitmapImageFromBytes(byte[] bytes)
        {
            BitmapImage image = null;
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(bytes);
                stream.Seek(0, SeekOrigin.Begin);
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                image = new BitmapImage();
                image.BeginInit();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                image.StreamSource = ms;
                image.StreamSource.Seek(0, SeekOrigin.Begin);
                image.EndInit();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                stream.Close();
                stream.Dispose();
            }
            return image;
        }
        public BitmapImage LoadImageByte(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
        public ImageSource ByteToImageSource(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }
        //public Image Base64ToImage(string base64String)
        public BitmapImage ImageDrawingToImageSource(Image image )
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
        public Image ByteToImage(byte[] imageBytes)
        {
            // Convert byte[] to Image
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = new Bitmap(ms);
            return image;
        }
        /***************** SQLite **************************/
        public void SaveImage(byte[] imagen , string TableName , string FieldName)
        {
            string InsertString = "INSERT INTO "+ TableName + " ("+ FieldName + ") VALUES (@imagen);";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(InsertString, conn))
                {
                    SQLiteParameter param = new SQLiteParameter("@imagen", System.Data.DbType.Binary);
                    param.Value = imagen;
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                }
            } 
           
        }
        public BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
        public BitmapImage LoadImage(string TableName, string Barcode)
        {
            BitmapImage image = null;
            string ReadString = "SELECT * FROM " + TableName + " WHERE Barcode=@barcode ";
            using (SQLiteConnection conn = new SQLiteConnection(server.connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(ReadString, conn))
                {
                    cmd.Parameters.AddWithValue("@barcode", Barcode);
                    try
                    {
                        SQLiteDataReader reader = cmd.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {

                              
                                if (!Convert.IsDBNull(reader["Image"]))
                                {
                                    byte[] a = (byte[])(reader["Image"]);
                                    using (var stream = new MemoryStream(a))
                                    {
                                        var bitmap = (BitmapSource)new ImageSourceConverter().ConvertFrom(a);
                                        //return BitmapFrame.Create(stream,
                                        //                        BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                                        return ToImage(a);
                                    }
                                }
                                else
                                {
                                    return null;
                                }
                               
                                //image = ByteToImage(a);
                            }
                        }
                        catch (Exception exc) { MessageBox.Show(exc.Message); }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return image;
                    }
                }
            }
            return image;
        }
        public string FindRelativePath(string hreflink)
        {
            string filename = "";
            Uri uri = new Uri(hreflink);
            if (uri.IsFile)
            {
               filename = System.IO.Path.GetFileName(uri.LocalPath);
               
            }
            return filename;
        }
    }
}
