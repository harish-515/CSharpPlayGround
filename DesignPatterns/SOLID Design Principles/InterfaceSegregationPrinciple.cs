using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPlayGrond.DesignPatterns.SOLID_Design_Principles
{
    //public interface IMachine
    //{
    //    void Print(Document d);
    //    void Scan(Document d);
    //    void Fax(Document d);
    //}

     public interface IPrinterMachine
    {
        void Print(Document d);
    }

    public interface IScannerMachine
    {
        void Scan(Document d);
    }

    public interface IFaxMachine
    {
        void Fax(Document d);
    }

    public class Document
    {

    }

    public class ModernPrinter : IPrinterMachine,IFaxMachine,IScannerMachine//IMachine
    {
        public void Fax(Document d)
        {
            //
        }

        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }

    public class OldPrinter : IPrinterMachine//IMachine
    {
        //public void Fax(Document d)
        //{
        //    throw new NotImplementedException();
        //}

        public void Print(Document d)
        {
            //
        }

        //public void Scan(Document d)
        //{
        //    throw new NotImplementedException();
        //}
    }


    static class InterfaceSegregationPrincipleDemo
    {
    }
}
