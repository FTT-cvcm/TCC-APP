﻿using App3.Data;
using App3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace App3.ViewModels
{
    public class RelatorioAnalisesViewModel
    {
        ObservableCollection<Analise> listaAnalise = new ObservableCollection<Analise>();

        public ObservableCollection<Analise> ListaAnalise
        {
            get
            {
                return listaAnalise;
            }
            set
            {
                listaAnalise = value;              
            }

        }

        public RelatorioAnalisesViewModel()
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConnection())
            {
                AnaliseDAO dao = new AnaliseDAO(conexao);
                var listadb = dao.Lista;
                this.listaAnalise.Clear();
                foreach (var itemDB in listadb)
                {
                    this.listaAnalise.Add(itemDB);
                }

            }

        }



    }
}
