﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using Ara2;
using Ara2.Components;
using Ara2.Dev;

namespace Ara2.Grid
{
    [Serializable]
    [AraDevComponent(vConteiner:false)]
    public class AraFormSearchLinqButton : AraButton,IAraDev
    {
        [AraDevEvent]
        public AraEvent<AraWindow.DAraWindowUnload> Return = new AraEvent<AraWindow.DAraWindowUnload>();

        [AraDevEvent]
        public AraEvent<Func<IQueryable<object>>> GetQuery = new AraEvent<Func<IQueryable<object>>>();
        

        public AraEvent<AraGridSearchLinq.dOnCommitBefore> OnCommitBefore = new AraEvent<AraGridSearchLinq.dOnCommitBefore>();

        public AraFormSearchLinqButton(IAraObject vConteiner, Func<IQueryable<object>> vQuery, AraWindow.DAraWindowUnload vReturnDial)
            : this(vConteiner, vQuery, null, vReturnDial)
        { }

        public AraFormSearchLinqButton(IAraObject vConteiner, Func<IQueryable<object>> vQuery, AraGridSearchLinq.dOnCommitBefore vOnCommitBefore, AraWindow.DAraWindowUnload vReturnDial)
            : this(vConteiner)
        {
            GetQuery += vQuery;
            Return += vReturnDial;
            OnCommitBefore += vOnCommitBefore;
        }

        public AraFormSearchLinqButton(IAraObject vConteiner)
            : base(vConteiner)
        {
            Construct();          
        }

        void Construct()
        {
            this.Click += OnClick;
            this.Text = "";
            this.Ico = ButtonIco.search;
            this.Width = 30;
        }

        private void OnClick (object sender, EventArgs e)
        {
            if (GetQuery.InvokeEvent != null)
            {
                AraFormSearchLinq AraFormSearchFB = new AraFormSearchLinq(this, GetQuery.InvokeEvent,  OnCommitBefore.InvokeEvent);
                AraFormSearchFB.Show(Return.InvokeEvent);
            }
        }

        public void ShowSearch()
        {
            OnClick(this, new EventArgs());
        }


        #region Ara2Dev
        

        #endregion
    }
}
