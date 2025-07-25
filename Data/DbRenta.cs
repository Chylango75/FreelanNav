using Microsoft.AspNetCore.Mvc;
using MvcFreelan.Helpers;
using MvcFreelan.Models;
using MvcFreelan.Models.Rentas;
using Rentas.Models;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;

namespace MvcFreelan.Data
{
    public class DbRenta
    {
        private IConfiguration _configuration;
        string strConn = "";
        //string strConn = "Data Source=VitralRyzen7;Initial Catalog=rentadb;User ID=Kermit;Password=1234";

        //public DbRenta(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    strConn = _configuration.GetConnectionString("conn");
        //}

        public DbRenta(string _strConn)
        {
            strConn = _strConn;
        }

        /////////////////         Iuilinos

        public List<Inquilino> InquisGetN(string cad)
        {
            var inquis = new List<Inquilino>();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand cmd = new SqlCommand("InquisGetN", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@cad", cad));
                conn.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var i = new Inquilino();
                        i.Id = rdr.GetInt32(0);
                        i.Nombre = rdr.GetString(1);
                        i.Active = rdr.GetBoolean(2);
                        i.FechaCaptura = rdr.GetDateTime(3);
                        i.CostoMensual = rdr.IsDBNull(4) ? 0 : rdr.GetDecimal(4);
                        inquis.Add(i);
                    }
                }
            }
            return inquis;
        }

        public List<Inquilino> InquisSinContrato(string cad)
        {
            var inquis = new List<Inquilino>();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand cmd = new SqlCommand("InquisSinContrato", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@cad", cad));
                conn.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var i = new Inquilino();
                        i.Id = rdr.GetInt32(0);
                        i.Nombre = rdr.GetString(1);
                        i.Active = rdr.GetBoolean(2);
                        i.FechaCaptura = rdr.GetDateTime(3);
                        inquis.Add(i);
                    }
                }
            }
            return inquis;
        }

        public string InquiAdd(Inquilino inquilino)
        {
            try
            {
                string strResp = "ERROR ";
                int intRes = -1;

                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("InquiAdd", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@cad", inquilino.Nombre));
                    cmd.CommandType = CommandType.StoredProcedure;

                    intRes = Convert.ToInt32(cmd.ExecuteScalar());
                }

                if(intRes > 0)
                    strResp = "OK, Inquilino Agregado con ID: " + intRes;

                return strResp;
            }
            catch(Exception ex)
            {
                return "ERROR INQUI: " + ex.Message;
            }
        }

        /////////////////         Contratos
        public List<Contrato> ContratosGetN(string cad)
        {
            var res = new List<Contrato>();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand cmd = new SqlCommand("ContratosGetN", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@cad", cad));
                conn.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var c = new Contrato();
                        c.ContratoId = rdr.GetInt32(0);
                        c.InquilinoId = rdr.GetInt32(1);
                        c.MesIni = rdr.GetDateTime(2);
                        c.MesFin = rdr.GetDateTime(3);
                        c.NumContrato = rdr.GetString(4);
                        c.CostoMensual = rdr.GetDecimal(5);
                        c.FechaCreacion = rdr.GetDateTime(6);
                        c.NombreInqui = rdr.GetString(8);
                        res.Add(c);
                    }
                }
            }

            return res;
        }

        public string ContratoAdd(Contrato contrato)
        {
            string resp = "ERROR";
            int newId = 0;
            
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("ContratoAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@InquiId", contrato.InquilinoId));
                cmd.Parameters.Add(new SqlParameter("@MesIni", contrato.MesIni));
                cmd.Parameters.Add(new SqlParameter("@NumContrato", contrato.NumContrato));
                cmd.Parameters.Add(new SqlParameter("@CostoMensual", contrato.CostoMensual));

                newId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            resp = "OK, Contrato agregado con ID: " + newId.ToString();
            return resp;
        }

        /////////////////         Pagos

        public string PagoAdd(Pago pago)
        {
            int intRes = 0;
            string resp = "ERROR";

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("PagoAdd", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@InquiId", pago.InquilinoId));
                cmd.Parameters.Add(new SqlParameter("@Total", pago.PagoTotal));
                cmd.Parameters.Add(new SqlParameter("@FechaCubierta", pago.FechaAplica));
                cmd.CommandType = CommandType.StoredProcedure;
                intRes = Convert.ToInt32(cmd.ExecuteScalar());
            }

            if (intRes > 1)
                return "OK, Pago agregado con Id: " + intRes.ToString();

            return "ERROR";
        }


        public List<Pago> PagosByInqui(string cad)
        {
            var pagos = new List<Pago>();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand cmd = new SqlCommand("PagosByInqui", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@InquiId", cad));
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var p = new Pago();
                        p.MesIni = rdr.IsDBNull(0) ? "sin valor " : rdr.GetDateTime(0).ToString();
                        p.MesFin = rdr.IsDBNull(1) ? "sin valor " : rdr.GetDateTime(1).ToString();
                        p.PagoTotal = rdr.IsDBNull(2) ? 0 : rdr.GetDecimal(2);
                        p.FechaAplica = rdr.IsDBNull(3) ? "sin valor " : rdr.GetDateTime(3).ToString();
                        pagos.Add(p);
                    }
                }
            }
            return pagos;
        }

        /////////////////         xTras

        public string VerifConnOLD(string cadConn)
        {
            try
            {
                string resp = "ERROR: No Conn";

                using (SqlConnection conn = new SqlConnection(cadConn))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("VerifConn", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                            resp = rdr.GetString(0);
                    }
                }

                return resp;
            }
            catch(Exception ex)
            {
                return "ERROR INQUI:" + ex.Message;
            }
        }

        public string VerifConn()
        {
            try
            {
                string resp = "ERROR: No Conn";

                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("VerifConn", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                            resp = rdr.GetString(0);
                    }
                }

                return resp;
            }
            catch (Exception ex)
            {
                return "ERROR INQUI:" + ex.Message;
            }
        }

    }
}
