using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using API.Dtos;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class DepartamentoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DepartamentoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DepartamentoDto>>> Get()
    {
        var departamentos = await _unitOfWork.Departamento.GetAllAsync();

        return _mapper.Map<List<DepartamentoDto>>(departamentos);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Departamento>> Post(DepartamentoDto departamentoDto)
    {
        var departamento = _mapper.Map<Departamento>(departamentoDto);
        this._unitOfWork.Departamento.Add(departamento);
        await _unitOfWork.SaveAsync();
        if (departamento == null)
        {
            return BadRequest();
        }
        departamentoDto.Id = departamento.Id;
        return CreatedAtAction(nameof(Post), new {id = DepartamentoDto.Id}, DepartamentoDto);
    }
}
