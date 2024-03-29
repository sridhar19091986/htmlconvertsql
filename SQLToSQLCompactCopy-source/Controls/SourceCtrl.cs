﻿//* Copyright (c) 2008, Scott "Johnny" Smith (www.JohnnyCantCode.com)
//* All rights reserved.
//*
//* Redistribution and use in source and binary forms, with or without
//* modification, are permitted provided that the following conditions are met:
//*     * Redistributions of source code must retain the above copyright
//*       notice, this list of conditions and the following disclaimer.
//*     * Redistributions in binary form must reproduce the above copyright
//*       notice, this list of conditions and the following disclaimer in the
//*       documentation and/or other materials provided with the distribution.
//*     * Neither the name of the <organization> nor the
//*       names of its contributors may be used to endorse or promote products
//*       derived from this software without specific prior written permission.
//*
//* THIS SOFTWARE IS PROVIDED BY Scott "Johnny" Smith ``AS IS'' AND ANY
//* EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//* WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//* DISCLAIMED. IN NO EVENT SHALL <copyright holder> BE LIABLE FOR ANY
//* DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
//* (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//* LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
//* ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
//* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//* SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace GLT.SqlCopy.Controls
{
    public partial class SourceCtrl : UserControl
    {

        private Server _SourceServer = null;
        public Server SourceServer
        {
            get { return _SourceServer; }
        }

        public string DatabaseName
        {
            get { return ddlCatalog.Text; }
        }


        public SourceCtrl()
        {
            InitializeComponent();
            ddlAuthType.SelectedItem = "Windows Authentication";
        }

        private void ddlCatalog_DropDown(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            if (ddlAuthType.Text == "SQL Server Authentication")
            {
                ServerConnection svrConn = new ServerConnection(tbServerName.Text);
                svrConn.LoginSecure = false;
                svrConn.Login = tbLogin.Text;
                svrConn.Password = tbPassword.Text;
                _SourceServer = new Server(svrConn);
            }
            else
                _SourceServer = new Server(tbServerName.Text);

            ddlCatalog.Items.Clear();

            try
            {
                foreach (Database db in _SourceServer.Databases)
                {
                    ddlCatalog.Items.Add(db.Name);
                }
            }
            catch (ConnectionFailureException ex)
            {
                MessageBox.Show(this, ex.Message, "Connection Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ddlAuthType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAuthType.Text == "Windows Authentication")
            {
                tbLogin.Enabled = false;
                tbPassword.Enabled = false;

                lblLogin.Enabled = false;
                lblPassword.Enabled = false;
            }
            else
            {
                tbLogin.Enabled = true;
                tbPassword.Enabled = true;

                lblLogin.Enabled = true;
                lblPassword.Enabled = true;

                tbLogin.Focus();
                tbLogin.SelectAll();
            }
        }

        private void SourceCtrl_Load(object sender, EventArgs e)
        {

        }
    }
}
