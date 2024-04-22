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

namespace AppTarea.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void cuandoConsultoTareas()
        {
            //Arrange 
            IServicioBase<Tarea, Guid> repositorioBase = Substitute.For<IServicioBase<Tarea, Guid>>();
            TareaController tareaController = new TareaController(repositorioBase);
            
            //Act
            var result = tareaController.Get();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void cuandoConsultoTareasPorId()
        {
            //Arrange 
            IServicioBase<Tarea, Guid> repositorioBase = Substitute.For<IServicioBase<Tarea, Guid>>();
            TareaController tareaController = new TareaController(repositorioBase);

            //Act
            var result = tareaController.Get(new Guid("6BD5C8BD-6058-455B-806A-F5D0B13DB454"));

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void cuandoAgregoTareas()
        {
            //Arrange 
            IServicioBase<Tarea, Guid> repositorioBase = Substitute.For<IServicioBase<Tarea, Guid>>();
            TareaController tareaController = new TareaController(repositorioBase);
            TareaDTO tarea = new TareaDTO()
            {
                Creado = DateTime.Now,
                CreadoPor = "userTarea",
                Actualizado = DateTime.Now,
                ActualizadoPor = "userTarea",
                tareaId = Guid.NewGuid(),
                fechaAdicion = DateTime.Now,
                descripcion = "Tarea del hogar",
                estado = "Nueva",
                prioridad = "Baja",
                fechaInicio = DateTime.Now,
                fechaFin = DateTime.Now.AddDays(10),
                comentarios = "Esta tarea me llevara mucho tiempo",
                Usuarios = null
            };

            //Act
            var result =  tareaController.Post(tarea);

            //Assert
            Assert.IsNotNull(result);

            //Assert.AreEqual((int)HttpStatusCode.OK, result.sta);

        }
        [Test]
        public void cuandoEditoTareas()
        {
            //Arrange 
            IServicioBase<Tarea, Guid> repositorioBase = Substitute.For<IServicioBase<Tarea, Guid>>();
            TareaController tareaController = new TareaController(repositorioBase);
            TareaDTO tarea = new TareaDTO()
            {
                Creado = DateTime.Now,
                CreadoPor = "userTarea",
                Actualizado = DateTime.Now,
                ActualizadoPor = "userTarea",
                tareaId = Guid.NewGuid(),
                fechaAdicion = DateTime.Now,
                descripcion = "Tarea del hogar",
                estado = "Nueva",
                prioridad = "Baja",
                fechaInicio = DateTime.Now,
                fechaFin = DateTime.Now.AddDays(10),
                comentarios = "Ahora no me llevara tanto tiempo",
                Usuarios = null
            };

            //Act
            var result = tareaController.Put(tarea);

            //Assert
            Assert.IsNotNull(result);

            //Assert.AreEqual((int)HttpStatusCode.OK, result.sta);

        }

        [Test]
        public void cuandoEliminoTarea()
        {
            //Arrange 
            IServicioBase<Tarea, Guid> repositorioBase = Substitute.For<IServicioBase<Tarea, Guid>>();
            TareaController tareaController = new TareaController(repositorioBase);

            //Act
            var result = tareaController.Delete(new Guid("6BD5C8BD-6058-455B-806A-F5D0B13DB454"));

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void cuandoAsignoUnaTarea()
        {
            //Arrange 
            IServicioBase<Tarea, Guid> repositorioBase = Substitute.For<IServicioBase<Tarea, Guid>>();
            TareaController tareaController = new TareaController(repositorioBase);
            TareaUsuaioDTO UsuarioTarea = new TareaUsuaioDTO()
            {
                tareaId = Guid.NewGuid(),
                usuarioId = Guid.NewGuid()
            };
            //Act
            var result = tareaController.Asignar(UsuarioTarea);

            //Assert
            Assert.IsNotNull(result);
        }

    }
}