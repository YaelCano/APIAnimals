using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

    public class CiudadController : BaseEntity
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;
        public CiudadController (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Ciudad>>> Get()
        {
            var ciudad = await _UnitOfWork.Ciudad.GetAllAsync();
            return null;
        }
    }
