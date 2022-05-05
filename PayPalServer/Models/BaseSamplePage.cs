using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPalServer.Models
{
    public abstract class BaseSamplePage
    {
        protected RequestFlow flow;

        public RequestFlow GetFlow()
        {
            this.RegisterSampleRequestFlow();
            try
            {
                this.RunSample();
            }
            catch (Exception ex)
            {
                this.flow.RecordException(ex);
                throw ex;
            }
            return flow;
        }
        protected abstract void RunSample();

        protected void RegisterSampleRequestFlow()
        {
            if(this.flow == null)
            {
                this.flow = new RequestFlow();
            }
            HttpContext.Current.Items["Flow"] = this.flow;
        }
    }
}