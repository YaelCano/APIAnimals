using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

    public class RazaController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper; 
        public RazaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _Mapper = mapper;
            
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<RazaDto>>> Get()
        {
            var raza = await _unitOfWork.Raza.GetAllAsync();
            _Mapper.Map<List<RazaDto>>(raza);
        }
    }
