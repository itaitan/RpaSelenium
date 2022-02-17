using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows;
using Anticaptcha_example.Api;
using Anticaptcha_example.Helper;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Automation;
using Application = System.Windows.Forms.Application;


namespace PrefeituraLoja2
{
    class Program
    {

        static string tempo, temp = "";
        static string ano = DateTime.Now.Year.ToString();
        static int TempMes = DateTime.Now.Month;

        static void Main(string[] args)
        {
            string connectionString = "";
                                  

            string mes = "";
            if (TempMes > 9)
            {

                mes = DateTime.Now.Month.ToString();

            }
            else
            {
                mes = "0" + DateTime.Now.Month.ToString();
            }

            //Região de retirar as informações do Forms
            string dataInicio, dataFim, datames;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            dataInicio = Form1.dtInicio.ToString();
            dataFim = Form1.dtFim.ToString();
            datames = Form1.dtMes.ToString();

            string queryInsert = "INSERT INTO logformprefeituraAruja (Datainicio, DataFim, DataMes, DataExecute) Values('" + dataInicio + "', '" + dataFim + ", '" + datames + "''Getdate())";

            

            SqlConnection conn = null;
            DataTable dttable = new DataTable();
            DataTable fornecedor = new DataTable();
            DataTable notavalor = new DataTable();
            DataTable servico = new DataTable();

            DataTable UltimaData = new DataTable();
            double valor;
            int nota, codfornec, id;
            int notaAnterior = 0;
                int codfornecAnterior = 0;
            string CNPJ, nome, cidade, estado, serv;
            //SqlDataReader reader = null;

            try
            {

                conn = new SqlConnection(connectionString);
                //Insert na tabela log do Forms para saber o que o usuario digitou!
                conn.Open();
                SqlCommand cmdComand = new SqlCommand(queryInsert, conn);
                //cmdComand.ExecuteNonQuery();
                conn.Close();

                conn.Open();

                SqlDataAdapter cmd = new SqlDataAdapter("Select  TOP 1 DTEMISSAO as DTEMISSAO from pcLanc WHERE INTEGRADO = 'S'" +
                    " and CONVERT(DATE,dtEmissao, 103) >= CONVERT(DATE, GETDATE() - DAY(GETDATE()) + 1, 103) and codfornec not in (137, 9184, 7843, 11446, 11444, 12236, 8904)" +
                    " order by CONVERT(DATE, dtEmissao, 103) desc", conn);
                String ultimo = "";
                cmd.Fill(UltimaData);


                if (UltimaData.Rows.Count > 0)
                {
                    ultimo = UltimaData.Rows[0]["DTEMISSAO"].ToString();
                }
                else
                {
                    ultimo = "01/" + mes + "/" + ano;
                }



                //SqlDataAdapter cmd2 = new SqlDataAdapter("Select * from pcLanc" +
                //    " WHERE INTEGRADO = 'N' " +
                //    " and CONVERT(DATE, dtEmissao, 103) >= CONVERT(DATE, '"+ ultimo + "', 103) " +
                //    " and codfornec not in (137, 9184, 7843, 11446, 11444, 12236, 8904) " +
                //    " order by CONVERT(DATE, dtEmissao, 103), numnota, codfornec", conn);

                // =============================================================================================================================
                // ===================================================== MUDAR A DATA AQUI =====================================================

                SqlDataAdapter cmd2 = new SqlDataAdapter("Select * from pcLanc " +
                                "WHERE INTEGRADO = 'N' " +
                                "and CONVERT(DATE, dtEmissao, 103) >= CONVERT(DATE, '"+ dataInicio +"', 103) " +
                                "and CONVERT(DATE, dtEmissao, 103) <= CONVERT(DATE, '"+ dataFim +"', 103) " +
                                "and codfornec not in (137, 9184, 7843, 11446, 11444, 12236, 8904, 15315, 14933, 16307, 16559, 16650) " +
                                "order by CONVERT(DATE, dtEmissao, 103), numnota, codfornec", conn);

                
                cmd2.Fill(dttable);

                if (dttable.Rows.Count == 0)
                {
                    Console.WriteLine("Sem Notas");
                }
                
            }
            finally
            {
                // Fecha a conexão
                if (conn != null)
                {
                    conn.Close();
                }
            }


            using (IWebDriver driver = new FirefoxDriver())
            {
                driver.Navigate().GoToUrl("http://servicos.prefeituradearuja.sp.gov.br:8080/tbw/loginCNPJContribuinte.jsp?execobj=ContribuintesWebRelacionados");
                driver.FindElement(By.Id("usuario")).SendKeys("");
                driver.FindElement(By.Id("senha")).SendKeys("");

                var filePath = "C:\\quaestum\\captcha\\";
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

                var img = driver.FindElement(By.XPath("/html/body/div/div/div[1]/form/div/div[6]/img"));
                Point location = img.Location;
                var screenshot = (driver as FirefoxDriver).GetScreenshot();

                using (MemoryStream stream = new MemoryStream(screenshot.AsByteArray))
                {
                    using (Bitmap bitmap = new Bitmap(stream))
                    {
                        RectangleF part = new RectangleF(location.X, location.Y, img.Size.Width, img.Size.Height);
                        using (Bitmap bn = bitmap.Clone(part, bitmap.PixelFormat))
                        {
                            // Salva imagem
                            bn.Save(filePath + "captcha.png", System.Drawing.Imaging.ImageFormat.Png);
                            // Resolve Captcha
                            ImageToText();
                            //System.Windows.Forms.MessageBox.Show("Paro");
                            // Preeche Captcha
                            driver.FindElement(By.Name("imagem")).SendKeys(tempo);
                            // Clica em acessar
                            driver.FindElement(By.XPath("/html/body/div/div/div[1]/form/div/div[9]/a[1]")).Click();

                            // espera frame aparecer
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.Id("_iFilho0")));

                            // vai para o frame desejado
                            IWebElement detailFrame = driver.FindElement(By.TagName("frame"));
                            driver.SwitchTo().Frame(detailFrame);
                            // espera imagem aparecer
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div[2]/img")));
                            driver.FindElement(By.XPath("/html/body/div/div[2]/img")).Click();
                            
                            // volta para o frame principal
                            driver.SwitchTo().DefaultContent();

                            // clica em declaração fiscal
                            driver.FindElement(By.Id("imgdeclaracaofiscal")).Click();
                            // clica em tomador de serviço
                            driver.FindElement(By.Id("imgtomadiss")).Click();
                            // informa mês e ano
                            mes = "01";
                            // =============================================================================================================================
                            // ===================================================== MUDAR A DATA AQUI =====================================================
                            driver.FindElement(By.Name("filterListaSearchValue")).SendKeys(datames);
                            //driver.FindElement(By.Name("filterListaSearchValue")).SendKeys(mesEAnoAtual);

                            Thread.Sleep(6000);
                            // clica em buscar
                             driver.FindElement(By.XPath("/ html / body / div[2] / div / form / section[2] / div / div / div[3] / div / div[1] / div[3] / div / div / button[1]")).Click();
                            // aguarda dados aparecer
                            Thread.Sleep(2000);
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("span1")));
                            // clica para entrar no mês
                            driver.FindElement(By.Id("span1")).Click();

                            // insere notas
                            foreach (DataRow linha in dttable.Rows)
                            {
                                Console.WriteLine(linha["ID"].ToString());

                                id = Convert.ToInt32(linha["ID"]);
                                nota = Convert.ToInt32(linha["NUMNOTA"]);
                                codfornec = Convert.ToInt32(linha["CODFORNEC"]);

                                // if (nota != notaAnterior && codfornec != codfornecAnterior)  Assim sem o else.
                                Console.WriteLine("Nota: "+ nota + " Nota Anterior: " + notaAnterior + " CodFornec: " + codfornec + " CodFornec Anterior: " + codfornecAnterior);
                                

                                //Verifica se a nota e CodFornec anterior é a mesma da atual 
                                if (nota == notaAnterior && codfornec == codfornecAnterior)
                                {
                                }
                                else
                                {

                                    // espera botão para incluir nota aparecer
                                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("_navigatorListabt6")));
                                    // clica em incluir nota
                                    driver.FindElement(By.Id("_navigatorListabt6")).Click();
                                    Thread.Sleep(2000);
                                    // espera dropdown aparecer
                                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"qyedicaocfps\"]")));
                                    // seleciona drop down list
                                    var Local = driver.FindElement(By.Id("qyedicaocfps"));
                                    // cria element object 
                                    var selectElement = new SelectElement(Local);
                                    // seleciona pelo valor
                                    selectElement.SelectByValue("2");
                                    // seleciona pelo texto
                                    // selectElement.SelectByText("2 - SERVIÇO PRESTADO FORA DO MUNICÍPIO");

                                    // espera dropdown aparecer
                                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("qyedicaoserie")));
                                    // seleciona drop down list
                                    Local = driver.FindElement(By.Id("qyedicaoserie"));
                                    // cria element object 
                                    selectElement = new SelectElement(Local);
                                    // seleciona pelo valor
                                    selectElement.SelectByValue("17");


                                    // seleciona radio button 
                                    driver.FindElement(By.Id("qyedicaotppessoa0")).Click();



                                    try
                                    {
                                        fornecedor.Clear();
                                        notavalor.Clear();
                                        servico.Clear();

                                        conn = new SqlConnection(connectionString);

                                        conn.Open();

                                        SqlDataAdapter cmd = new SqlDataAdapter("select * from fornecedor where codfornec = " + codfornec, conn);
                                        cmd.Fill(fornecedor);
                                        CNPJ = fornecedor.Rows[0]["CGC"].ToString();
                                        nome = fornecedor.Rows[0]["FORNECEDOR"].ToString();
                                        cidade = fornecedor.Rows[0]["CIDADE"].ToString();
                                        estado = fornecedor.Rows[0]["ESTADO"].ToString();

                                        cmd = new SqlDataAdapter("Select SUM(VALOR) as valor from pcLanc WHERE NUMNOTA = " + nota + " AND CODFORNEC = " + codfornec, conn);
                                        cmd.Fill(notavalor);
                                        temp = notavalor.Rows[0]["valor"].ToString();
                                        temp = temp.Replace(',', '.');
                                        //valor = Convert.ToDouble(notavalor.Rows[0]["valor"]);


                                        cmd = new SqlDataAdapter("select* from fornec_serv where codfornec = " + codfornec + " and codcidade = 2", conn);
                                        cmd.Fill(servico);

                                        if (servico.Rows.Count > 0)
                                        {
                                            serv = servico.Rows[0]["TipoServico"].ToString();
                                        }
                                        else
                                        {
                                            serv = "16.02";
                                        }


                                    }
                                    finally
                                    {
                                        // Fecha a conexão
                                        if (conn != null)
                                        {
                                            conn.Close();
                                        }
                                    }

                                    // serviço
                                    
                                    driver.FindElement(By.Id("suggest")).SendKeys(serv);

                                    driver.FindElement(By.XPath("/html/body/div[2]/div/form/section[2]/div/div/div[2]/div/div[2]/div[5]/div/i")).Click();
                                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".autoSuggestOver")));
                                    driver.FindElement(By.CssSelector(".autoSuggestOver")).Click();

                                    // Numero nota
                                    driver.FindElement(By.Id("qyedicaonfs")).SendKeys(nota.ToString());

                                    // emissão da nota
                                    driver.FindElement(By.Id("qyedicaodtemissao")).SendKeys(linha["DTEMISSAO"].ToString());

                                    // valor
                                    driver.FindElement(By.Id("qyedicaovlrservicos")).SendKeys(temp);
                                    //driver.FindElement(By.Id("qyedicaovlrservicos")).SendKeys(valor.ToString());


                                    //informações do fornecedor
                                    // CNPJ
                                    driver.FindElement(By.Id("qyedicaocnpjcpf")).SendKeys(CNPJ);

                                    Thread.Sleep(2500);
                                    SendKeys.SendWait("{Enter}");
                                    Thread.Sleep(500);

                                    // nome
                                    driver.FindElement(By.Id("qyedicaonome")).Clear();
                                    driver.FindElement(By.Id("qyedicaonome")).SendKeys(nome);
                                    // cidade
                                    driver.FindElement(By.Id("qyedicaocidade")).Clear();
                                    driver.FindElement(By.Id("qyedicaocidade")).SendKeys(cidade);

                                    // estado
                                    driver.FindElement(By.Id("qyedicaoestado")).SendKeys(estado);

                                    Thread.Sleep(1000);

                                    // espera dropdown aparecer
                                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("qyedicaoenquadradosn")));
                                    // seleciona drop down list
                                    Local = driver.FindElement(By.Id("qyedicaoenquadradosn"));
                                    // cria element object 
                                    selectElement = new SelectElement(Local);
                                    // seleciona pelo valor
                                    selectElement.SelectByValue("M");

                                    Thread.Sleep(1000);

                                    // espera dropdown aparecer
                                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("qyedicaoimpretido")));
                                    // seleciona drop down list
                                    Local = driver.FindElement(By.Id("qyedicaoimpretido"));
                                    // cria element object 
                                    selectElement = new SelectElement(Local);
                                    // seleciona pelo valor
                                    selectElement.SelectByValue("Z");

                                    //System.Windows.MessageBox.Show("ok");

                                    driver.FindElement(By.Id("__closebuttonstab1btOk")).Click();

                                    //Salva o numero da nota e do cod Fornec para verificar la em cima se é o mesmo 2x seguidas
                                    notaAnterior = Convert.ToInt32(linha["NUMNOTA"]);
                                    codfornecAnterior = Convert.ToInt32(linha["CODFORNEC"]);

                                }

                                try
                                {

                                    conn = new SqlConnection(connectionString);

                                    conn.Open();

                                    SqlCommand cmd = new SqlCommand("update pclanc set integrado = 'S' where id = " + id, conn);

                                    try
                                    {
                                        cmd.ExecuteNonQuery();
                                        
                                    }
                                    catch (Exception ex)
                                    {
                                        System.Windows.MessageBox.Show("Erro: " + ex.ToString());
                                    }
                                    finally
                                    {
                                        conn.Close();
                                    }

                                    conn.Open();

                                    SqlCommand cmd2 = new SqlCommand("UPDATE LOG_EXECUCAO SET DTFIM = GETDATE() where  dtFim IS NULL and codfilial = 2", conn);

                                    try
                                    {
                                        cmd2.ExecuteNonQuery();

                                    }
                                    catch (Exception ex)
                                    {
                                        System.Windows.MessageBox.Show("Erro: " + ex.ToString());
                                    }
                                    finally
                                    {
                                        conn.Close();
                                    }


                                }
                                finally
                                {
                                    // Fecha a conexão
                                    if (conn != null)
                                    {
                                        conn.Close();
                                    }
                                }

                            }
                        }
                    }

                }
            }
        }

        private static void ImageToText()
        {
            DebugHelper.VerboseMode = true;

            var api = new ImageToText
            {
                ClientKey = "",
                FilePath = ""
            };

            if (!api.CreateTask()) 
            {
                DebugHelper.Out("API v2 send failed. " + api.ErrorMessage, DebugHelper.Type.Error);
            }
            else if (!api.WaitForResult())
            {
                DebugHelper.Out("Could not solve the captcha.", DebugHelper.Type.Error);
            }
            else
            {
                tempo = api.GetTaskSolution().Text;
            }

        }
    }
}

