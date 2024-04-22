using NUnit.Framework;
using AppTarea.Dominio.Interfaces.Repositorio;
using AppTarea.Infraestructura.APII.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using AppTarea.Aplicacion.Interfaces;
using AppTarea.Dominio.Response;
using AppTarea.Infraestructura.DTO.DTOs;
using System.Net;
using AppTarea.Dominio.Modelos;
using Microsoft.Extensions.Configuration;

namespace AppTarea.Tests
{
    public class UsuarioTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CuandoMeLogueo()
        {
            //Arrange 
            IServicioLogin<Login> servicioLogin = Substitute.For<IServicioLogin<Login>>();
            IConfiguration configuration = Substitute.For<IConfiguration>();    
            UsuarioController usuarioController = new UsuarioController(servicioLogin, configuration);
            Login Login = new Login()
            {
                userName = "Mauricio",
                password = "mauricio123"
            };

            //Act
            var result = usuarioController.Post(Login);

            //Assert
            Assert.IsNotNull(result);

            //Assert.AreEqual((int)HttpStatusCode.OK, result.sta);

        }
        [Test]
        public void cuandoCreoUnUsuario()
        {
            //Arrange 
            IServicioLogin<Login> servicioLogin = Substitute.For<IServicioLogin<Login>>();
            IConfiguration configuration = Substitute.For<IConfiguration>();
            UsuarioController usuarioController = new UsuarioController(servicioLogin, configuration);
            UsuarioDTO Usuario = new UsuarioDTO()
            {
                usuarioId = new Guid(),
                nombre = "Mauricio",
                Contrasena = "123456789k0",
                Creado = DateTime.Now,
                CreadoPor = "prueba",
                Actualizado = DateTime.Now,
                ActualizadoPor = "prueba"
            };
            

            //Act
            var result = usuarioController.Agregar(Usuario);

            //Assert
            Assert.IsNotNull(result);

            //Assert.AreEqual((int)HttpStatusCode.OK, result.sta);

        }

       
    }
}