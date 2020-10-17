using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App3.Data;
using App3;
using SQLite;
using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;
using App3.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Android))]
namespace App3.Droid
{
    class SQLite_Android : ISQLite
    {
        private const string nomeArquivoDB = "AnaliseDB.db3";
        public SQLiteConnection PegarConnection()
        {
            //Cria uma pasta base local para o dispositivo
            var path = new LocalRootFolder();
            //Cria o arquivo
            var arquivo = path.CreateFile(nomeArquivoDB, CreationCollisionOption.OpenIfExists);

            return new SQLite.SQLiteConnection(arquivo.Path);
        }
    }
}