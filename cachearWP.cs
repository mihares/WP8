/*funcion que cache en disco los json*/
    static class Cache
    {
        //To write a file:

        public static async void CachearContenido(string contenido, string nombre)
        {
            try {
                string _nombreArchivo = LimpiarNombre(nombre);
                IStorageFolder applicationFolder = ApplicationData.Current.LocalFolder;
                IStorageFile storageFile = await applicationFolder.CreateFileAsync(_nombreArchivo, CreationCollisionOption.ReplaceExisting);

                using (Stream stream = await storageFile.OpenStreamForWriteAsync()) {
                    byte[] content = Encoding.UTF8.GetBytes(contenido);
                    await stream.WriteAsync(content, 0, content.Length);
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        //To read a file:
        public async static Task<String> ObtenerCache(string nombre)
        {
            try {
                string _nombreArchivo = LimpiarNombre(nombre);
            
                IStorageFolder applicationFolder = ApplicationData.Current.LocalFolder;
                IStorageFile storageFile = await applicationFolder.GetFileAsync(_nombreArchivo);

                IRandomAccessStream accessStream = await storageFile.OpenReadAsync();
                string text=null;
                using (Stream stream = accessStream.AsStreamForRead((int)accessStream.Size))
                {
                    byte[] content = new byte[stream.Length];
                    await stream.ReadAsync(content, 0, (int)stream.Length);
                    text = Encoding.UTF8.GetString(content, 0, content.Length);
                }

                return text;
            } catch (Exception) {
                return null;
            }
        }

	/*Clear special chars*/
        public static string LimpiarNombre(string nombre)
        {
            return System.Text.RegularExpressions.Regex.Replace(nombre, "[^0-9A-Za-z]", "", System.Text.RegularExpressions.RegexOptions.None);
        }
    }