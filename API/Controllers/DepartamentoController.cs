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
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Departamento>>> GetId(int id)
    {
        var departamento = await _unitOfWork.Departamento.GetByIdAsync(id);
        if (departamento == null)
        {
            return NotFound();
        }
        return Ok(departamento);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Departamento>> Post(DepartamentoDto departamentoDto)
    {
        var departamento = _mapper.Map<Departamento>(departamentoDto);
        this._unitOfWork.Departamento.Add(departamento);
        await _unitOfWork.SaveAsync();
        if(departamento == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), new {id = departamento.Id, departamento});
    }
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Departamento>> Put(int id,[FromBody] Departamento departamento)
    {
        if(departamento.Id == 0)
        {
            departamento.Id = id;
        }
        if(departamento.Id != id)
        {
            return BadRequest();    
        }
        if(departamento == null)
        {
            return NotFound();
        }
        _unitOfWork.Departamento.Update(departamento);
        await _unitOfWork.SaveAsync();
        return Ok(departamento);
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var departamento = await _unitOfWork.Departamento.GetByIdAsync(id);
        if(departamento == null)
        {
            return NotFound();
        }
        _unitOfWork.Departamento.Remove(departamento);
        await  _unitOfWork.SaveAsync();
        return NoContent();
    }
}