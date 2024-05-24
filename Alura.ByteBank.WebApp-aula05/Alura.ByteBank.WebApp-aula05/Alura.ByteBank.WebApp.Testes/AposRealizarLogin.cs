using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Alura.ByteBank.WebApp.Testes
{
    public class AposRealizarLogin
    {

        private IWebDriver driver;
        public ITestOutputHelper SaidaConsoleTeste;

        public AposRealizarLogin(ITestOutputHelper _saidaConsoleTeste)
        {
            driver = new ChromeDriver(Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().Location));

            SaidaConsoleTeste = _saidaConsoleTeste;
        }
        [Fact]
        public void AposRealizarLoginVerificaSeExisteOpcaoAgenciaMenu()
        {


            //Arrange

            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");
            var login = driver.FindElement(By.Id("Email"));//Selecionar elementos do HTML
            var senha = driver.FindElement(By.Id("Senha"));//Selecionar elementos do HTML
            var btnLogar = driver.FindElement(By.Id("btn-logar"));//Selecionar elementos do HTML

            login.SendKeys("andre@email.com");
            senha.SendKeys("senha01");

            //act - Faz o login
            btnLogar.Click();

            //Assert
            Assert.Contains("Agência", driver.PageSource);

        }

        [Fact]
        public void TentaRealizarLoginSemPreencherCampos()
        {

            //Arrange   
            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");
            var login = driver.FindElement(By.Id("Email"));//Selecionar elementos do HTML
            var senha = driver.FindElement(By.Id("Senha"));//Selecionar elementos do HTML
            var btnLogar = driver.FindElement(By.Id("btn-logar"));//Selecionar elementos do HTML

            //login.SendKeys("andre@email.com");
            //senha.SendKeys("senha01");

            //act - Faz o login
            btnLogar.Click();

            //Assert
            Assert.Contains("The Email field is required.", driver.PageSource);
            Assert.Contains("The Senha field is required.", driver.PageSource);

        }

        [Fact]
        public void TentaRealizarLoginComSenhaInvalida()
        {
            //Arrange          
            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");
            var login = driver.FindElement(By.Id("Email"));//Selecionar elementos do HTML
            var senha = driver.FindElement(By.Id("Senha"));//Selecionar elementos do HTML
            var btnLogar = driver.FindElement(By.Id("btn-logar"));//Selecionar elementos do HTML

            login.SendKeys("andre@email.com");
            senha.SendKeys("senha010");//senha inválida.

            //act - Faz o login
            btnLogar.Click();

            //Assert
            Assert.Contains("Login", driver.PageSource);

        }

        [Fact]
        public void RealizarLoginAcessaMenuECadastraCliente()
        {

            //Arrange
            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");

            var login = driver.FindElement(By.Name("Email"));
            var senha = driver.FindElement(By.Name("Senha"));

            login.SendKeys("andre@email.com");
            senha.SendKeys("senha01");

            driver.FindElement(By.Id("btn-logar")).Click();

            driver.FindElement(By.LinkText("Cliente")).Click();

            driver.FindElement(By.LinkText("Adicionar Cliente")).Click();

            driver.FindElement(By.Name("Identificador")).Click();
            driver.FindElement(By.Name("Identificador")).
                SendKeys("2df71922-ca7d-4d43-b142-0767b32f822a");
            driver.FindElement(By.Name("CPF")).Click();
            driver.FindElement(By.Name("CPF")).SendKeys("69981034096");
            driver.FindElement(By.Name("Nome")).Click();
            driver.FindElement(By.Name("Nome")).SendKeys("Tobey Garfield");
            driver.FindElement(By.Name("Profissao")).Click();
            driver.FindElement(By.Name("Profissao")).SendKeys("Cientista");

            //Act
            driver.FindElement(By.CssSelector(".btn-primary")).Click();
            driver.FindElement(By.LinkText("Home")).Click();

            //Assert 
            Assert.Contains("Logout", driver.PageSource);
        }

        [Fact]
        public void RealizarLoginAcessaListagemDeContas()
        {

            //Arrange
            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");
            var login = driver.FindElement(By.Name("Email"));
            var senha = driver.FindElement(By.Name("Senha"));
            login.SendKeys("andre@email.com");
            senha.SendKeys("senha01");
            driver.FindElement(By.Id("btn-logar")).Click();

            //Conta Corrente
            driver.FindElement(By.Id("contacorrente")).Click();

            IReadOnlyCollection<IWebElement> elements =
                driver.FindElements(By.TagName("a"));

            var elemento = (from webElemento in elements
                            where webElemento.Text.Contains("Detalhes")
                            select webElemento).First();

            //Act
            elemento.Click();

            //Assert 
            Assert.Contains("Voltar", driver.PageSource);



        }


    }
}
