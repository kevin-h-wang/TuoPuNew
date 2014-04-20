using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Whf.TuoPu.Common;
using System.Configuration;
using System.IO;
using System.Threading;
using Whf.TuoPu.Entity;

namespace Whf.TuoPu.Web
{
    public class LoginBasePage:Page
    {
        public PersonEntity PersonEntity
        {
            set
            {
                PersonEntity pe = value;
                Session["PersonOID"] = pe.OID;
                Session["PersonAccount"] = pe.PERSONACCOUNT;
                Session["PersonName"] = pe.PERSONNAME;
                Session["PersonType"] = pe.PERSONTYPE;
                Session["PersonStatus"] = pe.PERSONSTATUS;
                Session["PersonSex"] = pe.PERSONSEX;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            try
            {
                string m_rootPath = HttpContext.Current.Request.PhysicalApplicationPath;
                ConfigManager _cm = new ConfigManager(Path.Combine(m_rootPath, ConfigurationSettings.AppSettings["PalauGlobalConfig"]));
                Thread.GetDomain().SetData("ConfigManager", _cm);
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}